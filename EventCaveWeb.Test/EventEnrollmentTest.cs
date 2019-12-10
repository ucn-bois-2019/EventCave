using EventCaveWeb.Controllers;
using EventCaveWeb.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EventCaveWeb.Test
{
    [TestClass]
    public class EventEnrollmentTest
    {
        [TestMethod]
        public void UserEnrollmentToEventPositiveTest()
        {
            // Arrange
            bool areSpacesAvailable = false;
            EventsController controller = new EventsController();
            Event @event = new Event()
            {
                Attendees = new List<UserEvent>(),
                Limit = 10
            };

            // Act
            areSpacesAvailable = controller.CheckAvailablePlaces(@event);

            // Assert
            Assert.IsTrue(areSpacesAvailable, "The method should return true, because there are 10 spaces available");
        }

        [TestMethod]
        public void UserEnrollmentNegativeTest()
        {
            // Arrange
            bool areSpacesAvailable = false;
            EventsController controller = new EventsController();
            Event @event = new Event()
            {
                Attendees = new List<UserEvent>() { new UserEvent(), new UserEvent(), new UserEvent() },
                Limit = 3
            };

            // Act
            areSpacesAvailable = controller.CheckAvailablePlaces(@event);

            // Assert
            Assert.IsFalse(areSpacesAvailable, "The method should return false, because there are 0 spaces available");
        }

        [TestMethod]
        public void UserEnrollmentNegativeTestWithZeroLimit()
        {
            // Arrange
            bool areSpacesAvailable = false;
            EventsController controller = new EventsController();
            Event @event = new Event()
            {
                Attendees = new List<UserEvent>(),
                Limit = 0
            };

            // Act
            areSpacesAvailable = controller.CheckAvailablePlaces(@event);

            // Assert
            Assert.IsTrue(areSpacesAvailable, "The method should return false, there are unlimited spaces available");
        }
    }
}
