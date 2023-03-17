using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost]
        public async Task<IActionResult> Login(UserVM userVM)
        {
            var user = _userService.GetUser(userVM);
            if(user != null)
            {
                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.Role, "User"),
                            };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                };
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties); 

                return RedirectToAction("Index", "Home");
            }
           
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
