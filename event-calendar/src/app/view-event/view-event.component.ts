import { Component, Input, NgModuleRef, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { startOfDay } from 'date-fns';
import { CalendarView, CalendarEvent } from 'angular-calendar';
import { AddEventComponent } from '../add-event/add-event.component';
import {NgbModal, ModalDismissReasons, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

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
  @Output() update = new EventEmitter<any>();
  modalOptions:NgbModalOptions;

  constructor(public activeModal: NgbActiveModal, private modalService :NgbModal, private http: HttpClient) {

    this.modalOptions = {
      backdrop:'static',
      backdropClass:'customBackdrop'

    }

  }

  

  // receiveMessage($event : any) {
  //   if($event === "Update")
  //   {

  //     this.updateEvent();
  //   }
  // }
  ngOnInit() {
        
    const param = new HttpParams()
    .append('date', this.date.toISOString());


const body=JSON.stringify("");
console.log(`Bearer ` +  localStorage.getItem('jwt'));
const header = new HttpHeaders()
  .append(
    'Content-Type',
    'application/json'
  )
  .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

this.http.get<any>("https://localhost:44382/Events/Day", ({headers: header, params:param}) ).subscribe(result => {

console.log(result.toString())
// var output = JSON.parse(result);
console.log(result[0]['title']);
// console.log(result[0]['eventContents']['comment']);
    this.my_modal_content = result[0]['title'];
    this.my_modal_comment = result[0]['comment']
   }, error => console.error(error));




   
  }
  openAddEvent(  title: any, date:any) {
    const modalRef = this.modalService.open(AddEventComponent);
    modalRef.componentInstance.my_modal_title = title;
    modalRef.componentInstance.my_modal_content = "EVENT";
    console.log("DATE" + ":" + date);
    modalRef.componentInstance.date = date;
    // this.activeModal.close('Close click');
    modalRef.componentInstance.update.subscribe((event:any)=>
    {
      // setTimeout(() => {  console.log("World!"); }, 2000);
      this.updateEvent();
      this.update.emit({update: "Update"});
    })
    // modalRef.componentInstance["Update"].subscribe((event: any) => {
    //   console.log(" EVENT" +event) //< you now have access to the event that was emitted, to pass to your grandfather component.
    //  });

  
  }

  updateEvent(){
    const param = new HttpParams()
    .append('date', this.date.toISOString());

const body=JSON.stringify("");

const header = new HttpHeaders()
.append(
  'Content-Type',
  'application/json'
)
.append('Authorization', `Bearer ` + localStorage.getItem('jwt'));
this.http.get<any>("https://localhost:44382/Events/Day", ({headers: header, params:param}) ).subscribe(result => {

console.log(result.toString())
// var output = JSON.parse(result);
console.log(result[0]['title']);
// console.log(result[0]['eventContents']['comment']);
    this.my_modal_content = result[0]['title'];
    this.my_modal_comment = result[0]['comment']
   }, error => console.error(error));

  }

  deleteEvent( date:Date){
    // this.http.post<Event>("https://localhost:44382/Events/Add", params:({ date: this.date ,title: localTitle , comment:localComment })).subscribe(result => {
      
    //   }, error => console.error(error));
  
    const param = new HttpParams()
        .append('date', date.toISOString());
  
    const body=JSON.stringify("");
  
    let token = localStorage.getItem('jwt');
    const header = new HttpHeaders()
    .append(
      'Content-Type',
      'application/json'
    )
    .append ('Authorization', `Bearer ${token}`);
    this.http.post<Event>("https://localhost:44382/Events/Delete", body,({headers: header, params:param}) ).subscribe(result => {
      this.updateEvent();
      this.update.emit({update: "Update"});
      
       }, error => console.error(error));
  
  this.activeModal.close("Close click");
    
  
  }



 


 

}

interface Event{
  created : Date;
  year: number;
  month: number;
  day: number;
  commentedCreated: Date;
  eventsContent: EventContents;
}
interface EventContents{
  commentedCreated: Date;
  title: string;
  comment: string;
}
