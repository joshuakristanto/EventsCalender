import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Input, OnInit, Output, EventEmitter  } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgbActiveModal, NgbDateParserFormatter, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';  
import { FormBuilder } from '@angular/forms';
import { stringify } from '@angular/compiler/src/util';
import { ViewEventComponent } from '../view-event/view-event.component';
export type Update = { update: string };
@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})

export class AddEventComponent implements OnInit {
  title: string = '';
  comment: string = '';

  @Input() my_modal_title: any;
  @Input() my_modal_content: any;
  @Input() date: any;
  @Output() update = new EventEmitter<any>();
  message: string = "Update"
  modalOptions:NgbModalOptions;
 


  constructor(public activeModal: NgbActiveModal, private http: HttpClient, private modalService :NgbModal) {
    
    this.modalOptions = {
      backdrop:'static',
      backdropClass:'customBackdrop'
    }
    
  }


  ngOnInit() {
  }


addEvent( date:Date, localTitle: string, localComment: string){
  // this.http.post<Event>("https://localhost:44382/Events/Add", params:({ date: this.date ,title: localTitle , comment:localComment })).subscribe(result => {
    
  //   }, error => console.error(error));

  const param = new HttpParams()
      .append('date', date.toISOString())
      .append('title', localTitle)
      .append('comment',  localComment);

  const body=JSON.stringify("");

  const header = new HttpHeaders()
  .append(
    'Content-Type',
    'application/json'
  )
  .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

  this.http.post<Event>("https://localhost:44382/Events/Add", body,({headers: header, params:param}) ).subscribe(result => {
    this.update.emit({update: "Update"});
     }, error => console.error(error));


    
    
this.activeModal.close("Close click");
// this.updateEvent(date);
  

}

updateEvent(date:Date){
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
console.log("ADD-EVENT"+result[0]['eventsContents']['title']);
// console.log(result[0]['eventContents']['comment']);
 
  const modalRef = this.modalService.open(ViewEventComponent);

  //date.getMonth()+" "+date.getDate()+", " + date.getFullYear()
  // var date = result[0]['created'];
  // date = new Date( Date.parse(date));
  
  modalRef.componentInstance.my_modal_title = Months[date.getMonth()]+" "+date.getDate()+", " + date.getFullYear();
  modalRef.componentInstance.my_modal_content = result[0]['eventsContents']['title'];
  modalRef.componentInstance.my_modal_comment = result[0]['eventsContents']['comment']
  console.log("DATE" + ":" + result[0]['created']);
  modalRef.componentInstance.date =date;
 }, error => console.error(error));



}




}
interface Event {
  date: Date;
  title: string;
  comment: string;
}
