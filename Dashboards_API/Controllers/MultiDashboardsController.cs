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
    [ApiController]
    [Route("api/[controller]")]
    public class MultiDashboardsController : ControllerBase
    {
        private readonly DashboardsContext _db;
        public MultiDashboardsController(DashboardsContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This method gets all data related to user's multiple dashboards
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All user's multiple dashboards</returns>
        [Route("all/{userId}")]
        [HttpGet]
        public ActionResult<string> GetAllMultiDashboards([FromRoute] int userId)
        {
            List<MultiDashboard> multiBoards = _db.MultiDashBoards.Where(p => p.UserId == userId).ToList();
            string multiBoardsJSON = JsonConvert.SerializeObject(multiBoards);

            return Ok(multiBoardsJSON);
        }

        /// <summary>
        /// This method adds a new multiple dashboard
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Operation status message</returns>
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddMultiDashboardAsync([FromBody] MultiBoardViewModel model)
        {
            int numbOfMultiBoards = await _db.MultiDashBoards.CountAsync(n => n.UserId == model.UserId);

            await _db.MultiDashBoards.AddAsync(new MultiDashboard
            {
                DashboardName = model.DashboardName,
                UserId = model.UserId,
                PositionNo = numbOfMultiBoards,
            });

            await _db.SaveChangesAsync();
            return Ok(new { message = "The dashboard was created" });
        }

        /// <summary>
        /// This method updates positions of multiple dashboards
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Operation status message</returns>
        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> UpdatePositionsMultiDashboardAsync([FromQuery(Name = "ids")] int[] ids)
        {
            int count = 0;

            foreach (var idBoard in ids)
            {
                MultiDashboard board = _db.MultiDashBoards.Where(x => x.Id == idBoard).FirstOrDefault();
                board.PositionNo = count;
                _db.MultiDashBoards.Update(board);
                await _db.SaveChangesAsync();
                count++;
            }

            return Ok(new { message = "The positions were sorted successfully" });
        }

        /// <summary>
        /// This method deletes a multiple dashboard
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Operation status message</returns>
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteMultiDashboardAsync([FromQuery] int id)
        {
            var board = _db.MultiDashBoards.Find(id);

            if (board == null) return BadRequest(new { error = "The dashboard wasn't found" });

            _db.MultiDashBoards.Remove(board);
            await _db.SaveChangesAsync();

            return Ok(new { message = "The dashboard was deleted successfully" });
        }

        /// <summary>
        /// This method changes the name of user's multiple dashboard
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boardInfo"></param>
        /// <returns>Operation status message</returns>
        [Route("edit")]
        [HttpPut]
        public async Task<IActionResult> EditMultiDashboardAsync([FromQuery] int id, [FromQuery] string boardInfo)
        {
            MultiDashboard board = _db.MultiDashBoards.Find(id);

            if (board == null) return BadRequest(new { error = "The dashboard wasn't found" });

            board.DashboardName = boardInfo;

            _db.MultiDashBoards.Update(board);
            await _db.SaveChangesAsync();

            return Ok(new { message = "The dashboard name was changed successfully" });
        }
    }
}
