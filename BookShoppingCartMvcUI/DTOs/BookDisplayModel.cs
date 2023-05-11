namespace BookShoppingCartMvcUI.DTOs
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string book_name { get; set; } = "";
        public int genre_id { get; set; } = 0;
    }
}
