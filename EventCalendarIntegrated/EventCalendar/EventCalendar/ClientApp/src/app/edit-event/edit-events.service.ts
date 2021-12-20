import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class EditEventsService {

  constructor(private http: HttpClient) { }

  addEvent(date: Date, localTitle: string, localComment: string, id: any) {
    const param = new HttpParams()
    .append('date', date.toISOString())
    .append('title', localTitle)
    .append('comment', localComment)
    .append('id', id);

  const body = JSON.stringify("");

  const header = new HttpHeaders()
    .append(
      'Content-Type',
      'application/json'
    )
    .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

  return this.http.post<Event>(location.origin + "/Events/EditItem", body, ({ headers: header, params: param }));

  }

  fetchData(id:any){
    const param = new HttpParams()

    .append('id', id);

  const body = JSON.stringify("");

  const header = new HttpHeaders()
    .append(
      'Content-Type',
      'application/json'
    )
    .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));

   return this.http.get<EventFetch>(location.origin + "/Events/EventContent", ({ headers: header, params: param }));
  }


  updateEvent(date: Date){


    const param = new HttpParams()
      .append('date', date.toISOString());

    const body = JSON.stringify("");

    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ` + localStorage.getItem('jwt'));



   return this.http.get<EventFetchDay>(location.origin + "/Events/Day", ({ headers: header, params: param }));

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
interface EventFetchDay {

  title: string;
  comment: string;
  id: string;
  date: Date;
}