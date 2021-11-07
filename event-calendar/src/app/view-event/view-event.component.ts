import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { startOfDay } from 'date-fns';
import { CalendarView, CalendarEvent } from 'angular-calendar';
import { AddEventComponent } from '../add-event/add-event.component';
import {NgbModal, ModalDismissReasons, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {

  @Input() my_modal_title: any;
  @Input() my_modal_content: any;

  modalOptions:NgbModalOptions;

  constructor(public activeModal: NgbActiveModal, private modalService :NgbModal) {

    this.modalOptions = {
      backdrop:'static',
      backdropClass:'customBackdrop'
    }

  }


  ngOnInit() {
  }
  openAddEvent(  title: any) {
    const modalRef = this.modalService.open(AddEventComponent);
    modalRef.componentInstance.my_modal_title = title;
    modalRef.componentInstance.my_modal_content = "EVENT";
  }
}
