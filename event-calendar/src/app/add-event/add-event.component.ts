import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';  
@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {

  @Input() my_modal_title: any;
  @Input() my_modal_content: any;

  constructor(public activeModal: NgbActiveModal) {}


  ngOnInit() {
  }



  
}
