using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlusDashData.Data.Models.Accounts;
using PlusDashData.Data.ViewModels.Accounts;
using BC = BCrypt.Net.BCrypt;

namespace Accounts_API.Controllers
{
    /// <summary>
    /// Controller for managing user's data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountsContext _db;
        public AccountsController(AccountsContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This Post method authenticates a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Serialized user's data</returns>
        [Route("login")]
        [HttpPost]
        public async Task<string> GetUserAsync([FromBody] LoginViewModel model)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null) return null;

            if (BC.Verify(model.Password, user.Password))
            {
                return user.Email;
            }
            return null;
        }

        /// <summary>
        /// This Post method creates a user's account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Serialized user's data</returns>
        [Route("create")]
        [HttpPost]
        public async Task<string> CreateUserAsync([FromBody] RegisterViewModel model)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null) return null;

            User newUser = new User
            {
                Email = model.Email,
                Password = BC.HashPassword(model.Password),
                ValidatedEmail = false,
            };

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return newUser.Email;
        }

        /// <summary>
        /// This Get method retrieves user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Serialized user's data</returns>
        [Route("users/{email}")]
        [HttpGet]
        public async Task<string> GetUserAsync([FromRoute]string email)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            return JsonConvert.SerializeObject(user);
        }

    }
}
