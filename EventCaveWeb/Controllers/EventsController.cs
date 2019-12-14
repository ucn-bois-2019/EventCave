using EventCaveWeb.Database;
using EventCaveWeb.Entities;
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
using System.Data.Entity;

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
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            var events = db.Events.Include("Categories").AsEnumerable();
            return View(FilterEvents(events, model.Location, model.Keyword, model.DateTime, model.SelectedCategoryIds));
        }

        public List<Event> FilterEvents(IEnumerable<Event> events, string location, string keyword, DateTime datetime, IEnumerable<int> categoryIds)
        {
            if (location != null)
            {
                events = events.Where(e => e.Location.ToLower().Contains(location.ToLower()));
            }
            if (keyword != null)
            {
                events = events.Where(e => e.Name.ToLower().Contains(keyword.ToLower()));
            }
            if (categoryIds != null && categoryIds.Any())
            {
                events = events.Where(e => e.Categories.Any(c => categoryIds.Contains(c.Id)));
            }
            if (datetime.Date.ToShortDateString() != "1/1/0001")
            {
                events = events.Where(e => datetime.Date == e.Datetime.Date);
            }
            return events.ToList();
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
            else
            {
                DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
                return View(new CreateUpdateEventViewModel
                {
                    Datetime = DateTime.Now,
                    Categories = db.Categories.ToList()
                });
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
                    HostPicture = Imgur.Instance.GetImage(@event.Host.Picture),
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
            EventEnrollmentResult result = UserEnroll(id, User.Identity.GetUserId());

            if (result == EventEnrollmentResult.Success)
            {
                Message.Create(Response, "You were successfully enrolled");
            }
            else if (result == EventEnrollmentResult.NotEnoughPlaces)
            {
                Message.Create(Response, "No more places left at this event");
            }
            else
            {
                Message.Create(Response, "Something went wrong");
            }

            return RedirectToAction("Detail", "Events", new { id });
        }

        [Route("{id}/Unattend")]
        [HttpGet]
        [Authorize]
        public ActionResult Unattend(int id)
        {
            EventEnrollmentResult result = UserEnrollRevert(id, User.Identity.GetUserId());

            if (result == EventEnrollmentResult.Success)
            {
                Message.Create(Response, "Your enrollment was reverted");
            }
            else
            {
                Message.Create(Response, "Something went wrong");
            }

            return RedirectToAction("Detail", "Events", new { id });
        }

        public enum EventEnrollmentResult
        {
            Success, UnknownError, NotEnoughPlaces
        }

        public EventEnrollmentResult UserEnroll(object eventId, object userId)
        {
            using (var db = new DatabaseContext())
            {
                Event @event = db.Events.Find(eventId);
                ApplicationUser user = db.Users.Find(userId);
                if (@event == null || user == null)
                {
                    return EventEnrollmentResult.UnknownError;
                }

                if (!CheckAvailablePlaces(@event))
                {
                    return EventEnrollmentResult.NotEnoughPlaces;
                }

                UserEvent userEvent = new UserEvent()
                {
                    ApplicationUserId = user.Id,
                    User = user,
                    EventId = @event.Id,
                    Event = @event
                };

                db.UserEvents.Add(userEvent);
                db.SaveChanges();
            }

            return EventEnrollmentResult.Success;
        }

        public EventEnrollmentResult UserEnrollRevert(object eventId, object userId)
        {
            using (var db = new DatabaseContext())
            {
                UserEvent userEvent = db.UserEvents.Find(userId, eventId);

                if (userEvent == null)
                {
                    return EventEnrollmentResult.UnknownError;
                }

                db.UserEvents.Remove(userEvent);
                db.SaveChanges();
            }

            return EventEnrollmentResult.Success;
        }

        public bool CheckAvailablePlaces(Event @event)
        {
            bool result = false;

            if (@event.Limit > @event.Attendees.Count || @event.Limit == 0)
            {
                result = true;
            }

            return result;
        }
    }
}

