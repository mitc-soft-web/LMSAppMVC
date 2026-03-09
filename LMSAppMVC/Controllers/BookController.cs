using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LMSAppMVC.Controllers
{
    public class BookController(IBookService bookService) : Controller
    {
        private readonly IBookService _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();

            return View(books);
        }


        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(AddBookRequestModel request)
        {
            var books = await _bookService.AddBook(request);

            return RedirectToAction("Index");
        }


    }
}
