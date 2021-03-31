using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TaskManager.Controllers
{ 
    public class PagesController : Controller
    {
        public AccountContext db;
        public PagesController(AccountContext context)
        {
            db = context;
        }

        [Authorize]
        public async Task<IActionResult> Cards()
        {
            string userEmail = User.Identity.Name;
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            int userId = user.UserId;

             List<PersonalCard> listOfCards = db.PerCards.Where(p => p.UserId == userId).ToList();

             string str  = JsonConvert.SerializeObject(listOfCards);

            ViewBag.strCards = str;

            return View();
        }

        [Authorize]
        public IActionResult Boards()
        {
            return RedirectToAction("ShowBoards", "Boards");
        }

        [Authorize]
        public IActionResult Notifications()
        {
            return View();
        }
    }
}
