import { Component } from '@angular/core';
import { startOfDay } from 'date-fns';
import { CalendarView, CalendarEvent } from 'angular-calendar';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'event-calendar';
  viewDate: Date = new Date();
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  
setView(view: CalendarView) {
  this.view = view;
}
events: CalendarEvent[] = [
  {
    start: startOfDay(new Date()),
    title: 'First event',
  },
  {
    start: startOfDay(new Date()),
    title: 'Second event',
  }
]

dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
  console.log(date);
  if(events !== undefined){
  alert(date +" EVENTS" + events[0].title);}
  else
  {

    alert(date);
  }
  //this.openAppointmentList(date)
}
}
