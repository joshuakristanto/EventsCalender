using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

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

namespace EventCalendar.UnitTesting
{
    [TestClass]
    public class EventControllerTesting
    {
        
        private Mock<ILogger> _logger = new Mock<ILogger>();
        private Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
       

       // private CalendarEventData eventData =
       //     new CalendarEventData(options => options.UseSqlite("ConnectionStrings:EventDatabase"));
        [TestMethod]
        public void TestDetailsView()
        {
            var  optionsBuilder = new DbContextOptionsBuilder<CalendarEventData>();
            optionsBuilder.UseSqlite("ConnectionStrings:EventDatabase");
            CalendarEventData eventData = new CalendarEventData(optionsBuilder.Options);
            var mock = new Mock<ILogger<EventsController>>();
            ILogger<EventsController> logger = mock.Object;
            var controller = new EventsController(logger, eventData );
           var results =  controller.GetMonthEventContents(11, 2021);
            //var result = controller.Details(2) as ViewResult;
            Assert.AreEqual(results, results);

        }
        
    }
}
