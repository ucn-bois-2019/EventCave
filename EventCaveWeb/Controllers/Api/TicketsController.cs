using EventCaveWeb.Database;
using EventCaveWeb.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EventCaveWeb.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TicketsController : ApiController
    {
        [Route("Api/Tickets")]
        [HttpGet]
        public IQueryable<TicketDto> List()
        {
            DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>();
            IQueryable<TicketDto> tickets = db.Tickets.Include("Sender").Select(t =>
                new TicketDto()
                {
                    Id = t.Id,
                    Subject = t.Subject,
                    Message = t.Message,
                    Response = t.Response,
                    Resolved = t.Resolved,
                    SubmittedAt = t.SubmittedAt,
                    SenderId = t.Sender.Id,
                    SenderUsername = t.Sender.UserName
                });
            return tickets;
        }

        [Route("Api/Tickets/{id}")]
        [HttpGet]
        public TicketDto Single(int id)
        {
            DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>();
            TicketDto ticketDto = db.Tickets.Include("Sender").Where(t => t.Id == id).Select(t =>
                new TicketDto
                {
                    Id = t.Id,
                    Subject = t.Subject,
                    Message = t.Message,
                    Response = t.Response,
                    Resolved = t.Resolved,
                    SubmittedAt = t.SubmittedAt,
                    SenderId = t.Sender.Id,
                    SenderUsername = t.Sender.UserName
                }).FirstOrDefault();
            return ticketDto;
        }

        [Route("Api/Tickets/{id}/Resolve")]
        [HttpPost]
        public IHttpActionResult ResolveTicket(int id, [FromBody] TicketResolveDto dto)
        {
            DatabaseContext db = HttpContext.Current.GetOwinContext().Get<DatabaseContext>();
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return BadRequest(ModelState);
            }
            ticket.Response = dto.Response;
            ticket.Resolved = true;
            db.SaveChanges();
            return Ok(
                new
                {
                    Status = 200,
                    Message = "Ticket was marked as resolved."
                }
            );
        }
    }
}