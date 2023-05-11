namespace BookShoppingCartMvcUI.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders();

    }
}