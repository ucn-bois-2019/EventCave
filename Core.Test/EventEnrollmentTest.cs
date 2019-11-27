using Core.Controllers;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static Core.Controllers.EventController;

namespace Core.Test
{
    [TestClass]
    public class EventEnrollmentTest
    {
        [TestMethod]
        public void UserEnrollmentToEventPositiveTest()
        {
            // Arrange
            int expectedAttendeeCount = 1;
            EventEnrollmentResult expectedEventEnrollmentResult;
            EventController controller = new EventController();
            User user = new User()
            {
                EventsEnrolledIn = new List<Event>(),
            };
            Event @event = new Event()
            {
                Attendees = new List<User>(),
            };

            // Act
            expectedEventEnrollmentResult = controller.UserEnroll(@event, user);

            // Assert
            Assert.AreEqual(expectedEventEnrollmentResult, EventEnrollmentResult.Success, "The method should return 'sucess'");
            Assert.AreEqual(expectedAttendeeCount, @event.Attendees.Count, "The attendee count on the event should increase by 1");
        }

        [TestMethod]
        public void UserEnrollmentRevertingPositiveTest()
        {
            // A
            int expectedAttendeeCount = 0;
            EventEnrollmentResult expectedEventEnrollmentResult = EventEnrollmentResult.UserNotFoundInAttendees;
            EventEnrollmentResult actualEventEnrollmentResult;
            EventController controller = new EventController();
            User user = new User()
            {
                EventsEnrolledIn = new List<Event>(),
            };
            Event @event = new Event()
            {
                Attendees = new List<User>(),
            };

            // A
            actualEventEnrollmentResult = controller.UserEnrollRevert(@event, user);

            // A
            Assert.AreEqual(expectedEventEnrollmentResult, actualEventEnrollmentResult, "Should throw an error because user is not enrolled in the event");
            Assert.AreEqual(expectedAttendeeCount, @event.Attendees.Count, "Attendee count should stay 0");
        }

        [TestMethod]
        public void EventAdditionToUserPositiveTest()
        {
            // Arrange
            int expectedEventEnrolledInCount = 1;
            EventEnrollmentResult expectedEventEnrollmentResult = EventEnrollmentResult.Success;
            EventEnrollmentResult actualEventEnrollmentResult;
            EventController controller = new EventController();
            User user = new User()
            {
                EventsEnrolledIn = new List<Event>(),
            };
            Event @event = new Event()
            {
                Attendees = new List<User>(),
            };

            // Act
            actualEventEnrollmentResult = controller.AddEventToUser(@event, user);

            // Assert
            Assert.AreEqual(expectedEventEnrollmentResult, actualEventEnrollmentResult, "Should 'succeed' adding the event to the user");
            Assert.AreEqual(expectedEventEnrolledInCount, user.EventsEnrolledIn.Count, "The 'events enrolled in' for the user should increase by 1");
        }

        [TestMethod]
        public void EventRemovalFromUserPositiveTest()
        {
            // Arrange
            int expectedEventEnrolledInCount = 0;
            EventEnrollmentResult expectedEventEnrollmentResult = EventEnrollmentResult.EventNotFoundInUsersEvents;
            EventEnrollmentResult actualEventEnrollmentResult;
            EventController controller = new EventController();
            User user = new User()
            {
                EventsEnrolledIn = new List<Event>(),
            };
            Event @event = new Event()
            {
                Attendees = new List<User>(),
            };

            // Act
            actualEventEnrollmentResult = controller.RemoveEventFromUser(@event, user);

            // Assert
            Assert.AreEqual(expectedEventEnrollmentResult, actualEventEnrollmentResult, "Should fail because user doesn't have the event in his 'EventsEnrolledIn'");
            Assert.AreEqual(expectedEventEnrolledInCount, user.EventsEnrolledIn.Count, "The 'events enrolled in' for the user should stay 0");
        }
    }
}
