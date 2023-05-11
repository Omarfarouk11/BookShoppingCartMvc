using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingCartMvcUI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string BookName { get; set; }
        [MaxLength(40)]
        public string AuthorName { get; set; }
        
        public int GenreId { get; set; }
        public double Price { get; set; }

  

        public Genre genre { get; set; }
        public string? Image { get; set; }

        public List<OrderDetail> orderDetails { get; set; } 
        public List<CartDetail> cartDetails { get; set; }
        [NotMapped]
        public string? GenreName { get; set; }


    }
}
