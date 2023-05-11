using BookShoppingCartMvcUI.DTOs;
using BookShoppingCartMvcUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _Repository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository repository)
        {
            _logger = logger;
            _Repository = repository;
        }

        public async Task< IActionResult> Index(string Book_name="",int Genre_Id=0)
        {
            var books = await _Repository.GetBooks(Book_name, Genre_Id);
            var genres = await _Repository.AllGenres();
            var BookModel = new BookDisplayModel
            {
                books= books,
                Genres= genres,
                book_name=Book_name,
                genre_id=Genre_Id
                
            };
        
          

            return View(BookModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}