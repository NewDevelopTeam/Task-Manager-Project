using System;
using External_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace External_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private AccountContext db;
        public AccountsController(AccountContext context)
        {
            db = context;
        }

        //<summary>
        //The POST method provides user's account data
        //</summary>
        //<param name="model"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

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

        [HttpPut]
        private async Task UpdateAccountAccess(User user)
        {
            DateTime currentTime = DateTime.Now;

            await db.SaveChangesAsync();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {

            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

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
    }
}






