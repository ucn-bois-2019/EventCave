using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCaveWeb.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Keyword = "",
                Location = "",
                DateTime = DateTime.Today,
                Categories = db.Categories.Take(4)
            };
            return View(homeViewModel);
        }

        [Route]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SearchRedirect(HomeViewModel homeViewModel)
        {
            return RedirectToAction("Search", "Events", new { keyword = homeViewModel.Keyword, location = homeViewModel.Location, date = homeViewModel.DateTime });
        }
    }

}