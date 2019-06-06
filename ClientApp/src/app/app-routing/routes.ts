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
import { ControllerComponent } from '../AdminPage/controller/controller.component';
import { DocumentPassangerComponent } from '../HomePage/document-passanger/document-passanger.component';
export const routes: Routes = [
  {path: '', redirectTo: '/Home', pathMatch: 'full'},
  {path: 'Home', component: HomePageComponent},
  {path: 'Register', component: RegisterComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'About', component: AboutComponent},

  {
    path: 'Admin', 
    component: AdminPageComponent, 
    canActivate: [AuthGuard]
  },
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
    path: 'AddController',
    component: ControllerComponent
  },
  {
    path: 'Passenger',
    component: PassangerComponent,
    canActivate: [PassangerAuthGuard]
  },
  {
    path: 'Documentation',
    component: DocumentPassangerComponent
  }
];
