using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using PlusDashData.Data.ViewModels.Accounts;
using TaskManager.Services.WebClients.Interfaces;

namespace TaskManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountsWebClient _webClient;
        public AccountController(IAccountsWebClient webClient)
        {
            _webClient = webClient;
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

            string response = await _webClient.PostAsync<LoginViewModel>("api/accounts/login", model);

            if (response == null || response == "")
            {
                SetErrorMessage("Неверный логин и/или пароль");
                return View();
            }

            await Authenticate(model.Email);

            return RedirectToAction("Boards", "Pages");         
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

            string response = await _webClient.PostAsync<RegisterViewModel>("api/accounts/create/", model);
            
            if (response == null || response == "") 
            {
                SetErrorMessage("Пользователь с таким e-mail уже зарегистрирован на сайте. Выберите другой логин (e-mail), или восстановите пароль");
                return View();
            }

            await Authenticate(response);

            return RedirectToAction("Boards", "Pages");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {         
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Pages");
        }

        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private void SetErrorMessage(string message)
        {
            ModelState.AddModelError("", message);
            ModelState.AddModelError("Email", " ");
            ModelState.AddModelError("Password", " ");
        }
    }
}
