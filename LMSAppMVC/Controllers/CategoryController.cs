using LMSAppMVC.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMSAppMVC.Controllers
{
    public class CategoryController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        [Authorize(Roles ="Librarian, Admin")]
        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            var categoryResponse = await _categoryService.GetAllCategoriesAsync();
            if (categoryResponse.Status)
            {

                return View(categoryResponse);
            }
            ViewBag.Message = categoryResponse.Message;

            return View(categoryResponse);
        }

        public IActionResult AddCategory()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            Console.WriteLine("NAME: " + startName);
            ViewBag.NameAvatar = startName;

            return View();
        }

        [Authorize(Roles = "Librarian")]
       [HttpPost]
        public async Task<IActionResult> AddCategory(string name, string description)
        {
            if (!ModelState.IsValid)
            {
                return View(name, description);
            }
            var addCategory = await _categoryService.AddCategoryAsync(name, description);
            if (addCategory.Status)
            {
                return RedirectToAction("AllCategories");
            }
            ViewBag.Message = addCategory.Message;
            return View(name, description);
        }
    }
}

