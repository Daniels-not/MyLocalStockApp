using Microsoft.AspNetCore.Mvc;
using MyStockApp.Core.Application.Helpers;
using MyStockApp.Core.Application.Interfaces.Services;
using MyStockApp.Core.Application.ViewModels.User;
using MyStockApp.Middlewares;

namespace MyStockApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            UserViewModel userdata = await _userService.Login(data);
            if (userdata != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userdata);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Wrong access data");
            }

            return View(data);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Add(data);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
