using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers
{ 
    public class PagesController : Controller
    {
        [HttpGet]
        public IActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Boards", "Pages");
            }
            return View("~/Views/Account/Registration.cshtml");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Boards", "Pages");
            }
            return View("~/Views/Account/Login.cshtml");
        }

        [Authorize]
        public IActionResult Cards()
        {
            return RedirectToAction("ShowCards", "Cards");
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
