import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  checkLoginState(){
    const param = new HttpParams()
    .append('date', "this.date.toISOString()");

  const body = JSON.stringify("");

  const header = new HttpHeaders()
    .append(
      'Content-Type',
      'application/json'
    )
    .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));
  return this.http.get<any>(location.origin + "/Events/CheckLoginState", ({ headers: header, params: param }))
  }
  getCalendarEvents(months: number, years: number){

    const param = new HttpParams()
    .append('month', months)
    .append('year', years);

  const body = JSON.stringify("");

  const header = new HttpHeaders()
  .append(
    'Content-Type',
    'application/json'
  )
  .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

  return this.http.get<Event>(location.origin+"/Events/Month", ({ headers: header, params: param }));
  }

  
}

interface Event {
  date: Date;
  title: string;
}

