import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class FetchDataService {

  constructor(private http: HttpClient) { }

  fetchData(){
    const body=JSON.stringify("");

let token = localStorage.getItem('jwt');
const header = new HttpHeaders()
.append(
  'Content-Type',
  'application/json'
)
.append ('Authorization', `Bearer ${token}`);

   return this.http.get<WeatherForecast[]>(location.origin+"/WeatherForecast", { headers:{
      'Authorization' : `Bearer ${token}`
    }
  })
  }
}
interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}