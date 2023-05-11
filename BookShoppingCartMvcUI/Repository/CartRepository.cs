using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repository
{
    public class CartRepository:ICartRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public CartRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _Context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<int> AddItem(int bookId, int qty)
        {
            string userId = GetUserId();
            using var transaction = _Context.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _Context.ShoppingCarts.Add(cart);
                }
                _Context.SaveChanges();
                // cart detail section
                var cartItem = _Context.cartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
             
                else
                {
                    var book = await _Context.Books.FindAsync(bookId);
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice=book.Price
                      
                    };
                    _Context.cartDetails.Add(cartItem);
                }
                _Context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartitemcount=await GetCartItemCount(userId);
            return cartitemcount;

         
        }
        public async Task<int> RemoveItem(int bookId)
        {
       
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                // cart detail section
                var cartItem = _Context.cartDetails
                                  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
                if (cartItem is null)
                    throw new Exception("Not items in cart");
                else if (cartItem.Quantity == 1)
                    _Context.cartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                _Context.SaveChanges();


            }
            catch (Exception ex)
            {
             
            }
            var cartitemcount = await GetCartItemCount(userId);
            return cartitemcount;
        }
        public async Task<ShoppingCart> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var shoppingCart = await _Context.ShoppingCarts
                                  .Include(a => a.Cartdetails)
                                  .ThenInclude(a => a.Book)
                                  .ThenInclude(a => a.genre)
                                  .Where(a => a.UserId == userId).FirstOrDefaultAsync();
            return shoppingCart;

        }
        public async Task <int> GetCartItemCount(string userId="")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId=GetUserId();

            }
            var data = await (from Cart in _Context.ShoppingCarts
                              join cartdetails in _Context.cartDetails
                              on Cart.Id equals cartdetails.ShoppingCartId
                              select new { cartdetails.Id }

                              ).ToListAsync();
            return data.Count;
        }
        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _Context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }
        public async Task<bool> DoCheckOut()
        {
            string userId = GetUserId();

            var transaction = _Context.Database.BeginTransaction();
            try
            {
                if(userId== null)
                {
                    throw new Exception("In Vaild User");

                }
                var cart=await GetCart(userId);
                if(cart == null)
                {
                    throw new Exception("Cart Is Empty");
                }
                var cartdetails= await _Context.cartDetails.Where(c=>c.ShoppingCartId==cart.Id).ToListAsync();
                if (cartdetails.Count == 0)
                {
                    throw new Exception("No Item in Your Cart");
                }
                var order = new Order
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    OrderStatusId=1,
                    

                };

               
                _Context.Add(order);
                _Context.SaveChanges();
                foreach(var item in cartdetails)
                {
                    var orderdetail = new OrderDetail
                    {
                        BookId= item.BookId,
                        Quantity  = item.Quantity,
                        UnitPrice= item.UnitPrice,
                        OrderId=order.Id

                    };
                    _Context.Add(orderdetail);
                }
                _Context.SaveChanges();
                //reomve Cart Details
                _Context.cartDetails.RemoveRange(cartdetails);
                _Context.SaveChanges();
               transaction.Commit();
                return true;
                
            }
            catch (Exception)
            {
                return false;
                
            }
        }
        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

       
    }

  
}
