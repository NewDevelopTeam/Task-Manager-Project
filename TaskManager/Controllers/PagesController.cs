using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers
{ 
    public class PagesController : Controller
    {
        [Authorize]
        public IActionResult Cards()
        {
            return View();
        }
        [Authorize]
        public IActionResult Boards()
        {
            return View();
        }
        [Authorize]
        public IActionResult Notifications()
        {
            return View();
        }
    }
}
