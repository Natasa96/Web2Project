import { Component, OnInit, NgZone, EventEmitter, Output } from '@angular/core';
import { NotificationService } from '../notification.service';
import { ScheduleLineModel } from '../AdminPage/ScheduleLineModel';
import { ConnectionService } from '../connection.service';
import { GeoLocation } from '../map/Model/geolocation';
import { DepartureModel } from '../AdminPage/DepartureModel';
import { SchaduleType } from '../SchaduleType';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css']
})
export class TimerComponent implements OnInit {

  isConnected: Boolean;
  notifications: string[];
  time: string;
  stationCoords: Array<GeoLocation>;
  dropdownSettings1;
  dropdownSettings2;

  nlines: ScheduleLineModel[];
  selectedLine: ScheduleLineModel;
  days: SchaduleType[];

  @Output() public childEvent = new EventEmitter();
  @Output() public stations = new EventEmitter();

  constructor(private notifService: NotificationService,private ngZone: NgZone, private connection: ConnectionService) {
    this.isConnected = false;
    this.notifications = [];
   }

  ngOnInit() {
    this.dropdownSettings1 = {
      singleSelection: true,
      itemsShowLimit: 1,
      idField: 'Type',
      textField: 'Type',
      allowSearchFilter: true,
    };
    this.dropdownSettings2 = {
      singleSelection: true,
      itemsShowLimit: 1,
      idField: 'Id',
      textField: 'LineNumber',
      allowSearchFilter: true,
    };
    this.checkConnection();
    this.subscribeForNotifications();
    this.getSchedule();
  }

  getSchedule(){
    this.connection.getScheduleTypes().subscribe((result) =>{
      this.days = result
      console.log(result)
    });
  }

    //For Schedule multi-select
    onItemSelect(type: string){
      console.log(type);
      this.connection.getLinesSchedule(type).subscribe((result) => {
        this.nlines = result,
        console.log(result)
      })
    }
  
    //For Lines multi-select
    onSelectLine(item: any){
      console.log(item.Id);
      this.connection.getDeparturesLine(item.Id).subscribe((result) => {
        console.log(result),
        this.selectedLine = this.nlines.find(x => x.Id == item.Id)
        this.SendLineInfo(this.selectedLine)
      })
    }

  getStations(){
    this.connection.getStationCoords().subscribe((res) =>
      this.stationCoords = res
    );
  }

  private checkConnection(){
    this.notifService.startConnection().subscribe(e => 
    {
      if(e){
        this.notifService.StartTimer();
        this.isConnected = e;
    }});
  }

  private subscribeForNotifications () {
    this.notifService.registerForBusSimulation().subscribe(e => this.onNotification(e));
  }

  public onNotification(notif: string) {

     this.ngZone.run(() => { 
       this.notifications.push(notif);  
       this.childEvent.emit(notif);
    });
  }

  SendLineInfo(item: ScheduleLineModel){
    let data = new ScheduleLineModel();
    data.Id = item.Id;
    data.LineNumber =item.LineNumber;
    this.connection.setNetworkLine(data).subscribe((res) => {
      this.stations.emit(res);
    })
  }

}
