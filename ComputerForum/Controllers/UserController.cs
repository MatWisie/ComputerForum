using ComputerForum.Interfaces;
using ComputerForum.Models;
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
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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
                    if(user.Active == false)
                    {
                        ModelState.AddModelError("", "User is not active");
                        _logger.LogError("User " + userVM.Name + " tried to login, but his account was not active");
                        return View(userVM);
                    }
                    else
                    {
                        int? userId = _userService.GetUserId(user.Name);
                        List<Claim> claims;
                        if (user.Admin != true)
                        {
                            claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                                new Claim(ClaimTypes.Role, "User"),
                            };
                        }
                        else
                        {
                            claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                                new Claim(ClaimTypes.Role, "Admin"),
                            };
                        }

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
                        _logger.LogInformation("Login " + userVM.Name + " successfull");
                        return RedirectToAction("Index", "Home");
                    }
                }
                _logger.LogError("Login " + userVM.Name + " failed, wrong name or password");
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
                _logger.LogError("User " + userVM.Name + " registered");
                _userService.AddUser(userVM);
                return RedirectToAction("Login");
            }
            return View(userVM);
        }
        [Authorize]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult UserDetails()
        {
            var user = _userService.GetUserByIdWithInclude(Int32.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
            return View(user);
        }

        [Authorize]
        public IActionResult UserSettings()
        {
            var user = _userService.GetUserToEditById(Int32.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
            return View(user);
        }
        [HttpPost]
        [Authorize]
        public IActionResult UserSettings(UserEditVM user)
        {
            user.Id = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value);
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateUser(user);
                    _httpContextAccessor.HttpContext.User.Identity.Name.Replace(HttpContext.User.Identity.Name, user.Name);
                    return RedirectToAction("UserDetails");
                }
                catch(Exception ex) 
                {
                    ModelState.AddModelError("", "User with this Name or Email already exists");
                    return View(user);
                }

            }
            return View(user);
        }

        [Authorize]
        public IActionResult UserTopics()
        {
            var user = _userService.GetUserByIdWithInclude(Int32.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
            return View(user);
        }

        public IActionResult ForgotPassword()
        {
            ForgotPasswordVM tmp = new ForgotPasswordVM();
            return View(tmp);
        }
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            _tokenService.GenerateForgotPasswordToken(forgotPasswordVM.email);
            return RedirectToAction("Login");
        }

        public IActionResult NewUserPassword(string token)
        {
            PasswordResetToken? tmpToken = _tokenService.GetForgotPasswordToken(token);
            if (tmpToken != null)
            {
                PasswordChangeVM tmp = new PasswordChangeVM();
                tmp.userId = tmpToken.UserId;
                tmp.token = tmpToken.Token;
                return View(tmp);
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult NewUserPassword(PasswordChangeVM passwordVM)
        {
            if (ModelState.IsValid)
            {
                User? user = _userService.GetUserById(passwordVM.userId);
                if (user != null)
                {
                    _tokenService.DeleteUserForgotPasswordTokens(passwordVM.userId);
                    user.Password = passwordVM.Password;
                    _logger.LogInformation("User " + passwordVM.userId + " changed password");
                    _userService.ChangePassword(user);
                }
                return RedirectToAction("Login");
            }
            return View(passwordVM);

        }
    }
}
