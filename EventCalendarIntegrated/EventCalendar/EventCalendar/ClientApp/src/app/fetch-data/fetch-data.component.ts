import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];

  constructor(http: HttpClient) {

    // const param = new HttpParams()
    // .append('date', date.toISOString());

const body=JSON.stringify("");

let token = localStorage.getItem('jwt');
const header = new HttpHeaders()
.append(
  'Content-Type',
  'application/json'
)
.append ('Authorization', `Bearer ${token}`);

    http.get<WeatherForecast[]>(location.origin+"WeatherForecast", { headers:{
      'Authorization' : `Bearer ${token}`
    }
  }).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
