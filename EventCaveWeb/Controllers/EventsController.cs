using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EventCaveWeb.Controllers
{
    [RoutePrefix("Events")]
    public class EventsController : Controller
    {
        private ApplicationUserManager _userManager;

        public EventsController()
        { }
        public EventsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("Search")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string keyword, string location, string date)
        {
            ViewBag.Keyword = keyword;
            ViewBag.Location = location;
            ViewBag.Date = date;
            return View();
        }

        [Route("Create")]
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUpdateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
                {
                    var Event = new Event
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Location = model.Location,
                        Datetime = model.Datetime,
                        Public = model.Public,
                        Limit = model.Limit,
                        CreatedAt = DateTime.Now,
                        Host = UserManager.FindById(User.Identity.GetUserId())
                    };
                    db.Events.Add(Event);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
