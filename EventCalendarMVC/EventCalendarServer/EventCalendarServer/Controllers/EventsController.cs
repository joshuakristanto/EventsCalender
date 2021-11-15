﻿using Microsoft.AspNetCore.Mvc;
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
using EventCalendarServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace EventCalendarServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : Controller
    {


        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        
        [Route("Month")]
        [HttpGet]
        public IEnumerable GetMonthEvents( int month, int year)
        {
            using (var db = new CalendarEventData())
            {
                var localEvent = db.Events;

                var localEventComplete = localEvent.Where(c => c.Month == month && c.Year == year).Include(c=>c.EventsContents).ToList();
                foreach (var events in localEventComplete)
                {
                    Console.WriteLine("Order: order.Created");
                    
                    //var comm =localComment.Find(events.EventsContents);
                    

                    yield return events ;
                }

            }



        }



        [Route("MonthComments")]
        [HttpGet]
        public IEnumerable GetMonthEventContents()
        {
            using (var db = new CalendarEventData())
            {
                var localEvent = db.EventsContents;


                foreach (var events in localEvent)
                {
                    Console.WriteLine("Order: order.Created");
                    yield return events;
                }

            }

        }


        [Authorize]
        [Route("Day")]
        [HttpGet]
        public IEnumerable GetMonthDayEvents( DateTime date)
        {
            var db = new CalendarEventData();


            var results = db.Events.Where(p => p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month && p.Created.Value.Date.Year == date.Year).Include(c => c.EventsContents); 

            return results;
        }

        [Route("Add")]
        [HttpPost]
        public IAsyncDisposable AddEvent(DateTime date, string title, string comment)
        {
            using (var db = new CalendarEventData())
            {
                
                
                
                var eventContents = new EventsContents()
                {
                   CommentCreated = date,
                    Comment = comment,
                    Title = title,

                };
                var events = new Events()
                {
                    
                    Created = date,
                    CommentCreated = date,
                    Year = date.Year,
                    Month = date.Month,
                    Day = date.Day,
                    EventsContents = eventContents
                };

                db.EventsContents.Add(eventContents);
                db.Events.AddAsync(events);
                db.SaveChangesAsync();
                

            };
            

           // var db = new CalendarEventData();

           return null;
        }

        [Route("Delete")]
        [HttpPost]
        public IAsyncDisposable DeleteEvent(DateTime date)
        {
            using (var db = new CalendarEventData())
            {

                var results = db.Events.Where(p =>
                    p.Created.Value.Date.Day == date.Day && p.Created.Value.Date.Month == date.Month &&
                    p.Created.Value.Date.Year == date.Year).ToArray();

                var resultsEventContent = db.EventsContents.Where(p =>
                    p.CommentCreated.Value.Date.Day == date.Day && p.CommentCreated.Value.Date.Month == date.Month &&
                    p.CommentCreated.Value.Date.Year == date.Year).ToArray();
                var eventContents = resultsEventContent[0];
                var events = results[0];
                db.EventsContents.Remove(eventContents);
                db.Events.Remove(events);
                db.SaveChangesAsync();


            };



            // var db = new CalendarEventData();


            return null;
        }
    }
}
