using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMSAppMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LibrarianDashboard()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            ViewBag.NameAvatar = startName;

            var librarianIdString = User?.FindFirst("LibrarianId")?.Value;

            return View();
        }

        public IActionResult MemberDashboard()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            ViewBag.NameAvatar = startName;
            return View();
        }
    }
}
