using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUI.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public int StatusId { get; set; }   
        public string ?StatusName { get; set; }
    }
}
