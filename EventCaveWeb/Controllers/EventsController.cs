using EventCaveWeb.Database;
using EventCaveWeb.Models;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
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
        [Route("Search")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search([Bind(Include = "Keyword,Location,DateTime,SelectedCategoryIds")] HomeViewModel model)
        {
            // TODO: MySQL does not support DBFunctions to truncate time and EF6 does not support Datetime type.
            // Figure out or we are done. 
            // https://stackoverflow.com/questions/7016765/currentutcdatetime-does-not-exist-entity-framework-and-mysql

            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            var events = db.Events.Include("Categories").AsQueryable();
            if (model.Location != null)
            {
                events = events.Where(e => e.Location.Equals(model.Location));
            }
            if (model.Keyword != null)
            {
                events = events.Where(e => e.Name.Equals(model.Keyword));
            }
            if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
            {
                events = events.Where(e => e.Categories.Any(c => model.SelectedCategoryIds.Contains(c.Id)));
            }
            return View(events.ToList());   
        }

        [Route("Create")]
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            return View(new CreateUpdateEventViewModel
            {
                Datetime = DateTime.Now,
                Categories = db.Categories.ToList()
            });
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
                    Event @event = new Event
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Location = model.Location,
                        Datetime = model.Datetime,
                        Limit = model.Limit,
                        Categories = model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any()
                                     ? db.Categories.Where(c => model.SelectedCategoryIds.Contains(c.Id)).ToList()
                                     : new List<Category>(),
                        CreatedAt = DateTime.Now,
                        Host = db.Users.Find(User.Identity.GetUserId()),
                        Images = model.Images
                    };
                    db.Events.Add(@event);
                    db.SaveChanges();
                    Message.Create(Response, "Event was successfully created.");
                    return RedirectToAction("Detail", "Events", new { id = @event.Id });
                }
            }
            return View();
        }

        [Route("{id}/Edit")]
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                Event @event = db.Events.Find(id);
                if (@event.Host.Id != User.Identity.GetUserId())
                {
                    return RedirectToAction("Index", "Home");
                }
                CreateUpdateEventViewModel CreateUpdateEventViewModel = new CreateUpdateEventViewModel()
                {
                    Id = @event.Id,
                    Name = @event.Name,
                    Description = @event.Description,
                    Location = @event.Location,
                    Datetime = @event.Datetime,
                    Limit = @event.Limit,
                    Categories = db.Categories.ToList(),
                    SelectedCategoryIds = @event.Categories.ToList().Select(c => c.Id),
                    Images = @event.Images
                };
                return View(CreateUpdateEventViewModel);
            }
        }

        [Route("{id}/Edit")]
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, CreateUpdateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
                {
                    Event Event = db.Events.Include("Categories").First(e => e.Id == id);
                    if (Event == null)
                    {
                        return HttpNotFound();
                    }
                    Event.Name = model.Name;
                    Event.Description = model.Description;
                    Event.Location = model.Location;
                    Event.Categories = model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any()
                                       ? db.Categories.Where(c => model.SelectedCategoryIds.Contains(c.Id)).ToList()
                                       : new List<Category>();
                    Event.Datetime = model.Datetime;
                    Event.Limit = model.Limit;
                    Event.Images = model.Images;
                    db.SaveChanges();
                    Message.Create(Response, "Event was successfully edited.");
                }
            }
            return RedirectToAction("Detail", "Events", new { id });
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Detail(int id)
        {
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                Event @event = db.Events.Find(id);
                EventDetailViewModel EventDetailViewModel = new EventDetailViewModel()
                {
                    Id = @event.Id,
                    Name = @event.Name,
                    Description = @event.Description,
                    Location = @event.Location,
                    Datetime = @event.Datetime,
                    Limit = @event.Limit,
                    Host = @event.Host,
                    AttendeeCount = @event.Attendees.Count,
                    SpacesLeft = @event.Limit - @event.Attendees.Count,
                    Categories = @event.Categories,
                    Images = Imgur.Instance.GetAlbumImages(@event.Images)
                };
                bool going = false;
                if (User.Identity.IsAuthenticated)
                {
                    ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                    if (user.EventsEnrolledIn.Where(e => e.EventId == @event.Id).Any())
                    {
                        going = true;
                    }
                }
                EventDetailViewModel.Going = going;
                return View(EventDetailViewModel);
            }
        }

        [Route("{id}/Attend")]
        [HttpGet]
        [Authorize]
        public ActionResult Attend(int id)
        {
            Event @event = null;
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                @event = db.Events.Find(id);
                ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                UserEvent userEvent = new UserEvent()
                {
                    Event = @event,
                    EventId = @event.Id,
                    User = user,
                    ApplicationUserId = user.Id
                };

                @event.Attendees.Add(userEvent);
                user.EventsEnrolledIn.Add(userEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Detail", "Events", new { id = @event.Id });
        }

        [Route("{id}/unattend")]
        [HttpGet]
        [Authorize]
        public ActionResult Unattend(int id)
        {
            Event @event = null;
            using (DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>())
            {
                @event = db.Events.Find(id);
                ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                UserEvent userEvent = db.UserEvents.Where(ue => ue.EventId == @event.Id && ue.ApplicationUserId == user.Id).First();
                @event.Attendees.Remove(userEvent);
                user.EventsEnrolledIn.Remove(userEvent);
                db.SaveChanges();
            }
            return RedirectToAction("Detail", "Events", new { id = @event.Id });
        }
    }
}

