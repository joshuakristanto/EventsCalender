using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using EventCalendar.Classes;
using EventCalendar.Interfaces;
using EventCalendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Features;

namespace EventCalendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {

        private CalendarEventData _eventData;
        private ICalendarEvent _calendarDb;
       
        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger, CalendarEventData eventData, ICalendarEvent calendarDb)
        {
            _logger = logger;
            _eventData = eventData;
            _calendarDb = calendarDb;
        }

    


        //[Authorize]
        [Route("Month")]
        [HttpGet]
        public IEnumerable GetMonthEvents( int month, int year)
        {
           
            /*using (var db = _eventData)
            {
                var localEvent = db.Events;
                var localEventContent = db.EventsContents;
                //Switch Include to Select. 

                // var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Include(c=>c.EventsContents).ToList();
                  var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).SelectMany(c => c.Items, (c,i) => new { c.Created,i.Title } ).ToList();
              //  var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Select(c => new { c.Created, EventContents =  c.Items.Select(k => k.Title)}).ToList();
                // var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).ToList();
                foreach (var events in localEventComplete)
                {
                    Console.WriteLine("Order: order.Created");
                    
                    //var comm =localComment.Find(events.EventsContents);
                    

                    yield return events ;
                }

            }
            */

            return _calendarDb.GetMonthEvents(month, year);



        }


     //   [Authorize]
        [Route("MonthComments")]
        [HttpGet]
        public IEnumerable GetMonthEventContents(int month, int year)
        {
            using (var db = _eventData)
            {
                var localEvent = db.Events;
                var localEventContent = db.EventsContents;
                //Switch Include to Select. 

                //  var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Include(c=>c.EventsContents).ToList();
                var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).SelectMany(c => c.Items, (c, i) => new { c.Created, i.Title ,i.Comment}).ToList();
                // var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).ToList();
                foreach (var events in localEventComplete)
                {
                    Console.WriteLine("Order: order.Created");

                    //var comm =localComment.Find(events.EventsContents);


                    yield return events;
                }

            }

        }


       // [Authorize]
        [Route("Day")]
        [HttpGet]
        public IEnumerable GetMonthDayEvents( DateTime date)
        {
            var db = _eventData;


            // var results = db.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).SelectMany(c => c.Items, (c,i) => new { c.Created, i.Title, i.Comment, i.Id });
            var results = db.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) });
            return results;
        }
        // [Authorize]
        [Route("Test")]
        [HttpGet]
        public IEnumerable Test(DateTime date)
        {
            var db = _eventData;
                
           var results = db.EventsContents.Where(c => c.Events.Created == date);
           
            // var results = db.Events.Where(c => c.Created == date);

            //   var results2 = db.Events.Single(c => c.Created == date.);
            return results;
        }
        //[Authorize]
        [Route("Add")]
        [HttpPost]
        public IActionResult AddEvent(DateTime date, string title, string comment)
        {
            using (var db = _eventData)
            {
                var ticks = new DateTime(2016, 1, 1).Ticks;
                var ans = DateTime.Now.Ticks - ticks;
                var uniqueId = ans.ToString("x");


                

               
                var day = db.Events;
                ICollection<EventsContents>[] dbContent = day.Where(c => c.Created == date).Select(c => c.Items).ToArray();
                Events [] eventLocal = day.Where(c => c.Created == date).ToArray();
              
                if (eventLocal.Length >0)
                {
                    var eventContents = new EventsContents()
                    {
                        Id = uniqueId,
                        //   CommentCreated = date,
                        Comment = comment,
                        Title = title,
                        EventId = uniqueId,
                        Events = eventLocal[0],

                    };
                    dbContent[0].Add(eventContents);
                    eventLocal[0].Items = dbContent[0];

                    db.Events.Update(eventLocal[0]);
                    db.EventsContents.Add(eventContents);
                }
                else
                {
                    var events = new Events()
                    {

                        Created = date,
                        Year = date.Year,
                        Month = date.Month,
                        Day = date.Day,
                        Items = new List<EventsContents>(),
                        EventId = uniqueId,

                    }; 
                    var eventContents = new EventsContents()
                    {
                        Id = uniqueId,
                        //   CommentCreated = date,
                        Comment = comment,
                        Title = title,
                        EventId = uniqueId,
                        Events = events,

                    };
                    events.Items.Add(eventContents);
                    db.Events.AddAsync(events);
                    db.EventsContents.Add(eventContents);

                }
               
                // events.id.Add(uniqueId);
               
              
              //  db.Events.AddAsync(events);
              db.SaveChanges();


            };
            

           // var db = new CalendarEventData();

           return Ok();
        }
       // [Authorize]
        [Route("Delete")]
        [HttpPost]
        public IActionResult DeleteEvent(DateTime date)
        {
            using (var db = _eventData)
            {

                var results = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).ToArray();
                var resultsEventContents = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).Select(c =>c.Items).ToArray();
                /*
                var resultsEventContent = db.EventsContents.Where(p =>
                    p.CommentCreated.Value.Date.Day == date.Day && p.CommentCreated.Value.Date.Month == date.Month &&
                    p.CommentCreated.Value.Date.Year == date.Year).ToArray();
                */
                //var eventContents = resultsEventContent[0];
                var events = results[0];
                //  db.EventsContents.Remove(eventContents);

                foreach (var item in resultsEventContents[0])
                {
                    db.EventsContents.Remove(item);
                }

                db.Events.Remove(events);
                db.SaveChanges();


            };



            // var db = new CalendarEventData();


            return Ok();
        }


        // [Authorize]
        [Route("DeleteItem")]
        [HttpPost]
        public async Task<IActionResult> DeleteEventItem(DateTime date, string id)
        {
            using (var db = _eventData)
            {

                var results = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).ToArray();
                var resultsEventContents = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).Select(c => c.Items).ToArray();
                /*
                var resultsEventContent = db.EventsContents.Where(p =>
                    p.CommentCreated.Value.Date.Day == date.Day && p.CommentCreated.Value.Date.Month == date.Month &&
                    p.CommentCreated.Value.Date.Year == date.Year).ToArray();
                */
                //var eventContents = resultsEventContent[0];
                var events = results[0];
                //  db.EventsContents.Remove(eventContents);

                foreach (var item in resultsEventContents[0])
                {
                    if (item.Id == id)
                    {
                        db.EventsContents.Remove(item);
                      //  resultsEventContents[0].Remove(item);
                    }
                   // db.EventsContents.Remove(item);
                }

                events.Items = resultsEventContents[0];
               // db.Events.Update(events);
                //db.Events.Remove(events);
                
                db.SaveChanges();


            };



            // var db = new CalendarEventData();


            return Ok();
        }

        [Route("EditItem")]
        [HttpPost]
        public async Task<IActionResult> EditEventItem(DateTime date, string id, string title, string comment)
        {
            using (var db = _eventData)
            {

                var results = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).ToArray();
                var resultsEventContents = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).Select(c => c.Items).ToArray();
              
                var events = results[0];

                

                foreach (var item in resultsEventContents[0])
                {

                    if (item.Id == id)
                    {
                        var eventContents = db.EventsContents.Find(item.Id);
                        eventContents.Comment = comment;
                        eventContents.Title = title;
                    }
                }

                events.Items = resultsEventContents[0];
              

                db.SaveChanges();


            };



            // var db = new CalendarEventData();


            return Ok();
        }
        [Authorize]
        [HttpGet]
        [Route("CheckLoginState")]

        public async Task<IActionResult> LoginState()
        {



            return Ok();

        }

    }
}
