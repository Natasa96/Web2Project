import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AppUser } from './AppUser';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PassangerType } from './PassangerType';
import { DocumentModel } from './documentModel';
import { LineType } from './LineType';

const httpOprions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {

  private ServiceUrl = 'http://localhost:52295/api/';

  constructor(private http: HttpClient) { }

  addUser(user: AppUser) : Observable<AppUser>{
    return this.http.post<AppUser>(
      this.ServiceUrl + 'Account/Register',
      user, 
      httpOprions
    ).pipe(
      catchError(this.handleError<AppUser>('Register'))
    );
  }

  getPassangerTypes(): Observable<PassangerType[]>{
    return this.http.get<PassangerType[]>(
      this.ServiceUrl + 'Enums/GetPassangerType'
    ).pipe(catchError(this.handleError<PassangerType[]>("PassangerType")));
  }

  getLineTypes() : Observable<LineType[]>{
    return this.http.get<LineType[]>(
      this.ServiceUrl + 'Enums/GetLineType'
    ).pipe(catchError(this.handleError<LineType[]>("LineType")));
  }

  addDocumentation(documentModel: DocumentModel): Observable<DocumentModel>{
    return this.http.post<DocumentModel>(
      this.ServiceUrl + "AppUser/AddDocumentation",
      documentModel,
      httpOprions
    ).pipe(
      catchError(this.handleError<DocumentModel>("DocumentModel"))
    );
  }

  addLine(line: LineType): Observable<LineType>{
    return this.http.post<LineType>(
      this.ServiceUrl + "Admin/AddLine",
      line,
      httpOprions).pipe(
        catchError(this.handleError<LineType>("LineType"))
      );
  }

  private handleError<T>(operation = 'operation', result? : T){
    return (error : any) : Observable<T> => {
      return of(result as T);
    };
  }

  
}
