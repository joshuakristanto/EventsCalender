using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar.Interfaces
{
   public interface ICalendarEvent
    {
        IEnumerable GetMonthEvents(int month, int year);
        IEnumerable GetMonthEventContents(int month, int year);
        IEnumerable GetMonthDayEvents(DateTime date);
        void AddEvent(DateTime date, string title, string comment);
        void AddEventToday(string title, string comment);
        void DeleteToday();
        void DeleteTodayEventItem(string id);
        void DeleteEvent(DateTime date);
        void DeleteEventItem(DateTime date, string id);
        void EditEventItem(DateTime date, string id, string title, string comment);
        void EditEventItemToday(string id, string title, string comment);

    }
}
