using LMSAppMVC.Contracts.Services;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMSAppMVC.Controllers
{
    public class AuthController(IUserService userService, IIdentityService identityService) : Controller
    {
        private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel
            {
                Member = new RegisterMemberRequestModel(),
                Librarian = new RegisterLibrarianRequestModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMember([Bind(Prefix = "Member")] RegisterMemberRequestModel request)
        {
            var viewModel = new RegisterViewModel { Member = request };
            if (!ModelState.IsValid)
            {
                return View("Register", viewModel);
            }
            var registerMember = await _userService.RegisterMemberAsync(request);

            if (registerMember.Status)
            {
                TempData["SuccessMessage"] = registerMember.Message;
                return RedirectToAction("Login");
            }

            ViewBag.Message = registerMember.Message;
            return View("Register", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterLibrarian([Bind(Prefix = "Librarian")] RegisterLibrarianRequestModel request)
        {
            var viewModel = new RegisterViewModel { Librarian = request };
            if (!ModelState.IsValid)
            {

                return View("Register", viewModel);
            }
            var registerLibrarian = await _userService.RegisterLibrarianAsync(request);

            if (registerLibrarian.Status)
            {
                TempData["SuccessMessage"] = registerLibrarian.Message;
                return RedirectToAction("Login");
            }

            ViewBag.Message = registerLibrarian.Message;
            return View(viewModel);
      
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var loginResponse = await _userService.LoginAsync(model);
            var checkRole = "";
            if (loginResponse.Status)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginResponse.Data.FullName),
                    new Claim(ClaimTypes.Email, loginResponse.Data.Email),
                    new Claim(ClaimTypes.NameIdentifier, loginResponse.Data.UserId.ToString()),
                     new Claim(ClaimTypes.Role, loginResponse.Data.Role),

                };

                checkRole = loginResponse.Data.Role;

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, authenticationProperties);
                if (checkRole == "Member")
                {
                    return RedirectToAction("MemberDashboard", "User");
                }
                else if (checkRole == "Librarian")
                {
                    return RedirectToAction("LibrarianDashboard", "User");
                }
                return RedirectToAction("AdminDashboard", "User");

            }
            else
            {
                ViewBag.Message = loginResponse.Message;
                return View(model);
            }

        }

    }
}
