using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LMSAppMVC.Controllers
{
    public class AuthorController(IAuthorService authorService) : Controller
    {
        private readonly IAuthorService _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));

        [Authorize(Roles = "Librarian, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authorsResponse = await _authorService.GetAllAuthorsAsync();

            if (authorsResponse.Status)
            {
                return View(authorsResponse);
            }

            ViewBag.Error = authorsResponse.Message;
            return View(authorsResponse);
        }

        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public async Task<IActionResult> CreateAuthor()
        {

            return View();
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var bookResponse = await _authorService.CreateAuthorAsync(request);
            if (bookResponse.Status)
            {
                TempData["SuccessMessage"] = bookResponse.Message;
                return RedirectToAction("Index");
            }
            
            ViewBag.Message = bookResponse.Message;
            return View(request);
        }
    }
}
