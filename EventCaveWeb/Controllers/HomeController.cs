using EventCaveWeb.Database;
using EventCaveWeb.Entities;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                Categories = db.Categories.ToList(),
                RandomEvents = GetRandomEvents(db.Events, 4),
                DateTime = DateTime.Now
            };
            return View(homeViewModel);
        }

        public List<Event> GetRandomEvents(DbSet<Event> eventSet, int count)
        {
            int eventCount = eventSet.Count();
            List<int> randomNumbers = new List<int>();
            List<Event> events = new List<Event>();
            Random random = new Random();

            while (randomNumbers.Count < count)
            {
                int randomNumber = random.Next(0, eventCount);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            foreach (var num in randomNumbers)
            {
                events.Add(eventSet.ToArray()[num]);
            }

            return events;
        }
    }

}