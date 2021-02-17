using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using BC = BCrypt.Net.BCrypt;

namespace TaskManager.Controllers
{
    public class AccountController : Controller
    {
        public AccountContext db;
        public AccountController(AccountContext context)
        {
            db = context;
        }
        private void SetErrorMessage(string message)
        {
            ModelState.AddModelError("", message);
            ModelState.AddModelError("Email", " ");
            ModelState.AddModelError("Password", " ");
        }

        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async Task ResetAccountAccess(User user)
        {
            user.AccountAccess = true;
            user.LoginAttempts = 3;
            user.LockOutEnd = DateTime.Now.AddMinutes(15);

            await db.SaveChangesAsync();
        }

        private async Task UpdateAccountAccess(User user)
        {    
            DateTime currentTime = DateTime.Now;

            if (currentTime > user.LockOutEnd)
            {
                await ResetAccountAccess(user);
            }

            if (user.LoginAttempts == 0)
            {
                user.AccountAccess = false;
            }

            if (!user.AccountAccess)
            {
                SetErrorMessage("Восстановите пароль или попытайтесь войти через 15 минут");
            }

            if (user.AccountAccess)
            {
                user.LoginAttempts--;

                SetErrorMessage(string.Format("Введен неверный пароль. У Вас осталось {0} из 3 попыток", user.LoginAttempts));
            }

            await db.SaveChangesAsync();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Boards", "Pages");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Boards", "Pages");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Введены некорректные данные");
                return View();
            }
                
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                SetErrorMessage("Пользователь с указанной почтой не существует");
                return View();
            }
           
            await UpdateAccountAccess(user);

            if(user.AccountAccess)
            {
                if (BC.Verify(model.Password, user.Password))
                {
                    await Authenticate(model.Email);
                    await ResetAccountAccess(user);

                    return RedirectToAction("Boards", "Pages");
                }
            }           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMessage("Введены некорректные данные");
                return View();
            }

            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                SetErrorMessage("Пользователь с таким e-mail уже зарегистрирован на сайте. Выберите другой логин (e-mail), или восстановите пароль");

                return View();
            }

            db.Users.Add(new User
            {
                Email = model.Email,
                Password = BC.HashPassword(model.Password),
                LoginAttempts = 3,
                ValidatedEmail = false,
                LockOutEnd = DateTime.Now,
                AccountAccess = true
            });

            await db.SaveChangesAsync();
            await Authenticate(model.Email);

            return RedirectToAction("Boards", "Pages");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("UserEmail");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}
