using EventCaveWeb.Database;
using EventCaveWeb.Models;
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
            TicketListingViewModel model = new TicketListingViewModel()
            {
                Tickets = db.Tickets.ToList()
            };
            return View(model);
        }

        [Route("Tickets/{id}")]
        [HttpGet]
        [Authorize]
        public ActionResult Detail(int id)
        {
            DatabaseContext db = HttpContext.GetOwinContext().Get<DatabaseContext>();
            Ticket ticket = db.Tickets.Find(id);
            DetailTicketViewModel model = new DetailTicketViewModel()
            {
                Subject = ticket.Subject,
                Message = ticket.Message,
                Response = ticket.Response,
                Resolved = ticket.Resolved,
                SubmittedAt = ticket.SubmittedAt
            };
            return View(model);
        }
    }
}