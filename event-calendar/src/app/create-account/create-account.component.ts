import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  invalidLogin: boolean = false;
  constructor(private http: HttpClient , private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }


  login(form:NgForm){
    console.log("UserName " + form.value['username']);
    console.log("Password " + form.value['password']);
    const param = new HttpParams()
    // .append('date', this.date.toISOString());
    .append('UserName',form.value['username'])
    .append('Password', form.value['password']);

const body=JSON.stringify("");

const header = new HttpHeaders()
  .append(
    'Content-Type',
    'application/json'
  );

this.http.post<any>("https://localhost:44382/api/Auth/Create", body,({headers: header, params:param}) ).subscribe(result => {

// const token = (<any> result).auth_token;
// console.log("jwt token: " + result.token);
// localStorage.setItem('jwt', result.token);

this.invalidLogin = false;
this.router.navigate([`../login`], { relativeTo: this.route });
// var output = JSON.parse(result);
// console.log(result[0]['eventsContents']['title']);
// console.log(result[0]['eventContents']['comment']);
    // this.my_modal_content = result[0]['eventsContents']['title'];
    // this.my_modal_comment = result[0]['eventsContents']['comment']
   }, error => console.error(error));

  }

}
