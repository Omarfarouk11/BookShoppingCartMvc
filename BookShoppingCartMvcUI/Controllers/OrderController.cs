using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCartMvcUI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _OrderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _OrderRepository = orderRepository;

        }
        public async Task <IActionResult> Userorders()
        {
            var orders=await _OrderRepository.UserOrders();

            return View(orders);
        }
    }
}
