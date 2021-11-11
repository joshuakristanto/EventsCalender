import { Component } from '@angular/core';
import { startOfDay } from 'date-fns';
import { CalendarView, CalendarEvent } from 'angular-calendar';
import { ViewEventComponent } from '../view-event/view-event.component';
import {NgbModal, ModalDismissReasons, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['../app.component.css']
})

export class HomeComponent {
  title = 'event-calendar';
  viewDate: Date = new Date();
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;

  modalOptions:NgbModalOptions;
  
  constructor(
    private modalService: NgbModal
  ){
    this.modalOptions = {
      backdrop:'static',
      backdropClass:'customBackdrop'
    }

    
  }

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
    enum Months{
      January,
      Feburary,
      March,
      April,
      May,
      June,
      July,
      August,
      September,
      October,
      November,
      December
  }
    
    console.log(date);
    // alert(date +" EVENTS" );
    this.open( Months[date.getMonth()]+" "+date.getDate()+", " + date.getFullYear(), "events[0].title", date);
    //this.openAppointmentList(date)
  }

  open(  date : any, event : any, dateFormat:Date) {
    const modalRef = this.modalService.open(ViewEventComponent);
    modalRef.componentInstance.my_modal_title = date;
    modalRef.componentInstance.my_modal_content = "No Events";
    modalRef.componentInstance.date = dateFormat;
  }
  }
