using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Runtime.CompilerServices;
using IdentityModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EventCalendarServer.Models
{
    public class EventsContents
    {
        
        [Key]
        public string Id { get; set; }

      //  [ForeignKey("Created")]
       // public DateTime? CommentCreated { get; set; }

        [ForeignKey ("EventId")]
        public string? EventId { get; set; }

     //   public virtual Events Events { get; set; }
        public string Title { get; set; }

        public string Comment { get; set; }

        // public ICollection<EventItems> Items { get; set; }
    }
}
