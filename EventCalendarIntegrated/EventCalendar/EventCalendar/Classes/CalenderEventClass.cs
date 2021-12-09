using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCalendar.Interfaces;
using EventCalendar.Models;

namespace EventCalendar.Classes
{
    public class CalenderEventClass : ICalendarEvent
    {
        private readonly CalendarEventData db;
        public CalenderEventClass(CalendarEventData dbContext)
        {
            db = dbContext;
        }
        public IEnumerable GetMonthEvents(int month, int year)
        {
            //  throw new NotImplementedException();
            var localEvent = db.Events;
            var localEventContent = db.EventsContents;
            //Switch Include to Select. 

            // var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Include(c=>c.EventsContents).ToList();
            var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).SelectMany(c => c.Items, (c, i) => new { c.Created, i.Title }).ToList();
            //  var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Select(c => new { c.Created, EventContents =  c.Items.Select(k => k.Title)}).ToList();
            // var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).ToList();
            foreach (var events in localEventComplete)
            {
                Console.WriteLine("Order: order.Created");

                //var comm =localComment.Find(events.EventsContents);


                yield return events;
            }
        }

        public IEnumerable GetMonthEventContents(int month, int year)
        {
            var localEvent = db.Events;
            var localEventContent = db.EventsContents;
            //Switch Include to Select. 

            //  var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Include(c=>c.EventsContents).ToList();
            var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).SelectMany(c => c.Items, (c, i) => new { c.Created, i.Title, i.Comment }).ToList();
            // var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).ToList();
            foreach (var events in localEventComplete)
            {
                Console.WriteLine("Order: order.Created");

                //var comm =localComment.Find(events.EventsContents);


                yield return events;
            }
        }

        public IEnumerable GetMonthDayEvents(DateTime date)
        {
         


            // var results = db.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).SelectMany(c => c.Items, (c,i) => new { c.Created, i.Title, i.Comment, i.Id });
            var results = db.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Select(c => new { c.Created, items = c.Items.Select(c => new { c.Title, c.Comment, c.Id }) });
            return results;
        }

        public void AddEvent(DateTime date, string title, string comment)
        {
            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");


             int dayDate = date.Day;
             int monthDate = date.Month;
             int yearDate = date.Year;


            var eventDb = db.Events;
            ICollection<EventsContents>[] dbContent = eventDb.Where(c => c.Created == date).Select(c => c.Items).ToArray();
            Events[] eventLocal = eventDb.Where(c => c.Created == date).ToArray();

            if (eventLocal.Length > 0)
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
                    Year = yearDate,
                    Month = monthDate,
                    Day = dayDate,
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

        }

        public void AddEventToday(string title, string comment)
        {
            DateTime todayDate = DateTime.Today;
            AddEvent(todayDate, title, comment);

        }

        public void DeleteToday()
        {
            DateTime todayDate = DateTime.Today;
            DeleteEvent(todayDate);

        }

        public void DeleteTodayEventItem(string id)
        {
            DateTime todayDate = DateTime.Today;
            DeleteEventItem(todayDate, id);

        }

        public void DeleteEvent(DateTime date)
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
                db.EventsContents.Remove(item);
            }

            db.Events.Remove(events);
            db.SaveChanges();

        }

        public void DeleteEventItem(DateTime date, string id)
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

        }

        public void EditEventItem(DateTime date, string id, string title, string comment)
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

        }

        public void EditEventItemToday(string id, string title, string comment)
        {
            DateTime todayDate = DateTime.Today;
            EditEventItem(todayDate, id, title, comment);
        }
    }
}
