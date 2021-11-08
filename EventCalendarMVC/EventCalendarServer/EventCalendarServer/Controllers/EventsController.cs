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

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<CalendarEventData> Get()
        {
            var db = new CalendarEventData();

            


            return null;
        }

        [HttpPost]
        public IAsyncDisposable PostEvent()
        {
            using (var db = new CalendarEventData())
            {
                var eventContents = new EventsContents()
                {
                    CommentId = 1,
                    Comment = "HELLO",
                    Title = "Hello",
                };
                var events = new Events()
                {
                    EventId = 1,
                    Created = new DateTime(2020,5,5),
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
