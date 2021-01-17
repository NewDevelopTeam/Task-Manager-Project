using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Cards()
        {
            return View();
        }
        public IActionResult Boards()
        {
            return View();
        }
    }
}
