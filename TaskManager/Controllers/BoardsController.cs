using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlusDashData.Data.Models.Accounts;
using PlusDashData.Data.Models.Content;
using PlusDashData.Data.ViewModels.Content;
using TaskManager.Services.WebClients.Interfaces;

namespace TaskManager.Controllers
{
    [Authorize]
    public class BoardsController: Controller
    {
        private readonly IAccountsWebClient _wcAccounts;
        private readonly IDashboardsWebClient _wcDashboards;
        public BoardsController(IAccountsWebClient wcAccounts, IDashboardsWebClient wcDashboards)
        {
            _wcAccounts = wcAccounts;
            _wcDashboards = wcDashboards;           
        }

        [HttpGet]
        public async Task<IActionResult> ShowBoards()
        {

            User user = await GetCurrentUser();

            string jsonPersBoards = await _wcDashboards.GetAsync("api/perdashboards/all/", $"{user.UserId}");
            List<PersonalDashboard> persBoards = JsonConvert.DeserializeObject<List<PersonalDashboard>>(jsonPersBoards);

            string jsonMultiBoards = await _wcDashboards.GetAsync("api/multidashboards/all/", $"{user.UserId}");
            List<MultiDashboard> multiBoards = JsonConvert.DeserializeObject<List<MultiDashboard>>(jsonMultiBoards);

            BoardsPageViewModel viewmodel = new BoardsPageViewModel { PersonalDashboards = persBoards, MultiDashboards = multiBoards };

            ViewBag.jsonPersBoards = jsonPersBoards;
            ViewBag.jsonMultiBoards = jsonMultiBoards;

            return View("~/Views/Pages/Boards.cshtml", viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonalBoard([FromForm] BoardsPageViewModel nameOfBoard)
        {
            User user = await GetCurrentUser();

            PersonalBoardViewModel newBoard = new PersonalBoardViewModel
            {
                DashboardName = nameOfBoard.PersonalBoardName,
                UserId = user.UserId,
            };

            await _wcDashboards.PostAsync<PersonalBoardViewModel>("api/perdashboards/add/", newBoard);

            return RedirectToAction(nameof(ShowBoards));
        }

        [HttpPost]
        public async Task<IActionResult> AddMultiBoard([FromForm] BoardsPageViewModel nameOfBoard)
        {
            User user = await GetCurrentUser();

            MultiBoardViewModel newBoard = new MultiBoardViewModel
            {
                DashboardName = nameOfBoard.MultiBoardName,
                UserId = user.UserId,
            };

            await _wcDashboards.PostAsync<MultiBoardViewModel>("api/multidashboards/add/", newBoard);

            return RedirectToAction(nameof(ShowBoards));
        }

        [HttpPut]
        public async Task UpdatePersonalBoards([FromQuery] string ids)
        {         
            await _wcDashboards.PutAsync("api/perdashboards/update", "?ids=" + ids);
        }

        [HttpPut]
        public async Task UpdateMultiBoards([FromQuery] string ids)
        {
            await _wcDashboards.PutAsync("api/multidashboards/update/", "?ids=" + ids);
        }

        [HttpDelete]
        public async Task DeletePersonalBoard([FromQuery] int id)
        {
            await _wcDashboards.DeleteAsync("api/perdashboards/delete", "?id=" + id);
        }

        [HttpDelete]
        public async Task DeleteMultiBoard([FromQuery] int id)
        {
            await _wcDashboards.DeleteAsync("api/multidashboards/delete", "?id=" + id);
        }

        [HttpPut]
        public async Task<JsonResult> EditPersonalBoard([FromQuery] int id, [FromQuery] string boardInfo)
        {
            User user = await GetCurrentUser();

            await _wcDashboards.PutAsync("api/perdashboards/edit", "?id=" + id + "&boardInfo=" + boardInfo);

            string jsonPersBoards = await _wcDashboards.GetAsync("api/perdashboards/all/", $"{user.UserId}");

            return Json(new { jsonBoards = jsonPersBoards });
        }

        [HttpPut]
        public async Task<JsonResult> EditMultiBoard([FromQuery] int id, [FromQuery] string boardInfo)
        {
            User user = await GetCurrentUser();

            await _wcDashboards.PutAsync("api/multidashboards/edit", "?id=" + id + "&boardInfo=" + boardInfo);

            string jsonPersBoards = await _wcDashboards.GetAsync("api/multidashboards/all/", $"{user.UserId}");

            return Json(new { jsonBoards = jsonPersBoards });
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
