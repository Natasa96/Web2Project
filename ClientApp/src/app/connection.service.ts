import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AppUser } from './AppUser';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PassangerType } from './PassangerType';
import { DocumentModel } from './documentModel';
import { LineType } from './LineType';
import { StationModel } from './StationModel';
import { NetworkLineModel } from './NetworkLineModel';
import { TimetableModel } from './TimetableModel';
import { MyInfo } from './PassangerPage/UserInfo';
import { Pricelist } from './PassangerPage/Pricelist';
import { BuyTicketModel } from './PassangerPage/BuyTicketModel';
import { TicketType } from './PassangerPage/TicketType';
import { PasswordModel } from './PassangerPage/PasswordModel';
import { UserModel } from './ControllerPage/UserModel';
import { ValidateModel } from './ControllerPage/ValidateModel';
import { CheckTicketModel } from './ControllerPage/CheckTicketModel';
import { TicketModel } from './ControllerPage/TicketModel';

const httpOprions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
};
const httpdataOption ={
  headers: new HttpHeaders({
    'Content-Type' : 'multipart/form-data'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {


  private ServiceUrl = 'http://localhost:52295/api/';

  constructor(private http: HttpClient) { }

  CheckTicket(data : CheckTicketModel) : Observable<TicketModel>{
    return this.http.post<TicketModel>(
      this.ServiceUrl+"Controller/CheckTicket",
      data,
      httpOprions
    ).pipe(catchError(this.handleError<TicketModel>("TicketModel")));
  }

  validateUser(data: ValidateModel): Observable<any>{
    return this.http.post<any>(
      this.ServiceUrl+"Controller/CheckDocument",
      data,
      httpOprions
    ).pipe(catchError(this.handleError<any>("ValidateModel")));
  }

  getAllUsers(): Observable<UserModel[]>{
    return this.http.get<UserModel[]>(
      this.ServiceUrl+"Controller/GetPassengers"
    ).pipe(catchError(this.handleError<UserModel[]>("UserMOdel")));
  }

  updateProfile(data: MyInfo) : Observable<any>{
    let fd = new FormData();
    fd.append("Username", data.Username);
    fd.append("Firstname", data.Firstname);
    fd.append("Lastname", data.Lastname);
    fd.append("Address", data.Address);
    fd.append("Email", data.Email);
    fd.append("file", data.Document);
    fd.append("Type", data.Type);
    return this.http.post<any>(
      this.ServiceUrl+"AppUser/UpdateProfile",
      fd
    ).pipe(catchError(this.handleError<any>("Update profile")));
  }

  changePassword(data: PasswordModel): Observable<any>{
    return this.http.post(
      this.ServiceUrl + "Account/ChangePassword",
      data,
      httpOprions
    ).pipe(catchError(this.handleError<any>("ChangePassword")));
  }

  getPricelist() : Observable<string[]> {
    return this.http.get<string[]>(this.ServiceUrl+"AppUser/GetPricelist")
    .pipe(catchError(this.handleError<string[]>("GetPricelist")));
  }

  buyTicket(data: BuyTicketModel) : Observable<any>{
    return this.http.post<any>(
      this.ServiceUrl+ 'Appuser/BuyTicket',
      data,
      httpOprions
    ).pipe(catchError(this.handleError<any>("Error")));
  }

  calculatePrice(Type: TicketType): Observable<number>{
    return this.http.post<number>(
      this.ServiceUrl+ 'Appuser/CalculatePrice',
      Type,
      httpOprions
    ).pipe(catchError(this.handleError<number>("calculatePrice")));
  }


  addUser(user: AppUser) : Observable<AppUser>{
    return this.http.post<AppUser>(
      this.ServiceUrl + 'Account/Register',
      user, 
      httpOprions
    ).pipe(
      catchError(this.handleError<AppUser>('Register'))
    );
  }

  getUserInfo() : Observable<MyInfo>{
    return this.http.get<MyInfo>(
      this.ServiceUrl+'AppUser/MyInfo'
    ).pipe(catchError(this.handleError<MyInfo>("MyInfo")));
  }

  getPassangerTypes(): Observable<PassangerType[]>{
    return this.http.get<PassangerType[]>(
      this.ServiceUrl + 'Enums/GetPassangerType'
    ).pipe(catchError(this.handleError<PassangerType[]>("PassangerType")));
  }

  //For enums LineType
  getLineTypes() : Observable<LineType[]>{
    return this.http.get<LineType[]>(
      this.ServiceUrl + 'Enums/GetLineType'
    ).pipe(catchError(this.handleError<LineType[]>("LineType")));
  }

  //For NetworkLine objects
  getLines() : Observable<NetworkLineModel[]>{
    return this.http.get<NetworkLineModel[]>(
      this.ServiceUrl + 'Admin/GetLines'
    ).pipe(catchError(this.handleError<NetworkLineModel[]>("NetworkLineModel")));
  }

  getStations(): Observable<StationModel[]>{
    return this.http.get<StationModel[]>(
      this.ServiceUrl + 'Admin/GetStations'
    ).pipe(catchError(this.handleError<StationModel[]>("StationModel")));
  }

  addDocumentation(formData: FormData){
    console.log("data in service: " + formData.get("Image"));
    return this.http.post<any>(
      this.ServiceUrl + "AppUser/AddDocumentation",
      formData,
      httpOprions
    )
  }

  addLine(line: LineType): Observable<LineType>{
    return this.http.post<LineType>(
      this.ServiceUrl + "Admin/AddLine",
      line,
      httpOprions).pipe(
        catchError(this.handleError<LineType>("LineType"))
      );
  }

  addStations(station: StationModel){
    return this.http.post<StationModel>(
      this.ServiceUrl + "Admin/AddStation",
      station,
      httpOprions).pipe(
        catchError(this.handleError<StationModel>("StationModel"))
    );
  }

  addTimetable(timetable: TimetableModel){
    return this.http.post<TimetableModel>(
      this.ServiceUrl + "Admin/AddTimetable",
      timetable,
      httpOprions).pipe(
        catchError(this.handleError<TimetableModel>("TimetableModel"))
      );
  }

  private handleError<T>(operation = 'operation', result? : T){
    return (error : any) : Observable<T> => {
      return of(result as T);
    };
  }

  
}
