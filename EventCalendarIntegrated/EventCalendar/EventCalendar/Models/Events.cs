using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EventCalendar.Models
{
    public class Events
    {
        public Events()
        {
            this.Items = new HashSet<EventsContents>();
        }
        public DateTime? Created { get; set; }

        [Key]
        public string EventId { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        //   [ForeignKey("Id")]
        //  public List <string> Id { get; set; }
        //public EventsContents EventsContents { get; set; }
        //[ForeignKey("Id")]
        public virtual ICollection<EventsContents> Items { get; set; }
    }
}
