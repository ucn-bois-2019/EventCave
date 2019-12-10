using EventCaveWeb.Database;
using EventCaveWeb.Entities;
using EventCaveWeb.Utils;
using EventCaveWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventCaveWeb.Controllers
{
    public class TicketsController : Controller
    {
        [Route("Tickets/Create")]
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateUpdateTicketViewModel());
        }
        
        [Route("Tickets/Create")]
        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "Subject,Message")] CreateUpdateTicketViewModel model)
        {
            if(ModelState.IsValid)
            {
                DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
                Ticket ticket = new Ticket()
                {
                    Subject = model.Subject,
                    Message = model.Message,
                    Resolved = false,
                    SubmittedAt = DateTime.Now,
                    Sender = db.Users.Find(User.Identity.GetUserId())
                };
                db.Tickets.Add(ticket);
                db.SaveChanges();
                Message.Create(Response, "Ticket was successfully created.");
            } else
            {
                Message.Create(Response, "There was a problem with creating your ticket. Check required fields in the form.");
            }
            return RedirectToAction("List", "Tickets");
        }

        [Route("Tickets")]
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            string userId = User.Identity.GetUserId();
            TicketListingViewModel model = new TicketListingViewModel()
            {
                ResolvedTickets = db.Tickets.Where(t => t.Sender.Id == userId && t.Resolved == true).ToList(),
                PendingTickets = db.Tickets.Where(t => t.Sender.Id == userId && t.Resolved == false).ToList()
            };
            return View(model);
        }
    }
}