import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';
import { HomePageComponent } from './HomePage/home-page/home-page.component';
import { RegisterComponent } from './HomePage/register/register.component';
import { LoginComponent } from './HomePage/login/login.component';
import { AboutComponent } from './HomePage/about/about.component';
import { AdminPageComponent } from './AdminPage/admin-page/admin-page.component';
import { PassangerComponent } from './PassangerPage/passanger/passanger.component';
import { DocumentPassangerComponent } from './HomePage/document-passanger/document-passanger.component';
import { LineEditComponent } from './AdminPage/line-edit/line-edit.component';
import { StationEditComponent } from './AdminPage/station-edit/station-edit.component';
import { TimetableEditComponent } from './AdminPage/timetable-edit/timetable-edit.component';
import { PricelistEditComponent } from './AdminPage/pricelist-edit/pricelist-edit.component';
import { ControllerComponent } from './AdminPage/controller/controller.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    RegisterComponent,
    LoginComponent,
    AboutComponent,
    AdminPageComponent,
    PassangerComponent,
    DocumentPassangerComponent,
    LineEditComponent,
    StationEditComponent,
    TimetableEditComponent,
    PricelistEditComponent,
    ControllerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
