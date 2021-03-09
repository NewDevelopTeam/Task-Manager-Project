using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TaskManager.Controllers
{
    public class CardsController: Controller
    {
        public AccountContext db;
        public CardsController(AccountContext context)
        {
            db = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddCard()
        {
            return RedirectToAction("Cards", "Pages");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCard(CardsViewModel card)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Pages/Cards.cshtml");
            }

            string userEmail = User.Identity.Name;
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            db.PerCards.Add(new PersonalCard
            {
                CardDescription = card.CardDescription,
                UserId = user.UserId,
            });

            await db.SaveChangesAsync();
            
            return RedirectToAction("Cards", "Pages");
        }

        [HttpPost]
        public void DeleteCard([FromBody] string cardId)
        {   
            int id = JsonConvert.DeserializeObject<int>(cardId);

            db.PerCards.Remove(db.PerCards.Find(id));
            db.SaveChanges();
        }

        [HttpPost]
        public void EditCards([FromBody]EditCardViewModel cardInfo)
        {
            int id = Int32.Parse(cardInfo.CardId);

            PersonalCard card = db.PerCards.Find(id);

            card.CardDescription = cardInfo.CardText;

            db.PerCards.Update(card);
            db.SaveChanges();
        }
    }
}
