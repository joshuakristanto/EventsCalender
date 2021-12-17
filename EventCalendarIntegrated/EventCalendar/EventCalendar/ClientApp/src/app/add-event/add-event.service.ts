import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AddEventService {

  constructor(private http: HttpClient) { }


  addEvent(date: Date, localTitle: string, localComment: string) {
    const param = new HttpParams()
      .append('date', date.toISOString())
      .append('title', localTitle)
      .append('comment', localComment);

    const body = JSON.stringify("");

    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

    return this.http.post<Event>(location.origin + "/Events/Add", body, ({ headers: header, params: param }));


  }

  updateEvent(date: Date) {

    const param = new HttpParams()
      .append('date', date.toISOString());

    const body = JSON.stringify("");

    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));



    return this.http.get<any>(location.origin + "/Events/Day", ({ headers: header, params: param }));

  }
}

interface Event {
  date: Date;
  title: string;
  comment: string;
}
