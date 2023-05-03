﻿using ComputerForum.Interfaces;
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
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
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

                        return RedirectToAction("Index", "Home");
                    }
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
            var user = _userService.GetUserById(Int32.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
            return View(user);
        }

        [Authorize]
        public IActionResult UserDetailsEdit()
        {
            var user = _userService.GetUserById(Int32.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value));
            return View(user);
        }
        [HttpPost]
        [Authorize]
        public IActionResult UserDetailsEdit(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(user);
                RedirectToAction("UserDetails");
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
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            _tokenService.GenerateForgotPasswordToken(email);
            return RedirectToAction("Login");
        }

        public IActionResult NewUserPassword(string token)
        {
            PasswordResetToken? tmpToken = _tokenService.GetForgotPasswordToken(token);
            if (tmpToken != null)
            {
                return View(tmpToken);
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
                    _userService.ChangePassword(user);
                }
                return RedirectToAction("Login");
            }
            return View(passwordVM);

        }
    }
}
