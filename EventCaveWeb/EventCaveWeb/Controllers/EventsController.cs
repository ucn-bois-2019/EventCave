using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.ViewModels;

namespace EventCaveWeb.Controllers
{
    public class EventsController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();

        [Route("search")]
        public ActionResult Search(string keyword, string location, string date)
        {
            ViewBag.Keyword = keyword;
            ViewBag.Location = location;
            ViewBag.Date = date;
            return View();
        }

        [Route("events")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("events")]
        public ActionResult Create(CreateUpdateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Event = new Event
                {
                    Name = model.Name, Description = model.Description, Location = model.Location,
                    Datetime = model.Datetime, Public = model.Public, Limit = model.Limit, CreatedAt = DateTime.Now
                };
                db.Events.Add(Event);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
