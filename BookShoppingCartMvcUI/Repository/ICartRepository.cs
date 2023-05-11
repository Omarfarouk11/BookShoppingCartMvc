using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repository
{
    public interface ICartRepository
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
       Task<ShoppingCart> GetCart(string userId);
       string GetUserId();
       Task<bool> DoCheckOut();


    }
}