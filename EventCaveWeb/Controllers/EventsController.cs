using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EventCaveWeb.Utils;

namespace EventCaveWeb.Controllers
{
    [RoutePrefix("Events")]
    public class EventsController : Controller
    {

        private ApplicationUserManager _userManager;

        public EventsController()
        {
        }

        public EventsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(Event eventModel)
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            var events = db.Events.ToList();
            return View(events);
        }

        [Route("Search")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string keyword, string location, DateTime date)
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            var events = db.Events.Where(e => e.Name.Equals(keyword)).Where(e => e.Location.Equals(location));
            return View(events.ToList());
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
                        Limit = model.Limit,
                        CreatedAt = DateTime.Now,
                        Host = UserManager.FindById(User.Identity.GetUserId()),
                        Images = model.Images
                    };
                    db.Events.Add(Event);
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Events", new { EventId = Event.Id });
                }
            }

            return View();
        }

        [Route("{EventId}/Edit")]
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int EventId)
        {
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                Event Event = db.Events.Find(EventId);
                if (Event.Host.Id != User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", "Home");
                }
                CreateUpdateEventViewModel CreateUpdateEventViewModel = new CreateUpdateEventViewModel()
                {
                    Name = Event.Name,
                    Description = Event.Description,
                    Location = Event.Location,
                    Datetime = Event.Datetime,
                    Limit = Event.Limit,
                    Images = Event.Images
                };
                return View(CreateUpdateEventViewModel);
            }
        }

        [Route("{EventId}/Edit")]
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int EventId, CreateUpdateEventViewModel CreateUpdateEventViewModel)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
                {

                    Event Event = db.Events.Find(EventId);
                    if (Event == null)
                    {
                        return HttpNotFound();
                    }
                    Event.Name = CreateUpdateEventViewModel.Name;
                    Event.Description = CreateUpdateEventViewModel.Description;
                    Event.Location = CreateUpdateEventViewModel.Location;
                    Event.Datetime = CreateUpdateEventViewModel.Datetime;
                    Event.Limit = CreateUpdateEventViewModel.Limit;
                    Event.Images = CreateUpdateEventViewModel.Images;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Edit", "Events", new { EventId = EventId });
        }


        [Route("{EventId}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Detail(int EventId)
        {
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                Event Event = db.Events.Find(EventId);
                EventDetailViewModel EventDetailViewModel = new EventDetailViewModel()
                {
                    Id = Event.Id,
                    Name = Event.Name,
                    Description = Event.Description,
                    Location = Event.Location,
                    Datetime = Event.Datetime,
                    Limit = Event.Limit,
                    Host = Event.Host,
                    AttendeeCount = Event.Attendees.Count,
                    SpacesLeft = Event.Limit - Event.Attendees.Count,
                    Categories = Event.Categories,
                    Images = Imgur.Instance.GetAlbumImages(Event.Images)
                };
                bool going = false;
                ApplicationUser user = new ApplicationUser();
                if (User.Identity.IsAuthenticated)
                {
                    user = db.Users.Find(User.Identity.GetUserId());
                    if (user.Events.Where(e => e.Id == Event.Id).Any())
                    {
                        going = true;
                    }
                }
                EventDetailViewModel.Going = going;
                EventDetailViewModel.AuthenticatedUser = user;

                return View(EventDetailViewModel);
            }
        }

        [Route("AttendEvent")]
        [HttpGet]
        [Authorize]
        public ActionResult AttendEvent(int eventId, string userId)
        {
            Event anEvent = null;
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                anEvent = db.Events.Find(eventId);
                ApplicationUser user = db.Users.Find(userId);
                anEvent.Attendees.Add(user);
                user.Events.Add(anEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Detail", "Events", new { EventId = anEvent.Id });
        }

        [Route("UnattendEvent")]
        [HttpGet]
        [Authorize]
        public ActionResult UnattendEvent(int eventId, string userId)
        {
            Event anEvent = null;
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                anEvent = db.Events.Find(eventId);
                ApplicationUser user = db.Users.Find(userId);
                anEvent.Attendees.Remove(user);
                user.Events.Remove(anEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Detail", "Events", new { EventId = anEvent.Id });
        }
    }
}

