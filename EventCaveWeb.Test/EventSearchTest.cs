using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCaveWeb.Controllers;
using EventCaveWeb.ViewModels;
using EventCaveWeb.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EventCaveWeb.Test
{
    [TestClass]
    public class EventSearchTest
    {
        [TestMethod]
        public void LocationSearchPositiveTestSingleResult()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Location = "Birmingham, United Kingdom" },
                new Event() { Location = "Manchester, United Kingdom" }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), "Birmingham", null, null);

            // Assert
            Assert.AreEqual(1, resultEvents.Count, string.Format("Should return 1 event from the search. Actual result: {0}", resultEvents.Count));
        }

        [TestMethod]
        public void LocationSearchPositiveTestMultipleResults()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Location = "Birmingham, United Kingdom" },
                new Event() { Location = "Manchester, United Kingdom" }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), "United Kingdom", null, null);

            // Assert
            Assert.AreEqual(2, resultEvents.Count, string.Format("Should return 2 events from the search. Actual result: {0}", resultEvents.Count));
        }

        [TestMethod]
        public void LocationSearchNegativeTest()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Location = "Birmingham, United Kingdom" },
                new Event() { Location = "Hamburg, Germany" }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), "Berlin", null, null);

            // Assert
            Assert.AreEqual(0, resultEvents.Count, string.Format("Should return 0 events from the search. Actual result: {0}", resultEvents.Count));
        }

        [TestMethod]
        public void KeywordSearchPositiveTest()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Name = "Ladies night" },
                new Event() { Name = "Bachelor party" },
                new Event() { Name = "Jason's goodbye party" }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), null, "Ladies night", null);

            // Assert
            Assert.AreEqual(1, resultEvents.Count, string.Format("Should return 1 event from the search. Actual result: {0}", resultEvents.Count));
        }

        [TestMethod]
        public void KeywordSearchPositiveTestLowercase()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Name = "LAdieS NIght" },
                new Event() { Name = "Jason's goodbye party" }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), null, "ladies night", null);

            // Assert
            Assert.AreEqual(1, resultEvents.Count, string.Format("Should return 1 event from the search. Actual result: {0}", resultEvents.Count));
        }

        [TestMethod]
        public void KeywordSearchNegativeTest()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Name = "Ladies night" },
                new Event() { Name = "Jason's goodbye party" }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), null, "Bachelor party", null);

            // Assert
            Assert.AreEqual(0, resultEvents.Count, string.Format("Should return 0 event from the search. Actual result: {0}", resultEvents.Count));
        }

        [TestMethod]
        public void CategorySearchPositiveTest()
        {
            // Arrange
            EventsController eventsController = new EventsController();
            ICollection<Event> events = new List<Event>()
            {
                new Event() { Categories = new List<Category>() { new Category() { Id = 1, Name = "Drinks" } } },
                new Event() { Categories = new List<Category>() { new Category() { Id = 2, Name = "Knitting" } } },
                new Event() { Categories = new List<Category>() { new Category() { Id = 3, Name = "Board games" } } }
            };
            List<Event> resultEvents = new List<Event>();

            // Act 
            resultEvents = eventsController.FilterEvents(events.AsEnumerable(), null, null, new List<int>() { 2 });

            // Assert
            Assert.AreEqual(1, resultEvents.Count, string.Format("Should return 1 event with a matching category from the search. Actual result: {0}", resultEvents.Count));
        }
    }
}
