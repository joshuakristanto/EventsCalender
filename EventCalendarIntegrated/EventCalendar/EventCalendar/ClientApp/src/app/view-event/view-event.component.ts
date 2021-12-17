import { Component, Input, NgModuleRef, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { startOfDay } from 'date-fns';
import { CalendarView, CalendarEvent } from 'angular-calendar';
import { AddEventComponent } from '../add-event/add-event.component';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EditEventComponent } from '../edit-event/edit-event.component';
import { ViewEventService } from './view-event.service';
@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {

  @Input() my_modal_title: any;
  @Input() my_modal_content: any;
  @Input() my_modal_comment: any;
  @Input() date: any;
  @Input() array: Array<any> = [];
  @Output() update = new EventEmitter<any>();
  modalOptions: NgbModalOptions;

  constructor(public activeModal: NgbActiveModal, private modalService: NgbModal, private http: HttpClient, private viewEventService: ViewEventService) {

    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'

    }

  }



  // receiveMessage($event : any) {
  //   if($event === "Update")
  //   {

  //     this.updateEvent();
  //   }
  // }
  ngOnInit() {

   this.viewEventService.fetchData(this.date).subscribe(result => {

      console.log("Day", result.toString());
      // var output = JSON.parse(result);
      console.log(result[0]);
      // console.log(result[0]['eventContents']['comment']);
      this.my_modal_content = result[0]['items']['title'];
      this.my_modal_comment = result[0]['items']['comment'];
      //  this.array= [{'title':result[0]['title'], 'comment': result[0]['comment'] }]; 
      for (var item in result[0]['items']) {
        this.array.push(result[0]['items'][item]);
      }

    }, error => this.errorResponse(error));

    // this.viewEventService.output();



  }
  openAddEvent(title: any, date: any) {
    const modalRef = this.modalService.open(AddEventComponent);
    modalRef.componentInstance.my_modal_title = title;
    modalRef.componentInstance.my_modal_content = "EVENT";
    console.log("DATE" + ":" + date);
    modalRef.componentInstance.date = date;
    // this.activeModal.close('Close click');
    modalRef.componentInstance.update.subscribe((event: any) => {
      // setTimeout(() => {  console.log("World!"); }, 2000);
      this.updateEvent();
      this.update.emit({ update: "Update" });
    })
    // modalRef.componentInstance["Update"].subscribe((event: any) => {
    //   console.log(" EVENT" +event) //< you now have access to the event that was emitted, to pass to your grandfather component.
    //  });


  }


  openEditEvent(date: any, id: any) {
    const modalRef = this.modalService.open(EditEventComponent);
    modalRef.componentInstance.id= id;
    modalRef.componentInstance.my_modal_title = "title";
    modalRef.componentInstance.my_modal_content = "EVENT";
    console.log("DATE" + ":" + date);
    modalRef.componentInstance.date = date;
    // this.activeModal.close('Close click');
    modalRef.componentInstance.update.subscribe((event: any) => {
      // setTimeout(() => {  console.log("World!"); }, 2000);
      this.updateEvent();
      this.update.emit({ update: "Update" });
    })
    // modalRef.componentInstance["Update"].subscribe((event: any) => {
    //   console.log(" EVENT" +event) //< you now have access to the event that was emitted, to pass to your grandfather component.
    //  });


  }

  updateEvent() {
    

    this.viewEventService.updateEvent(this.date).subscribe(result => {
      console.log("Day", result.toString())
      // var output = JSON.parse(result);
      console.log(result[0]);
      // console.log(result[0]['eventContents']['comment']);
      this.my_modal_content = result[0]['items']['title'];
      this.my_modal_comment = result[0]['items']['comment'];
      this.array = [];
      //  this.array= [{'title':result[0]['title'], 'comment': result[0]['comment'] }]; 
      for (var item in result[0]['items']) {
        this.array.push(result[0]['items'][item]);
      }

    }, error => this.errorResponse(error));





  }

  deleteEvent(date: Date) {
    // this.http.post<Event>("https://localhost:44382/Events/Add", params:({ date: this.date ,title: localTitle , comment:localComment })).subscribe(result => {

    //   }, error => console.error(error));

    this.viewEventService.deleteEvent(date).subscribe(result => {
      this.updateEvent();
      this.update.emit({ update: "Update" });

    }, error => this.errorResponse(error));

    this.activeModal.close("Close click");


  }

  deleteEventItem(date: Date,  eventID: any) {
    // this.http.post<Event>("https://localhost:44382/Events/DeleteItem", params:({ date: this.date ,title: localTitle , comment:localComment })).subscribe(result => {

    //   }, error => console.error(error));

    
    this.viewEventService.deleteEventItem(date,eventID).subscribe(result => {
      this.updateEvent();
      this.update.emit({ update: "Update" });

    }, error => this.errorResponse(error));

    this.updateEvent();
    //this.activeModal.close("Close click");


  }

  errorResponse(error: any) {
    console.log(error);
    console.log(error['status']);
    if (error['status'] === 401) {
      console.log("Please Login Calendar");
      // this.router.navigate([`../login`], { relativeTo: this.route });
    //  this.login = "Login";

      // alert("Not currently Login. Please Login or create account to have full access.");
      // this.router.navigate([`../login`], { relativeTo: this.route });
    }
    if (error['status'] === 403) {
      alert("You do not have the rights to do this actions. Error 403 Forbidden.");
    }
  }

  

}

interface Event {
  created: Date;
  year: number;
  month: number;
  day: number;
  commentedCreated: Date;
  eventsContent: EventContents;
}
interface EventContents {
  commentedCreated: Date;
  title: string;
  comment: string;
}
