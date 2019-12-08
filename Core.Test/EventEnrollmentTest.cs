using Core.Controllers;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Test
{
    [TestClass]
    public class EventEnrollmentTest
    {
        [TestMethod]
        public void UserEnrollmentToEventPositiveTest()
        {
            // Arrange
            bool areSpacesAvailable = false;
            EventController controller = new EventController();
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
            EventController controller = new EventController();
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
    }
}
