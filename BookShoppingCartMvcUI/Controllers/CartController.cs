using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository= cartRepository;
        }
        public async Task<IActionResult> addItem(int bookId,int Qty=1,int redirect=0)
        {
            var cartcount=await _cartRepository.AddItem(bookId, Qty);
            if (redirect == 0)
            {
                return Ok(cartcount);
            }

            return RedirectToAction("GetUsercart");
        }
        public async Task<IActionResult> removeItem(int BookId)
        {
             await _cartRepository.RemoveItem(BookId);

            return RedirectToAction("GetUsercart");
        }      
        public async Task<IActionResult> GetUsercart()
        {
            var cartitem = await _cartRepository.GetUserCart();

            return View(cartitem);
        }
        public async Task<IActionResult> GetCartItemcounts()
        {
            int count=await _cartRepository.GetCartItemCount();
            return Ok(count);
        }
        public async Task<IActionResult> CheckOut()
        {
            bool ischecked=await _cartRepository.DoCheckOut();
            if (!ischecked)
            {
                return BadRequest("Something Is Wrong");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
