import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class CreateAccountService {

  constructor(private http: HttpClient) { }

  createAccount(userName:string, password:string, role:string){


const param = new HttpParams()
    // .append('date', this.date.toISOString());
    .append('UserName',userName)
    .append('Password', password)
    .append('Role', role);

const body=JSON.stringify("");

const header = new HttpHeaders()
  .append(
    'Content-Type',
    'application/json'
  );

    return this.http.post<any>(location.origin+"/api/Auth/Create", body,({headers: header, params:param}) );
  }
}
