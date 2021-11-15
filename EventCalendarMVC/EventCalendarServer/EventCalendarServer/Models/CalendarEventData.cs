﻿using System;
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
