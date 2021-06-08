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
        public async Task<ActionResult<string>> GetUserAsync([FromBody] LoginViewModel model)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null) return BadRequest(new {error = "The user wasn't found" });

            if (BC.Verify(model.Password, user.Password))
            {
                return Ok(user.Email);
            }
            return BadRequest(new {error = "The password incorrect" });
        }

        /// <summary>
        /// This Post method creates a user's account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Serialized user's data</returns>
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<string>> CreateUserAsync([FromBody] RegisterViewModel model)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null) return BadRequest(new { error = "The user already exists" } );

            User newUser = new User
            {
                Email = model.Email,
                Password = BC.HashPassword(model.Password),
                ValidatedEmail = false,
            };

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return Ok(newUser.Email);
        }

        /// <summary>
        /// This Get method retrieves user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>Serialized user's data</returns>
        [Route("users/{userEmail}")]
        [HttpGet]
        public async Task<ActionResult<string>> GetUserAsync([FromRoute] string userEmail)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return BadRequest(new {error = "This user doesn't exist" });
            return JsonConvert.SerializeObject(user);
        }
    }
}
