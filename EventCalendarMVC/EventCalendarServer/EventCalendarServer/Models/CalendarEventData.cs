using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EventCalendarServer.Models
{
    public class CalendarEventData : DbContext
    {
        public DbSet<Events> Events { get; set; }
        public DbSet<EventsContents> EventsContents { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

       
    }
    public class Events
    {
        [Key]
        public int EventId { get; set; }

        public DateTime? Created { get; set; }

        public EventsContents EventsContents { get; set; }

       // public ICollection<EventItems> Items { get; set; }
    }

    public class EventsContents
    {
        [Key]
        public int CommentId { get; set; }
        public string Title { get; set; }

        public string Comment { get; set; }

        // public ICollection<EventItems> Items { get; set; }
    }
}
