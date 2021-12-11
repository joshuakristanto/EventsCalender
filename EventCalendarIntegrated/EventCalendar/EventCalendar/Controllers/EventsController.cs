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

    


       // [Authorize]
        [Route("Month")]
        [HttpGet]
        public IEnumerable GetMonthEvents( int month, int year)
        {
           
            

            return _calendarDb.GetMonthEvents(month, year);



        }


       // [Authorize]
        [Route("MonthComments")]
        [HttpGet]
        public IEnumerable GetMonthEventContents(int month, int year)
        {
            return _calendarDb.GetMonthEventContents(month, year);

        }


      //  [Authorize]
        [Route("Day")]
        [HttpGet]
        public IEnumerable GetMonthDayEvents( DateTime date)
        {
            return _calendarDb.GetMonthDayEvents(date);
        }
       //  [Authorize]
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
     //   [Authorize]
        [Route("Add")]
        [HttpPost]
        public IActionResult AddEvent(DateTime date, string title, string comment)
        {
            

            _calendarDb.AddEvent(date, title, comment);
            
            

           return Ok();
        }
      //  [Authorize]
        [Route("Delete")]
        [HttpPost]
        public IActionResult DeleteEvent(DateTime date)
        {
            _calendarDb.DeleteEvent(date);

            return Ok();
        }


      //   [Authorize]
        [Route("DeleteItem")]
        [HttpPost]
        public async Task<IActionResult> DeleteEventItem(DateTime date, string id)
        {
            _calendarDb.DeleteEventItem( date,id);


            return Ok();
        }

        //   [Authorize]
        [Route("EditItem")]
        [HttpPost]
        public async Task<IActionResult> EditEventItem(DateTime date, string id, string title, string comment)
        {
            _calendarDb.EditEventItem(date,id,title,comment);

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
