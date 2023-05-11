namespace BookShoppingCartMvcUI.Repository
{
    public interface IHomeRepository
    {
          Task<IEnumerable<Book>> GetBooks(string Sterm = "", int genreid = 0);
          Task<IEnumerable<Genre>> AllGenres();




    }


}