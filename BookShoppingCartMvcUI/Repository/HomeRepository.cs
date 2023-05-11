
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BookShoppingCartMvcUI.Repository
{
    public class HomeRepository:IHomeRepository
    {
        private readonly ApplicationDbContext _Context;
        public HomeRepository(ApplicationDbContext context)
        {
            _Context= context;
        }
        public async  Task<IEnumerable<Book>> GetBooks(string Sterm="",int genreid = 0)
        {
            Sterm=Sterm.ToLower();
            IEnumerable<Book> books = await (from book in _Context.Books
                         join gernre in _Context.Genres
                         on book.GenreId equals gernre.Id
                         where (book.BookName!=null && book.BookName.ToLower().StartsWith(Sterm))||string.IsNullOrWhiteSpace(Sterm)
                         select new Book
                         {
                             Id = book.Id,
                             Image = book.Image,
                             Price = book.Price,
                             AuthorName = book.AuthorName,
                             BookName = book.BookName,
                             GenreId = book.GenreId,
                             GenreName = gernre.GenreName
                         }).ToListAsync();

            if (genreid > 0)
            {
              books= books.Where(x=>x.GenreId==genreid).ToList();
            }
            return  books;
        }
        public async Task<IEnumerable<Genre>> AllGenres()
        {
            return await _Context.Genres.ToListAsync();
        }

    }
}
