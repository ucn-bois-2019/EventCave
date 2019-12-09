using Core.Database;
using Core.Models;

namespace Core.Controllers
{
    public class EventController
    {
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
