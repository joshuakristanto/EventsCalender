using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCalendar.Models;

namespace EventCalendar.ViewModels
{
    public class GetMonthEventContentModel
    {
       public  DateTime Created { get; set; }
       public string Title { get; set; }
       public string Comment { get; set; }
    }
}
