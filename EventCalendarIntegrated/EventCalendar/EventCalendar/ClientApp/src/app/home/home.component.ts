import { Component, Inject } from '@angular/core';
import { startOfDay } from 'date-fns';
import { Observable, Subject } from 'rxjs';

import { CalendarView, CalendarEvent,  CalendarMonthViewBeforeRenderEvent,
  CalendarWeekViewBeforeRenderEvent,
  CalendarDayViewBeforeRenderEvent, } from 'angular-calendar';
import { ViewEventComponent } from '../view-event/view-event.component';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['../app.component.css']
})

export class HomeComponent {
  
  title = 'event-calendar';
  viewDate: Date = new Date();
  currentMonth : number = new Date().getMonth();
  currentYear: number = new Date().getFullYear();
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  refresh: Subject<any> = new Subject(); 
  events: CalendarEvent[] = [
    // {
    //   start: startOfDay(new Date()),
    //   title: 'First event',
    // },
    // {
    //   start: startOfDay(new Date()),
    //   title: 'Second event',
    // }
  ]

  modalOptions: NgbModalOptions;

  constructor(
    private modalService: NgbModal, private http: HttpClient, private router: Router, private route: ActivatedRoute) {
    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    }
    this.checkLoginState();
   

  }

  ngOnInit() {
    this.getCalendarEvents(new Date().getMonth()+1 , new Date().getFullYear());
    this.refresh.next();
   
  }

  


  getCalendarEvents( months :number, years: number){
    this.events = [];
    const param = new HttpParams()
    .append('month', months)
    .append('year', years);

  const body = JSON.stringify("");

  const header = new HttpHeaders()
  .append(
    'Content-Type',
    'application/json'
  )
  .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

    this.http.get<any>(location.origin+"/Events/Month", ({ headers: header, params: param })).subscribe(result => {

    console.log(result.toString())
    // var output = JSON.parse(result);
    console.log("ADD-EVENT" + result[0]['title']);
   
    for (var item in result) { 
      // block of statements 
      console.log("Home Results" , result[item]['created']);
      var localDate = new Date(result[item]['created']);

      this.events.push({ start: startOfDay(localDate), title: result[item]['title'] });
      console.log(this.events);
  }
  this.refresh.next();
  }, error => console.error(error));
  }

  setView(view: CalendarView) {
    this.view = view;
  }
  
  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    enum Months {
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
    this.open(Months[date.getMonth()] + " " + date.getDate() + ", " + date.getFullYear(), "events[0].title", date);
    //this.openAppointmentList(date)
  }

  open(date: any, event: any, dateFormat: Date) {
    const modalRef = this.modalService.open(ViewEventComponent);
    modalRef.componentInstance.my_modal_title = date;
    modalRef.componentInstance.my_modal_content = "No Events";
    modalRef.componentInstance.date = dateFormat;
    modalRef.componentInstance.update.subscribe((event:any)=>
    {
      this.getCalendarEvents(dateFormat.getMonth()+1 , dateFormat.getFullYear());
    this.refresh.next();
    })
  }

  nextMonth(){
    this.currentMonth =  this.currentMonth + 1;
    if(this.currentMonth >=12 )
    {
      this.currentMonth = 0;
      this.currentYear = this.currentYear + 1;
    }
    console.log("Month: ", this.currentMonth + 1, this.currentYear);
    this.getCalendarEvents(this.currentMonth+1, this.currentYear );
    this.refresh.next();
  }
  pastMonth(){
    this.currentMonth = this.currentMonth - 1;
    if (this.currentMonth < 0) {
      this.currentMonth = 11;
      this.currentYear = this.currentYear - 1;
    }
    console.log("Month: ", this.currentMonth + 1, this.currentYear);
    this.getCalendarEvents(this.currentMonth+1, this.currentYear );
    this.refresh.next();
  }
  activeMonth() {

    this.currentMonth = new Date().getMonth();
    this.currentYear = new Date().getFullYear();
    console.log("Month: ", this.currentMonth + 1, this.currentYear);
    this.getCalendarEvents(this.currentMonth+1, this.currentYear);
    this.refresh.next();

  }

  checkLoginState() {


    const param = new HttpParams()
      .append('date', "this.date.toISOString()");

    const body = JSON.stringify("");

    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));
    this.http.get<any>(location.origin + "/Events/CheckLoginState", ({ headers: header, params: param })).subscribe(result => {


       // this.login = "Sign-Out";

      }, error =>

      this.errorResponse(error));




  }
  errorResponse(error: any) {
    console.log(error['status']);
    if (error['status'] === 401) {
      console.log("Please Login Calendar");
      this.router.navigate([`../login`], { relativeTo: this.route });
    //  this.login = "Login";

      // alert("Not currently Login. Please Login or create account to have full access.");
      // this.router.navigate([`../login`], { relativeTo: this.route });
    }
  }

}
