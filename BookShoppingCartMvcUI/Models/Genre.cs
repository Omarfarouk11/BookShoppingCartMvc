using System.ComponentModel.DataAnnotations;

namespace BookShoppingCartMvcUI.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string? GenreName { get; set; }
        public List<Book> Books { get; set; }


    }
}
