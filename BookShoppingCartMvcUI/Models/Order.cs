namespace BookShoppingCartMvcUI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus Orderstatus { get; set; }
        public bool IsDeleted { get; set; } 
        public List<OrderDetail> orderDetails { get; set; }


    }
}
