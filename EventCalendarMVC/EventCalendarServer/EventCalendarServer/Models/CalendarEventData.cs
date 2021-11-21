using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
//using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity.DbContext;

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
           protected override void OnModelCreating(ModelBuilder modelBuilder)
           {
            /* modelBuilder.Entity<EventsContents>()
                 .HasOne(b => b.Events)
                 .WithMany(b => b.Items)
                 .HasForeignKey(p =>p.EventId)
                 .HasPrincipalKey(p => p.EventId);
            */
            modelBuilder.Entity<Events>().HasMany(s => s.Items).WithOne(s => s.Events);
            modelBuilder.Entity<Events>()
                .Navigation(b => b.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
            modelBuilder.Entity<EventsContents>()
                .Navigation(b => b.Events)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }




    }
    /*
    public class Events
    {
        

        [Key]
        public DateTime? Created { get; set; } 

        
        public int Year { get; set; }

        public int Month { get; set; }
        
        public int Day { get; set; }

        

       [ForeignKey("CommentCreated")]
        public DateTime? CommentCreated { get; set; }
        public  EventsContents EventsContents { get; set; }

       // public ICollection<EventItems> Items { get; set; }
    }
    */
    /*
    public class EventsContents
    {
        [Key]
        public DateTime? CommentCreated { get; set; }
        public string Title { get; set; }

        public string Comment { get; set; }

        // public ICollection<EventItems> Items { get; set; }
    }
    */
}
