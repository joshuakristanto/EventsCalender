import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgbActiveModal, NgbDateParserFormatter, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder } from '@angular/forms';
import { stringify } from '@angular/compiler/src/util';
import { ViewEventComponent } from '../view-event/view-event.component';
import { Inject } from '@angular/core';
import { AddEventService } from './add-event.service';
import { ActivatedRoute, Router } from '@angular/router';
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
  message: string = "Update";

  modalOptions: NgbModalOptions;



  constructor(public activeModal: NgbActiveModal, private http: HttpClient, private modalService: NgbModal, private AddEventService: AddEventService,private router: Router, private route: ActivatedRoute) {

    this.modalOptions = {
      backdrop: 'static',
      backdropClass: 'customBackdrop'
    }

  }


  ngOnInit() {
  }


  addEvent(date: Date, localTitle: string, localComment: string) {
    // this.http.post<Event>("https://localhost:44382/Events/Add", params:({ date: this.date ,title: localTitle , comment:localComment })).subscribe(result => {

    //   }, error => console.error(error));

    this.AddEventService.addEvent(date, localTitle, localComment).subscribe(result => {
      this.update.emit({ update: "Update" });
    }, error => this.errorResponse(error));




    this.activeModal.close("Close click");
    // this.updateEvent(date);


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

    this.AddEventService.updateEvent(this.date).subscribe(result => {

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
