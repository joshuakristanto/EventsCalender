using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCalendar.ViewModels;

namespace EventCalendar.Interfaces
{
   public interface ICalendarEvent
    {
        IEnumerable <GetMonthEventsModel> GetMonthEvents(int month, int year);
        IEnumerable <GetMonthEventContentModel> GetMonthEventContents(int month, int year);
        IEnumerable <GetMonthDayEventsModel> GetMonthDayEvents(DateTime date);
        IEnumerable<GetEventContentModel> GetEventContentModel(string eventId);
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
