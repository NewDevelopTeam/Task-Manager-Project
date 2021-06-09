using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Dashboards_API.Models;
using PlusDashData.Data.Models.Content;
using PlusDashData.Data.ViewModels.Content;
using Newtonsoft.Json;

namespace Dashboards_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly DashboardsContext _db;
        public CardsController(DashboardsContext db)
        {
            _db = db;
        }
        
        /// <summary>
        /// This method gets all data related to user's cards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All user's cards</returns>
        [Route("all/{userId}")]
        [HttpGet]
        public string GetAllCards([FromRoute] int userId)
        {
            List<PersonalCard> listOfCards = _db.PerCards.Where(p => p.UserId == userId).ToList();
            string cardsJSON = JsonConvert.SerializeObject(listOfCards);

            return cardsJSON;
        }
        
        /// <summary>
        /// This method adds a new card
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Operation status message</returns>
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddPerCardAsync([FromBody] CardsViewModel model)
        {
            int numbOfCards = await _db.PerCards.CountAsync(n => n.UserId == model.UserId);

            await _db.PerCards.AddAsync(new PersonalCard
            {
                CardDescription = model.CardDescription,
                UserId = model.UserId,
                RowNo = numbOfCards,
            });

            await _db.SaveChangesAsync();

            return Ok(new { message = "The card was added successfully" });
        }

        /// <summary>
        /// This method deletes a card
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="userId"></param>
        /// <returns>Operation status message</returns>
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> DeletePerCardAsync([FromQuery] int cardId, [FromQuery] int userId)
        {
            var card = _db.PerCards.Find(cardId);

            if (card == null) return BadRequest(new { error = "The card wasn't found" });

            _db.PerCards.Remove(card);
            await _db.SaveChangesAsync();

            List<PersonalCard> listOfCards = _db.PerCards.Where(p => p.UserId == userId).ToList();
            int[] numOfCards = listOfCards.Select(card => card.Id).ToArray();

            await UpdatePositions(numOfCards);

            return Ok(new { message = "The card was deleted successfully" });
        }

        /// <summary>
        /// This method edits a card name
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="description"></param>
        /// <returns>Operation status message</returns>
        [Route("edit")]
        [HttpPut]
        public async Task<IActionResult> EditPerCardAsync([FromQuery] int cardId, [FromQuery] string description)
        {
            PersonalCard card = _db.PerCards.Find(cardId);

            if(card == null) return BadRequest(new { error = "The card wasn't found" });

            card.CardDescription = description;

            _db.PerCards.Update(card);
            await _db.SaveChangesAsync();

            return Ok(new {message = "The card description was changed successfully"});
        }

        /// <summary>
        /// This method saves positions of sorted cards
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Operation status message</returns>
        [Route("sort")]
        [HttpPut]
        public async Task<IActionResult> UpdatePositionsPerCardAsync([FromQuery(Name = "ids")] int[] ids)
        {
            await UpdatePositions(ids);

            return Ok(new { message = "The positions were sorted successfully" });
        }

        private async Task UpdatePositions(int[] ids)
        {
            int count = 0;

            foreach (var idCard in ids)
            {
                PersonalCard item = _db.PerCards.Where(x => x.Id == idCard).FirstOrDefault();
                item.RowNo = count;
                _db.PerCards.Update(item);
                await _db.SaveChangesAsync();
                count++;
            }
        }
    }
}
