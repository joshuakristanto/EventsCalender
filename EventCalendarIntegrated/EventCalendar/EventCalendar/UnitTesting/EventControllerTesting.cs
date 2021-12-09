using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EventCalendar.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCalendar.Controllers;
using EventCalendar.Identity;
using EventCalendar.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;

namespace EventCalendar.UnitTesting
{
    [TestClass]
    public class EventControllerTesting
    {
        private TestContext testContextInstance;

        // private Mock<ILogger> _logger = new Mock<ILogger>();
        //   private Mock<IConfiguration> _configuration = new Mock<IConfiguration>();


        // private CalendarEventData eventData =
        //     new CalendarEventData(options => options.UseSqlite("ConnectionStrings:EventDatabase"));

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void AddEventTest()
        {
            
            var  optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>().UseInMemoryDatabase(databaseName:"EventCalendar").Options;

            // optionsBuilder.UseSqlite("ConnectionStrings:EventDatabase");

            /*var mock = new Mock<ILogger<EventsController>>();
            ILogger<EventsController> logger = mock.Object;
            var controller = new EventsController(logger, eventData );
           var results =  controller.GetMonthEventContents(11, 2021);
            //var result = controller.Details(2) as ViewResult;
            */
            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            DateTime date = (DateTime.Parse("2021-11-30T08:00:00"));
          //  var results = eventClass.GetMonthDayEvents(DateTime.Parse("2021-11-30T08:00:00")).GetEnumerator().Current;
            var results = eventData.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) }).ToList();

            TestContext.WriteLine("Message..." + results[0].Created);
            Assert.AreEqual(date, results[0].Created);
            

        }

        [TestMethod]
        public void UpdateEventTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>().UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            DateTime date = DateTime.Parse("2021-11-30T08:00:00");
            var results = eventData.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) }).ToList();
            var id = results[0].items.Select(c => new{c.Id, c.Title, c.Comment}).ToList();
            eventClass.EditEventItem(date, id[0].Id,"NewTitle","NewComment");
           
            //Update Variable Searches.
            results = eventData.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) }).ToList();
            id = results[0].items.Select(c => new { c.Id, c.Title, c.Comment }).ToList();
         
            var results2 = results[0].items.Where(c => c.Id == id[0].Id).Select(c => c.Comment).ToList();
            //TestContext.WriteLine("Message..." + results2[0]);
            Assert.AreEqual("NewComment", results2[0]);


        }


        [TestMethod]
        public void DeleteEventTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>().UseInMemoryDatabase(databaseName: "EventCalendar").Options;
            
            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title2", "Comment2");
            DateTime date = (DateTime.Parse("2021-11-30T08:00:00"));
            eventClass.DeleteEvent(date);
            var results = eventData.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) }).ToList();


            TestContext.WriteLine("Message..." + results.Count);
            Assert.AreEqual(0, results.Count);


        }


        [TestMethod]
        public void DeleteSingleEventContentTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>().UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title2", "Comment2");
            DateTime date = (DateTime.Parse("2021-11-30T08:00:00"));

            var results = eventData.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) }).ToList();
            var id = results[0].items.Select(c => new { c.Id, c.Title, c.Comment }).ToList();

            eventClass.DeleteEventItem(date, id[0].Id);

            TestContext.WriteLine("Message..." + results.Count);
            Assert.AreEqual(1, results.Count);


        }

    }
}
