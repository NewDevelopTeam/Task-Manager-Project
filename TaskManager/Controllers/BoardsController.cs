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
    public class BoardsController: Controller
    {
        private AccountContext db;
        public BoardsController(AccountContext context)
        {
            db = context;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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


        [Authorize]
        [HttpGet]
        public int test(int boardId)
        {
            return boardId;
        }

        [Authorize]
        [HttpGet]
        public IActionResult PersonalBoard(int boardId)
        {
            PersonalBoardViewModel obj = new PersonalBoardViewModel {};
            return View("~/Views/Pages/PersDashboard.cshtml", obj);
        }
    }
}
