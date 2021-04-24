using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Models.BoardsPageModels;
using TaskManager.ViewModels.BoardsPageViewModels;

namespace TaskManager.Controllers
{
    [Authorize]
    public class BoardsController: Controller
    {
        private AccountContext db;
        public BoardsController(AccountContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> ShowBoards()
        {
            string userEmail = User.Identity.Name;
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            int userId = user.UserId;

            List<PersonalDashboard> persBoards = db.PerDashBoards.Where(p => p.UserId == userId).ToList();
            List<MultiDashboard> multiBoards = db.MultiDashBoards.Where(p => p.UserId == userId).ToList();

            BoardsPageViewModel viewmodel = new BoardsPageViewModel { PersonalDashboards = persBoards, MultiDashboards = multiBoards };

            string jsonPersBoards = JsonConvert.SerializeObject(persBoards);
            string jsonMultiBoards = JsonConvert.SerializeObject(multiBoards);

            ViewBag.jsonPersBoards = jsonPersBoards;
            ViewBag.jsonMultiBoards = jsonMultiBoards;

            return View("~/Views/Pages/Boards.cshtml", viewmodel);
        }
        [HttpGet]
        public async Task<IActionResult> AddPersonalBoard(BoardsPageViewModel nameOfBoard)
        {
            string userEmail = User.Identity.Name;
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            int numbOfPersBoards = await db.PerDashBoards.CountAsync(n => n.User == user);

            db.PerDashBoards.Add(new PersonalDashboard
            {
                DashboardName = nameOfBoard.PersonalBoardName,
                UserId = user.UserId,
                PositionNo = numbOfPersBoards,
            });

            await db.SaveChangesAsync();

            return RedirectToAction(nameof(ShowBoards));
        }
        [HttpGet]
        public async Task<IActionResult> AddMultiBoard(BoardsPageViewModel nameOfBoard)
        {
            string userEmail = User.Identity.Name;
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            int numbOfPersBoards = await db.MultiDashBoards.CountAsync(n => n.User == user);

            db.MultiDashBoards.Add(new MultiDashboard
            {
                DashboardName = nameOfBoard.MultiBoardName,
                UserId = user.UserId,
                PositionNo = numbOfPersBoards,
            });

            await db.SaveChangesAsync();

            return RedirectToAction(nameof(ShowBoards));
        }
        [HttpGet]
        public int test(int boardId)
        {
            return boardId;
        }
        [HttpGet]
        public IActionResult PersonalBoard(int boardId)
        {
            PersonalBoardViewModel obj = new PersonalBoardViewModel {};
            return View("~/Views/Pages/PersDashboard.cshtml", obj);
        }
        [HttpGet]
        public void UpdatePersonalBoards(List<string> data)
        {
            List<int> numOfBoards = data.Select(s => int.Parse(s)).ToList();
            int count = 0;
            foreach (var idBoard in numOfBoards)
            {
                PersonalDashboard board = db.PerDashBoards.Where(x => x.Id == idBoard).FirstOrDefault();
                board.PositionNo = count;
                db.PerDashBoards.Update(board);
                db.SaveChanges();
                count++;
            }
        }
        [HttpGet]
        public void UpdateMultiBoards(List<string> data)
        {
            List<int> numOfBoards = data.Select(s => int.Parse(s)).ToList();
            int count = 0;
            foreach (var idBoard in numOfBoards)
            {
                MultiDashboard board = db.MultiDashBoards.Where(x => x.Id == idBoard).FirstOrDefault();
                board.PositionNo = count;
                db.MultiDashBoards.Update(board);
                db.SaveChanges();
                count++;
            }
        }
        [HttpGet]
        public void DeletePersonalBoard(int id)
        {
            db.PerDashBoards.Remove(db.PerDashBoards.Find(id));
            db.SaveChanges();
        }
        [HttpGet]
        public void DeleteMultiBoard(int id)
        {
            db.MultiDashBoards.Remove(db.MultiDashBoards.Find(id));
            db.SaveChanges();
        }
        [HttpPost]
        public JsonResult EditPersonalBoard([FromBody]EditPersonalBoardViewModel boardInfo)
        {
            int id = Int32.Parse(boardInfo.BoardId);

            PersonalDashboard board = db.PerDashBoards.Find(id);
            board.DashboardName = boardInfo.BoardName;

            db.PerDashBoards.Update(board);
            db.SaveChanges();

            string userEmail = User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            int userId = user.UserId;

            List<PersonalDashboard> listOfBoards = db.PerDashBoards.Where(p => p.UserId == userId).ToList();
            string boards = JsonConvert.SerializeObject(listOfBoards);

            return Json(new { jsonBoards = boards });
        }
        [HttpPost]
        public JsonResult EditMultiBoard([FromBody] EditPersonalBoardViewModel boardInfo)
        {
            int id = Int32.Parse(boardInfo.BoardId);

            MultiDashboard board = db.MultiDashBoards.Find(id);
            board.DashboardName = boardInfo.BoardName;

            db.MultiDashBoards.Update(board);
            db.SaveChanges();

            string userEmail = User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            int userId = user.UserId;

            List<MultiDashboard> listOfBoards = db.MultiDashBoards.Where(p => p.UserId == userId).ToList();
            string boards = JsonConvert.SerializeObject(listOfBoards);

            return Json(new { jsonBoards = boards });
        }
    }
}
