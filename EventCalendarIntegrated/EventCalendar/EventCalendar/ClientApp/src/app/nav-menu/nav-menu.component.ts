import { Component, Input, NgModuleRef, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { startOfDay } from 'date-fns';
import { CalendarView, CalendarEvent } from 'angular-calendar';
import { AddEventComponent } from '../add-event/add-event.component';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  @Input() login: string = "Login";

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {

    this.checkLoginState();

  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  errorResponse(error:any){
    console.log(error['status']); 
    if (error['status'] === 401){
      console.log("Please Login");
      this.login ="Login";
      // alert("Not currently Login. Please Login or create account to have full access.");
     // this.router.navigate([`../login`], { relativeTo: this.route });
    }
  }


  loginDirective(){
    this.router.navigate([`../login`], { relativeTo: this.route });
  }
  signOutDirective(){

    // console.log("UserName " + form.value['username']);
    // console.log("Password " + form.value['password']);
    console.log("jwt token2: " + "SignOut");
    const param = new HttpParams()
      // .append('date', this.date.toISOString());
      // .append('UserName',localStorage.getItem('username'));
      .append('UserName', ''+localStorage.getItem('username'));
    const body = JSON.stringify("");

    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));
    localStorage.removeItem('jwt');

    this.router.navigate([`../login`], { relativeTo: this.route });
    // this.http.post<any>(location.origin+"/api/Auth/Logout", body, ({ headers: header, params: param })).subscribe(result => {

    //   const token = (<any>result).auth_token;
    //   console.log("jwt token2: " + result.token);
    //   localStorage.setItem('jwt', result.token);
     
    //  // localStorage.removeItem('jwt');
    //   this.router.navigate([`../`], { relativeTo: this.route });
    //   // var output = JSON.parse(result);
    //   // console.log(result[0]['eventsContents']['title']);
    //   // console.log(result[0]['eventContents']['comment']);
    //   // this.my_modal_content = result[0]['eventsContents']['title'];
    //   // this.my_modal_comment = result[0]['eventsContents']['comment']
    // }, error => console.error(error));

  }

  buttonTask(){
    if(this.login ==='Login'){
      this.loginDirective();
    }
    else{
     
      this.signOutDirective();
    }
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
    this.http.get<any>(location.origin+"/Events/CheckLoginState", ({ headers: header, params: param })).subscribe(result => {

       
      this.login ="Sign-Out";
        
    }, error => 
    
    this.errorResponse(error) );
    



  }

 


}

