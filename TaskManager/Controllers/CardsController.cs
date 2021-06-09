using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using TaskManager.Services.WebClients.Interfaces;
using PlusDashData.Data.Models.Accounts;
using PlusDashData.Data.ViewModels.Content;

namespace TaskManager.Controllers
{
    [Authorize]
    public class CardsController: Controller
    {
        private readonly IAccountsWebClient _wcAccounts;
        private readonly IDashboardsWebClient _wcDashboards;
        public CardsController(IAccountsWebClient wcAccounts, IDashboardsWebClient wcDashboards)
        {
            _wcAccounts = wcAccounts;
            _wcDashboards = wcDashboards;
        }
        
        [HttpGet]
        public async Task<IActionResult> ShowCards()
        {
            User user = await GetCurrentUser();       
            ViewBag.strCards = await _wcDashboards.GetAsync("api/cards/all/", $"{user.UserId}");

            return View("~/Views/Pages/Cards.cshtml");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCard(CardsViewModel card)
        {
            User user = await GetCurrentUser();
            card.UserId = user.UserId;

            await _wcDashboards.PostAsync<CardsViewModel>("api/cards/add/", card);
               
            return RedirectToAction("Cards", "Pages");
        }
        
        [HttpDelete]
        public async Task DeleteCard(int id)
        {
            User user = await GetCurrentUser();

            await _wcDashboards.DeleteAsync("api/cards/delete/", "?cardId=" + id + "&userId=" + user.UserId);       
                       
        }

        [HttpPut]
        public async Task EditCard([FromQuery] int id, [FromQuery] string description)
        {
            await _wcDashboards.PutAsync("api/cards/edit", "?cardId=" + id + "&description=" + description);
        }

        [HttpPut]
        public async Task UpdatePositionsCards([FromQuery] string ids)
        {
            await _wcDashboards.PutAsync("api/cards/sort", "?ids=" + ids);
        }
        
        private async Task<User> GetCurrentUser()
        {
            string userEmail = User.Identity.Name;
            string userJson = await _wcAccounts.GetAsync("api/accounts/users/", userEmail);
            User user = JsonConvert.DeserializeObject<User>(userJson);
            return user;
        }
    }
}
