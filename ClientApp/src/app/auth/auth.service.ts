import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, pipe, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { User } from './user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn = false;

  loginUrl: string = 'http://localhost:52295/oauth/token';

  constructor(private http: HttpClient, private router: Router) { }

  login(user: User): Observable<any> {
    return this.http.post<any>(this.loginUrl, `username=`+ user.username +`&password=`+ user.password + `&grant_type=password`, { 'headers': { 'Content-type': 'x-www-form-urlencoded' } }).pipe(
      map(res => {
        console.log(res.access_token);

        let jwt = res.access_token;

        let jwtData = jwt.split('.')[1]
        let decodedJwtJsonData = window.atob(jwtData)
        let decodedJwtData = JSON.parse(decodedJwtJsonData)

        let role = decodedJwtData.role

        console.log('jwtData: ' + jwtData)
        console.log('decodedJwtJsonData: ' + decodedJwtJsonData)
        console.log('decodedJwtData: ' + decodedJwtData)
        console.log('Role ' + role)

        localStorage.setItem('jwt', jwt)
        localStorage.setItem('role', role);
        //redirect logic
        switch(role){
          case 'Admin': {this.router.navigate(['/Admin']); break;}
          case 'AppUser': {this.router.navigate(['/Passenger']); break;}
          case 'Controller':  {this.router.navigate(['/Controller']); break} // change once added
          default : {this.router.navigate(['/Home']);break;}
        }


      }),

      catchError(this.handleError<any>('login'))
    );
  }

  logout(): void {
    this.isLoggedIn = false;
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    let result = this.http.post<any>("http://localhost:52295/api/Account/Logout",{});
    this.router.navigate(['/Home']);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}
