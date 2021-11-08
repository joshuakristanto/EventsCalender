using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using EventCalendarServer.Models;

namespace EventCalendarServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : Controller
    {

        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        /*
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        */

        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }


        [Route("Month")]
        [HttpGet]
        public IEnumerable<CalendarEventData> GetMonthEvents()
        {
            var db = new CalendarEventData();

            


            return null;
        }




        [Route("Day")]
        [HttpGet]
        public IEnumerable<CalendarEventData> GetMonthDayEvents()
        {
            var db = new CalendarEventData();




            return null;
        }

        [Route("Add")]
        [HttpPost]
        public IAsyncDisposable AddEvent(DateTime date, string title, string comment)
        {
            using (var db = new CalendarEventData())
            {
                
                var eventContents = new EventsContents()
                {
                    CommentId = 1,
                    Comment = comment,
                    Title = title,
                };
                var events = new Events()
                {
                    EventId = 1,
                    Created = date,
                    EventsContents = eventContents
                };

                db.EventsContents.Add(eventContents);
                db.Events.AddAsync(events);
                db.SaveChangesAsync();
                

            };
            
       

           // var db = new CalendarEventData();
           

            return null;
        }
    }
}
