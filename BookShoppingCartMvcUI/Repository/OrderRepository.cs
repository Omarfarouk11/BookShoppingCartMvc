using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _Context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userid=GetUserId();
            if(userid == null)
            {
                throw new Exception("Invaild User Login");
            }
            var orders = await _Context.Orders
                        .Include(s => s.Orderstatus)
                        .Include(o => o.orderDetails)
                        .ThenInclude(b => b.Book)
                        .ThenInclude(g => g.genre).Where(u => u.UserId==userid).ToListAsync();
            return orders;


        }
        public string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }


    }
}
