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
        private AccountContext db;
        public CardsController(AccountContext context)
        {
            db = context;
        }
        public void UpdatePositions(List<int> listofId)
        {
            int count = 0;
            foreach (var idCard in listofId)
            {
                PersonalCard item = db.PerCards.Where(x => x.Id == idCard).FirstOrDefault();
                item.RowNo = count;
                db.PerCards.Update(item);
                db.SaveChanges();
                count++;
            }
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
            int numbOfCards = await db.PerCards.CountAsync(n => n.User == user);

            db.PerCards.Add(new PersonalCard
            {
                CardDescription = card.CardDescription,
                UserId = user.UserId,
                RowNo = numbOfCards,
            });

            await db.SaveChangesAsync();          
            return RedirectToAction("Cards", "Pages");
        }
        [Authorize]
        [HttpGet]
        public void DeleteCard(int id)
        {   
            db.PerCards.Remove(db.PerCards.Find(id));
            db.SaveChanges();

            string userEmail = User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            int userId = user.UserId;

            List<PersonalCard> listOfCards = db.PerCards.Where(p => p.UserId == userId).ToList();
            List<int> numOfCards = listOfCards.Select(card => card.Id).ToList();
            UpdatePositions(numOfCards);
        }
        
        [Authorize]
        [HttpGet]
        public void EditCard(int id, string description)
        {
            PersonalCard card = db.PerCards.Find(id);
            card.CardDescription = description;

            db.PerCards.Update(card);
            db.SaveChanges();
        }

        [Authorize]
        [HttpGet]
        public void UpdateCards(string ids)
        {
            List<int> list = JsonConvert.DeserializeObject<List<int>>(ids);
            UpdatePositions(list);
        }
    }
}
