import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

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
import { AuthGuard } from './auth/auth.guard';
import { JwtInterceptor } from './auth/jwt-interceptor';
import { LineAddComponent } from './AdminPage/line-add/line-add.component';
import { StationAddComponent } from './AdminPage/station-add/station-add.component';
import { TimetableAddComponent } from './AdminPage/timetable-add/timetable-add.component';
import { MyProfileComponent } from './PassangerPage/my-profile/my-profile.component';
import { BuyTicketComponent } from './PassangerPage/buy-ticket/buy-ticket.component';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';
import { ControllerPageComponent } from './ControllerPage/controller-page/controller-page.component';
import { ValidateUserComponent } from './ControllerPage/validate-user/validate-user.component';
import { CheckTicketComponent } from './ControllerPage/check-ticket/check-ticket.component';
import { UserValidateInfoComponent } from './ControllerPage/user-validate-info/user-validate-info.component';
import { CheckTicketInfoComponent } from './ControllerPage/check-ticket-info/check-ticket-info.component';


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
    LineAddComponent,
    StationAddComponent,
    TimetableAddComponent,
    MyProfileComponent,
    BuyTicketComponent,
    ControllerPageComponent,
    ValidateUserComponent,
    CheckTicketComponent,
    UserValidateInfoComponent,
    CheckTicketInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AngularMultiSelectModule
  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
