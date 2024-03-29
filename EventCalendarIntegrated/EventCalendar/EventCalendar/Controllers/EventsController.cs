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




        [Authorize(Roles = "Admin,Guest")]
        [Route("Month")]
        [HttpGet]
        public IEnumerable GetMonthEvents(int month, int year)
        {
            return _calendarDb.GetMonthEvents(month, year);

        }


        [Authorize(Roles = "Admin,Guest")]
        [Route("MonthComments")]
        [HttpGet]
        public IEnumerable GetMonthEventContents(int month, int year)
        {
            return _calendarDb.GetMonthEventContents(month, year);

        }


        [Authorize(Roles = "Admin,Guest")]
        [Route("Day")]
        [HttpGet]
        public IEnumerable GetMonthDayEvents(DateTime date)
        {
            return _calendarDb.GetMonthDayEvents(date);
        }



        [Authorize(Roles = "Admin,Guest")]
        [Route("EventContent")]
        [HttpGet]
        public IEnumerable GetEventContent(string id)
        {
            return _calendarDb.GetEventContentModel(id);
        }

        [Authorize(Roles = "Admin,Guest")]
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


        [Authorize(Roles = "Admin")]
        [Route("Add")]
        [HttpPost]
        public IActionResult AddEvent(DateTime date, string title, string comment)
        {
            _calendarDb.AddEvent(date, title, comment);
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [Route("AddToday")]
        [HttpPost]
        public IActionResult AddEventToday(string title, string comment)
        {
            _calendarDb.AddEventToday(title, comment);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpPost]
        public IActionResult DeleteEvent(DateTime date)
        {
            _calendarDb.DeleteEvent(date);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteToday")]
        [HttpPost]
        public IActionResult DeleteToday()
        {
            _calendarDb.DeleteToday();

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [Route("DeleteTodayEventItem")]
        [HttpPost]
        public IActionResult DeleteTodayEventItem(string id)
        {
            _calendarDb.DeleteTodayEventItem(id);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [Route("DeleteItem")]
        [HttpPost]
        public async Task<IActionResult> DeleteEventItem(DateTime date, string id)
        {
            _calendarDb.DeleteEventItem(date, id);


            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [Route("EditItem")]
        [HttpPost]
        public async Task<IActionResult> EditEventItem(DateTime date, string id, string title, string comment)
        {
            _calendarDb.EditEventItem(date, id, title, comment);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [Route("EditItemToday")]
        [HttpPost]
        public async Task<IActionResult> EditEventItemToday(string id, string title, string comment)
        {
            _calendarDb.EditEventItemToday(id, title, comment);

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
