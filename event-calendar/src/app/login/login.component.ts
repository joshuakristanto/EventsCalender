import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import {NgForm} from '@angular/forms'
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  invalidLogin: boolean = false;
  constructor(private http: HttpClient) { }

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

this.http.post<any>("https://localhost:44382/api/Auth/Login", body,({headers: header, params:param}) ).subscribe(result => {

const token = (<any> result).auth_token;
console.log("jwt token: " + result.token);
localStorage.setItem('jwt', result.token);

this.invalidLogin = false;
console.log(result.toString())
// var output = JSON.parse(result);
// console.log(result[0]['eventsContents']['title']);
// console.log(result[0]['eventContents']['comment']);
    // this.my_modal_content = result[0]['eventsContents']['title'];
    // this.my_modal_comment = result[0]['eventsContents']['comment']
   }, error => console.error(error));

  }



}
