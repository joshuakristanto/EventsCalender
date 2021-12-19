import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgbActiveModal, NgbDateParserFormatter, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder } from '@angular/forms';
import { stringify } from '@angular/compiler/src/util';
import { ViewEventComponent } from '../view-event/view-event.component';
import { Inject } from '@angular/core';
import { EditEventsService } from './edit-events.service';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent implements OnInit {

  title: string = '';
  comment: string = '';

  @Input() my_modal_title: any;
  @Input() my_modal_content: any;
  @Input() titleModal: any;
  @Input() commentModal: any;
  @Input() date: any;
  @Input() id: any;
  @Output() update = new EventEmitter<any>();
  message: string = "Update";

  modalOptions: NgbModalOptions;



  constructor(public activeModal: NgbActiveModal, private http: HttpClient, private modalService: NgbModal, private editEventService : EditEventsService,private router: Router, private route: ActivatedRoute) {

    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    }


  }


  ngOnInit() {
    this.fetchData();
  }


  addEvent(date: Date, localTitle: string, localComment: string) {
    // this.http.post<Event>("https://localhost:44382/Events/Add", params:({ date: this.date ,title: localTitle , comment:localComment })).subscribe(result => {

    //   }, error => console.error(error));

    this.editEventService.addEvent(date,localTitle,localComment,this.id).subscribe(result => {
      this.update.emit({ update: "Update" });
    }, error => this.errorResponse(error));




    this.activeModal.close("Close click");
    // this.updateEvent(date);


  }
  fetchData() {

    console.log("ID", this.id);
    this.editEventService.fetchData(this.id).subscribe(result => {
      // this.update.emit({ update: "Update" });
      console.log("TITLE", result);
      this.titleModal = result[0]["title"];
      this.commentModal = result[0]["comment"];
    }, error => this.errorResponse(error));



  }

  updateEvent(date: Date) {
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

    this.editEventService.updateEvent(this.date).subscribe(result => {

      console.log(result.toString())
      // var output = JSON.parse(result);
      console.log("ADD-EVENT" + result[0]['eventsContents']['title']);
      // console.log(result[0]['eventContents']['comment']);

      const modalRef = this.modalService.open(ViewEventComponent);

      //date.getMonth()+" "+date.getDate()+", " + date.getFullYear()
      // var date = result[0]['created'];
      // date = new Date( Date.parse(date));

      modalRef.componentInstance.my_modal_title = Months[date.getMonth()] + " " + date.getDate() + ", " + date.getFullYear();
      modalRef.componentInstance.my_modal_content = result[0]['eventsContents']['title'];
      modalRef.componentInstance.my_modal_comment = result[0]['eventsContents']['comment']
      console.log("DATE" + ":" + result[0]['created']);
      modalRef.componentInstance.date = date;
    }, error => this.errorResponse(error));



  }
  errorResponse(error: any) {
    console.log(error);
    console.log(error['status']);
    if (error['status'] === 401) {
      console.log("Please Login Calendar");
      this.router.navigate([`../login`], { relativeTo: this.route });
      
      alert("Not currently Login. Please Login or create account to have full access.");
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
  date: Date;
  title: string;
  comment: string;
}

interface EventFetch {

  title: string;
  comment: string;
}

