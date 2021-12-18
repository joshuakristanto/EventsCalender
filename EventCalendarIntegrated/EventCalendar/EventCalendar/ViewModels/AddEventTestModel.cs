using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCalendar.Models;

namespace EventCalendar.ViewModels
{
    public class AddEventTestModel
    {
        public DateTime Created { get; set; }

        public IEnumerable<ItemsModel> Items { get; set; }
    }
}
