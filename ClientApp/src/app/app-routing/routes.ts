import { Routes } from '@angular/router';
import { RegisterComponent } from '../HomePage/register/register.component'
import { LoginComponent } from '../HomePage/login/login.component';
import { AboutComponent } from '../HomePage/about/about.component';
import { AdminPageComponent } from '../AdminPage/admin-page/admin-page.component';
import { AuthGuard } from '../auth/auth.guard';
import { HomePageComponent } from '../HomePage/home-page/home-page.component';
import { PassangerComponent } from 'src/app/PassangerPage/passanger/passanger.component';
import { PassangerAuthGuard } from '../auth/passanger.auth.guard';
import { LineEditComponent } from '../AdminPage/line-edit/line-edit.component';
import { StationEditComponent } from '../AdminPage/station-edit/station-edit.component';
import { TimetableEditComponent } from '../AdminPage/timetable-edit/timetable-edit.component';
import { PricelistEditComponent } from '../AdminPage/pricelist-edit/pricelist-edit.component';
import { DocumentPassangerComponent } from '../HomePage/document-passanger/document-passanger.component';
import { compileBaseDefFromMetadata } from '@angular/compiler';
import { LineAddComponent } from '../AdminPage/line-add/line-add.component';
import { StationAddComponent } from '../AdminPage/station-add/station-add.component';
import { TimetableAddComponent } from '../AdminPage/timetable-add/timetable-add.component';


import { MyProfileComponent } from '../PassangerPage/my-profile/my-profile.component';
import { BuyTicketComponent } from '../PassangerPage/buy-ticket/buy-ticket.component';
import { ControllerPageComponent } from '../ControllerPage/controller-page/controller-page.component';
import { ControllerAuthGuard } from '../auth/controller.auth.guard';
import { ValidateUserComponent } from '../ControllerPage/validate-user/validate-user.component';
import { CheckTicketComponent } from '../ControllerPage/check-ticket/check-ticket.component';
import { HomePriceListComponent } from '../HomePage/home-price-list/home-price-list.component';
import { LogoutComponent } from '../HomePage/logout/logout.component';
export const routes: Routes = [
  {path: '', redirectTo: '/Home', pathMatch: 'full'},
  {path: 'Home', component: HomePageComponent},
  {path: 'Register', component: RegisterComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'Logout', component: LogoutComponent},
  {path: 'About', component: AboutComponent},
  {path: 'TicketPrice', component:HomePriceListComponent},
  
  {
    path: 'Admin', 
    component: AdminPageComponent, 
    canActivate: [AuthGuard],
    children: [
      {
        path: 'LineEdit',
        component: LineEditComponent
      },
      {
        path: 'StationEdit',
        component: StationEditComponent
      },
      {
        path: 'TimetableEdit',
        component: TimetableEditComponent
      },
      {
        path: 'PricelistEdit',
        component: PricelistEditComponent
      },
      {
        path: 'AddLine',
        component: LineAddComponent
      },
      {
        path: 'AddStation',
        component: StationAddComponent
      },
      {
        path: 'AddTimetable',
        component: TimetableAddComponent
      }
    ]
  },
  {
    path: 'Passenger',
    component: PassangerComponent,
    canActivate: [PassangerAuthGuard],
    children: [
      {
        path: 'MyProfile',
        component: MyProfileComponent
      },
      {
        path: 'BuyTicket',
        component: BuyTicketComponent
      }
    ]
  },
  {
    path: "Controller",
    component: ControllerPageComponent,
    canActivate: [ControllerAuthGuard],
    children:[
      {
        path: "Validate",
        component: ValidateUserComponent
      },
      {
        path:"CheckTicket",
        component: CheckTicketComponent
      }
    ]
  },
  {
    path: 'Documentation',
    component: DocumentPassangerComponent
  }
];
