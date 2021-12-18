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
using EventCalendar.Interfaces;
using EventCalendar.ViewModels;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

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
          
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

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
            List<AddEventTestModel> results = eventData.Events
                .Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                            p.Created.Value.Date.Year == date.Year)
                .Select(c => new AddEventTestModel
                {
                    Created = c.Created.Value, Items = c.Items
                        .Select(c => new ItemsModel {Title = c.Title, Comment = c.Comment, Id = c.Id})
                }).ToList();

            TestContext.WriteLine("Message..." + results[0].Created);
            Assert.AreEqual(date, results[0].Created);


        }

        [TestMethod]
        public void UpdateEventTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);



            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            DateTime date = DateTime.Parse("2021-11-30T08:00:00");


            List<AddEventTestModel> results = eventData.Events
                .Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                            p.Created.Value.Date.Year == date.Year)
                .Select(c => new AddEventTestModel
                {
                    Created = c.Created.Value,
                    Items = c.Items
                        .Select(c => new ItemsModel {Title = c.Title, Comment = c.Comment, Id = c.Id})
                }).ToList();



            List<ItemsModel> id = results[0].Items
                .Select(c => new ItemsModel() {Id = c.Id, Title = c.Title, Comment = c.Comment})
                .ToList();




            eventClass.EditEventItem(date, id[0].Id, "NewTitle", "NewComment");



            //Update Variable Searches.
            results = eventData.Events
                .Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                            p.Created.Value.Date.Year == date.Year)
                .Select(c => new AddEventTestModel
                {
                    Created = c.Created.Value,
                    Items = c.Items
                        .Select(c => new ItemsModel {Title = c.Title, Comment = c.Comment, Id = c.Id})
                }).ToList();


            id = results[0].Items
                .Select(c => new ItemsModel() {Id = c.Id, Title = c.Title, Comment = c.Comment})
                .ToList();



            var results2 = results[0].Items.Where(c => c.Id == id[0].Id).Select(c => c.Comment).ToList();
            //TestContext.WriteLine("Message..." + results2[0]);
            Assert.AreEqual("NewComment", results2[0]);


        }


        [TestMethod]
        public void DeleteEventTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);



            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title2", "Comment2");
            DateTime date = (DateTime.Parse("2021-11-30T08:00:00"));
            eventClass.DeleteEvent(date);

            List<AddEventTestModel> results = eventData.Events
                .Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                            p.Created.Value.Date.Year == date.Year)
                .Select(c => new AddEventTestModel
                {
                    Created = c.Created.Value,
                    Items = c.Items
                        .Select(c => new ItemsModel {Title = c.Title, Comment = c.Comment, Id = c.Id})
                }).ToList();


            TestContext.WriteLine("Message..." + results.Count);
            Assert.AreEqual(0, results.Count);


        }


        [TestMethod]
        public void DeleteSingleEventContentTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);



            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title2", "Comment2");
            DateTime date = (DateTime.Parse("2021-11-30T08:00:00"));

            List<AddEventTestModel> results = eventData.Events
                .Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                            p.Created.Value.Date.Year == date.Year)
                .Select(c => new AddEventTestModel
                {
                    Created = c.Created.Value,
                    Items = c.Items
                        .Select(c => new ItemsModel {Title = c.Title, Comment = c.Comment, Id = c.Id})
                }).ToList();


            var id = results[0].Items
                .Select(c => new ItemsModel() {Id = c.Id, Title = c.Title, Comment = c.Comment})
                .ToList();


            eventClass.DeleteEventItem(date, id[0].Id);

            results = eventData.Events
                .Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                            p.Created.Value.Date.Year == date.Year)
                .Select(c => new AddEventTestModel
                {
                    Created = c.Created.Value,
                    Items = c.Items
                        .Select(c => new ItemsModel {Title = c.Title, Comment = c.Comment, Id = c.Id})
                }).ToList();



            id = results[0].Items
                .Select(c => new ItemsModel() {Id = c.Id, Title = c.Title, Comment = c.Comment})
                .ToList();

            TestContext.WriteLine("Message..." + results.Count);
            Assert.AreEqual(1, results.Count);


        }

        [TestMethod]
        public void GetMonthTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);


            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-27T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-28T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-23T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-21T08:00:00"), "Title2", "Comment2");

            IEnumerable<GetMonthEventsModel> monthEvents = eventClass.GetMonthEvents(11, 2021);


            List<IEnumerable<GetMonthEventsModel>> localEventComplete = eventData.Events
                .Where(c => c.Month == 11 && c.Year == 2021)
                .Select(c => c.Items
                    .Select(k => new GetMonthEventsModel {Created = c.Created.Value, Title = k.Title}))
                .ToList();

            int i = 0;
            foreach (var events in localEventComplete)
            {

                //var comm =localComment.Find(events.EventsContents);


                foreach (var localResults in events)
                {
                    TestContext.WriteLine("Message..." + monthEvents.ToList()[i].Title);
                    TestContext.WriteLine("Message..." + localResults.Title);
                    TestContext.WriteLine("Message..." + monthEvents.ToList()[i].Created);
                    TestContext.WriteLine("Message..." + localResults.Created);
                    TestContext.WriteLine("Message..." + i);
                    Assert.AreEqual(monthEvents.ToList()[i].Title, localResults.Title);
                    Assert.AreEqual(monthEvents.ToList()[i].Created, localResults.Created);
                    i++;
                }


            }

        }


        [TestMethod]
        public void GetMonthContentTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);


            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-27T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-28T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-23T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-21T08:00:00"), "Title2", "Comment2");

            IEnumerable<GetMonthEventContentModel> monthEvents = eventClass.GetMonthEventContents(11, 2021);




            List<IEnumerable<GetMonthEventContentModel>> localEventComplete = eventData.Events
                .Where(c => c.Month == 11 && c.Year == 2021)
                .Select(c => c.Items.Select(k => new GetMonthEventContentModel
                    {Created = c.Created.Value, Title = k.Title, Comment = k.Comment})).ToList();



            int i = 0;
            foreach (var events in localEventComplete)
            {

                //var comm =localComment.Find(events.EventsContents);


                foreach (var localResults in events)
                {
                    if (i >= monthEvents.ToList().Count)
                    {
                        break;
                    }

                    TestContext.WriteLine("Message..." + monthEvents.ToList()[i].Title);
                    TestContext.WriteLine("Message..." + localResults.Title);
                    TestContext.WriteLine("Message..." + monthEvents.ToList()[i].Created);
                    TestContext.WriteLine("Message..." + localResults.Created);
                    TestContext.WriteLine("Message..." + monthEvents.ToList()[i].Comment);
                    TestContext.WriteLine("Message..." + localResults.Comment);
                    TestContext.WriteLine("Message..." + i);
                    Assert.AreEqual(monthEvents.ToList()[i].Title, localResults.Title);
                    Assert.AreEqual(monthEvents.ToList()[i].Created, localResults.Created);
                    Assert.AreEqual(monthEvents.ToList()[i].Comment, localResults.Comment);
                    i++;
                }


            }

        }

        [TestMethod]
        public void GetEventContent()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>()
                .UseInMemoryDatabase(databaseName: "EventCalendar").Options;

            CalendarEventData eventData = new CalendarEventData(optionsBuilder);
            CalenderEventClass eventClass = new CalenderEventClass(eventData);


            eventClass.AddEvent(DateTime.Parse("2021-11-30T08:00:00"), "Title", "Comment");
            eventClass.AddEvent(DateTime.Parse("2021-11-27T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-28T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-23T08:00:00"), "Title2", "Comment2");
            eventClass.AddEvent(DateTime.Parse("2021-11-21T08:00:00"), "Title2", "Comment2");

            List<EventContentTestModel> eventIDs = eventData.EventsContents.Select(c => new EventContentTestModel(){ EventId = c.Id,Title= c.Title}).ToList();

            foreach (var items in eventIDs)
            {
                List<GetEventContentModel> resultsEventContents = eventData.EventsContents
                    .Where(c => c.Id == items.EventId)
                    .Select(d => new GetEventContentModel { Title = d.Title, Comment = d.Comment })
                    .ToList();
                foreach (var item in resultsEventContents)
                {
                    Assert.AreEqual(item.Title, items.Title);
                }

            }

            
            


          
        }
    }
}
