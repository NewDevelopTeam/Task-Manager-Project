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
    public class PerDashboardsController : ControllerBase
    {
        private readonly DashboardsContext _db;
        public PerDashboardsController(DashboardsContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This method gets all data related to user's personal dashboards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All user's personal dashboards</returns>
        [Route("all/{userId}")]
        [HttpGet]
        public ActionResult<string> GetAllPerDashboards([FromRoute] int userId)
        {
            List<PersonalDashboard> persBoards = _db.PerDashBoards.Where(p => p.UserId == userId).ToList();
            string persBoardsJSON = JsonConvert.SerializeObject(persBoards);

            return Ok(persBoardsJSON);
        }

        /// <summary>
        /// This method adds a new personal dashboard
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Operation status message</returns>
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddPerDashboardAsync([FromBody] PersonalBoardViewModel model)
        {
            int numbOfPersBoards = await _db.PerDashBoards.CountAsync(n => n.UserId == model.UserId);

            await _db.PerDashBoards.AddAsync(new PersonalDashboard
            {
                DashboardName = model.DashboardName,
                UserId = model.UserId,
                PositionNo = numbOfPersBoards,
            });

            await _db.SaveChangesAsync();
            return Ok(new {message = "The dashboard was created" });
        }

        /// <summary>
        /// This method updates positions of personal dashboards
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Operation status message</returns>
        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> UpdatePositionsPerDashboardAsync([FromQuery(Name ="ids")] int[] ids)
        {
            int count = 0;

            foreach (var idBoard in ids)
            {
                PersonalDashboard board = _db.PerDashBoards.Where(x => x.Id == idBoard).FirstOrDefault();
                board.PositionNo = count;
                _db.PerDashBoards.Update(board);
                await _db.SaveChangesAsync();
                count++;
            }

            return Ok(new {message = "The positions were sorted successfully"});
        }

        /// <summary>
        /// This method deletes a personal dashboard
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation status message</returns>
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> DeletePerDashboardAsync([FromQuery] int id)
        {
            var board = _db.PerDashBoards.Find(id);

            if (board == null) return BadRequest(new { error = "The dashboard wasn't found" });

            _db.PerDashBoards.Remove(board);
            await _db.SaveChangesAsync();

            return Ok(new {message = "The dashboard was deleted successfully"});
        }

        /// <summary>
        /// This method changes the name of user's personal dashboard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boardInfo"></param>
        /// <returns>Operation status message</returns>
        [Route("edit")]
        [HttpPut]
        public async Task<IActionResult> EditPerDashboardAsync([FromQuery] int id, [FromQuery] string boardInfo)
        {
            PersonalDashboard board = _db.PerDashBoards.Find(id);

            if (board == null) return BadRequest(new {error = "The dashboard wasn't found"});

            board.DashboardName = boardInfo;

            _db.PerDashBoards.Update(board);
            await _db.SaveChangesAsync();

            return Ok(new {message = "The dashboard name was changed successfully"});
        }
    }
}
