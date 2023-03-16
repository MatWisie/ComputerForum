using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ComputerForum.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login(UserVM userVM)
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Signout()
        {
            return View();
        }
    }
}
