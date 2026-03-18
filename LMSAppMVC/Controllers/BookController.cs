using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace LMSAppMVC.Controllers
{
    public class BookController(IBookService bookService,
        IAuthorService authorService, ICategoryService categoryService) : Controller
    {
        private readonly IBookService _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        private readonly IAuthorService _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        private readonly ICategoryService _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        [Authorize(Roles ="Librarian, Admin, Member")]
        [HttpGet]
        public async Task<IActionResult> AllBooks()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            var books = await _bookService.GetAllBooksAsync();

            if (books.Status)
            {
                return View(books);
            }

            ViewBag.Message = books.Message;
            return View(books);
        }

        [Authorize(Roles = "Librarian, Admin, Member")]
        [HttpGet]
        public async Task<IActionResult> AvailableBooks()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            var books = await _bookService.GetAllAvailableBooksAsync();

            if (books.Status)
            {
                return View(books);
            }

            ViewBag.Message = books.Message;
            return View(books);
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public async Task<IActionResult> BookDetailsForMember(Guid id)
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            var book = await _bookService.GetBookByIdForMemberAsync(id);

            if (book.Status)
            {
                return View(book);
            }

            ViewBag.Message = book.Message;
            return View(book);
        }


        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public async Task<IActionResult> BookDetailsForLibrarian(Guid id)
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            var book = await _bookService.GetBookByIdForLibrarianAsync(id);

            if (book.Status)
            {
                return View(book);
            }

            ViewBag.Message = book.Message;
            return View(book);
        }


        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            var authors = await _authorService.GetAllAuthorsAsync();
            ViewData["Authors"] = new SelectList(authors.Data, "Id", "FullName");

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["Categories"] = new SelectList(categories.Data, "Id", "Name");

            return View();
        }

        [Authorize(Roles ="Librarian")]
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var book = await _bookService.AddBookAsync(request);
            if (book.Status)
            {
                return RedirectToAction("AllBooks");
            }
            ViewBag.Message = book.Message;

            return View(request);
        }


    }
}
