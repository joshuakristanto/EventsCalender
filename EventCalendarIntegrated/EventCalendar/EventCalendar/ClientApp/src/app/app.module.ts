import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';

import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { AddEventComponent } from './add-event/add-event.component';
import { ViewEventComponent } from './view-event/view-event.component';
import { EditEventComponent } from './edit-event/edit-event.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatNativeDateModule} from '@angular/material/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreateAccountComponent } from './create-account/create-account.component';

import { SocialLoginModule, SocialAuthServiceConfig  } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    AddEventComponent,
    ViewEventComponent,
    CreateAccountComponent,
    EditEventComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    FormsModule,
    SocialLoginModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'createAccount', component: CreateAccountComponent },
    ]),
    BrowserAnimationsModule
  ],
  entryComponents: [ ViewEventComponent,  LoginComponent,
    AddEventComponent, CreateAccountComponent, EditEventComponent],
    providers: [
      {
        provide: 'SocialAuthServiceConfig',
        useValue: {
          autoLogin: false,
          providers: [
            {
              id: GoogleLoginProvider.PROVIDER_ID,
              provider: new GoogleLoginProvider(
                // replace this with your google client id			
                '218984349286-j5ri6sd2vkl0u85j2h6g41glgekrlis1.apps.googleusercontent.com'
              )
            }
          ]
        } as SocialAuthServiceConfig,
      },],
    bootstrap: [AppComponent]
  })
  export class AppModule { }
