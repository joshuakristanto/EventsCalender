import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  googleAuthenticate(user:any){
    return this.http.post<any>(location.origin + "/api/Auth/GoogleAuthenticate", { idToken: user.idToken });
  }
  login(userName:any, password:any){
    const param = new HttpParams()
      // .append('date', this.date.toISOString());
      .append('UserName', userName)
      .append('Password', password);

    const body = JSON.stringify("");

    const header = new HttpHeaders()
      .append(
        'Content-Type',
        'application/json'
      );


        return this.http.post<any>(location.origin + "/api/Auth/Login", body, ({ headers: header, params: param }));
  }
}
