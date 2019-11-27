using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class EventController
    {
        public enum EventEnrollmentResult
        {
            Success, UserError, EventError, UnknownError, UserNotFoundInAttendees, EventNotFoundInUsersEvents
        }

        public EventEnrollmentResult AddEventToUser(Event @event, User user)
        {
            int eventCount = user.EventsEnrolledIn.Count;
            if (@event == null)
            {
                return EventEnrollmentResult.EventError;
            }

            if (user == null)
            {
                return EventEnrollmentResult.UserError;
            }

            user.EventsEnrolledIn.Add(@event);

            if (eventCount == user.EventsEnrolledIn.Count - 1)
            {
                return EventEnrollmentResult.Success;
            }
            else
            {
                return EventEnrollmentResult.UnknownError;
            }
        }

        public EventEnrollmentResult UserEnroll(Event @event, User user)
        {
            int attendeeCount = @event.Attendees.Count;
            if (@event == null)
            {
                return EventEnrollmentResult.EventError;
            }

            if (user == null)
            {
                return EventEnrollmentResult.UserError;
            }

            @event.Attendees.Add(user);

            if (attendeeCount == @event.Attendees.Count - 1)
            {
                return EventEnrollmentResult.Success;
            }
            else
            {
                return EventEnrollmentResult.UnknownError;
            }
        }

        public EventEnrollmentResult RemoveEventFromUser(Event @event, User user)
        {
            int eventCount = user.EventsEnrolledIn.Count;
            bool removalResult = false;
            if (@event == null)
            {
                return EventEnrollmentResult.EventError;
            }

            if (user == null)
            {
                return EventEnrollmentResult.UserError;
            }

            removalResult = user.EventsEnrolledIn.Remove(@event);

            if (!removalResult)
            {
                return EventEnrollmentResult.EventNotFoundInUsersEvents;
            }

            if (eventCount == user.EventsEnrolledIn.Count + 1)
            {
                return EventEnrollmentResult.Success;
            }
            else
            {
                return EventEnrollmentResult.UnknownError;
            }
        }

        public EventEnrollmentResult UserEnrollRevert(Event @event, User user)
        {

            int attendeeCount = @event.Attendees.Count;
            bool removeResult = false;
            if (@event == null)
            {
                return EventEnrollmentResult.EventError;
            }

            if (user == null)
            {
                return EventEnrollmentResult.UserError;
            }

            removeResult = @event.Attendees.Remove(user);

            if (!removeResult)
            {
                return EventEnrollmentResult.UserNotFoundInAttendees;
            }

            if (attendeeCount == @event.Attendees.Count + 1)
            {
                return EventEnrollmentResult.Success;
            }
            else
            {
                return EventEnrollmentResult.UnknownError;
            }
        }
    }
}
