using ComputerForum.Interfaces;
using ComputerForum.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<IActionResult> Login(UserLoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.LoginUser(userVM);
                if (user != null)
                {
                    int? userId = _userService.GetUserId(user.Name);
                    
                    var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
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
                ModelState.AddModelError("", "Wrong name or password");
                return View(userVM);
            }
            return View(userVM);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                bool result = _userService.AddUser(userVM);
                if (result == true)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "User with that name already exists");
                return View(userVM);
            }
            return View(userVM);
        }
        [Authorize]
        public async Task Signout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
