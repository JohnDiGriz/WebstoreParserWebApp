using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParserWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Products");
        }
        public ActionResult About()
        {
            return View(new ViewModels.MessageViewModel() { Name = "About", Message = "Please hire me :)." });
        }
    }
}