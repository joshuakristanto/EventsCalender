import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ViewEventService {

  constructor(private http: HttpClient) {



  }


  errorResponse(error: any) {
    console.log(error);
    console.log(error['status']);
    if (error['status'] === 401) {
      console.log("Please Login Calendar");
      // this.router.navigate([`../login`], { relativeTo: this.route });
      //  this.login = "Login";

      // alert("Not currently Login. Please Login or create account to have full access.");
      // this.router.navigate([`../login`], { relativeTo: this.route });
    }
    if (error['status'] === 403) {
      alert("You do not have the rights to do this actions. Error 403 Forbidden.");
    }
  }

  updateEvent(date: any) {
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
  output(){
    console.log("SERVICE");
  }

  deleteEvent(date: Date) {

    const param = new HttpParams()
      .append('date', date.toISOString());

    const body = JSON.stringify("");

    let token = localStorage.getItem('jwt');
    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ${token}`);
    return this.http.post<Event>(location.origin + "/Events/Delete", body, ({ headers: header, params: param }));
  }


  deleteEventItem(date: Date, eventID: any) {
    console.log("EVENT ID" + eventID);

    const param = new HttpParams()
      .append('date', date.toISOString())
      .append('id', eventID);

    const body = JSON.stringify("");

    let token = localStorage.getItem('jwt');
    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      )
      .append('Authorization', `Bearer ${token}`);
    return this.http.post<Event>(location.origin + "/Events/DeleteItem", body, ({ headers: header, params: param }));
    //this.activeModal.close("Close click");
  }

  fetchData(date: any) {
    const param = new HttpParams()
      .append('date', date.toISOString());


    const body = JSON.stringify("");
    console.log(`Bearer ` + localStorage.getItem('jwt'));
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
  created: Date;
  year: number;
  month: number;
  day: number;
  commentedCreated: Date;
  eventsContent: EventContents;
}
interface EventContents {
  commentedCreated: Date;
  title: string;
  comment: string;
}
