import { Component, OnInit, ViewChild } from '@angular/core';
import { EditStationsModel } from '../EditStationsMode';
import { StationModel } from 'src/app/StationModel';
import { ConnectionService } from 'src/app/connection.service';
import { StationEditInfoComponent } from '../station-edit-info/station-edit-info.component';
import { GeoLocation } from 'src/app/map/Model/geolocation';
import { MapComponent } from 'src/app/map/map.component';

@Component({
  selector: 'app-station-edit',
  templateUrl: './station-edit.component.html',
  styleUrls: ['./station-edit.component.css']
})
export class StationEditComponent implements OnInit {

  selectedStation: EditStationsModel;
  stations: StationModel[];
  dropdownSettings;

  constructor(private Service: ConnectionService) { }

  ngOnInit() {
    this.getStations();
    this.dropdownSettings = {
      singleSelection: true,
      idField: 'Id',
      textField: 'Name',
      itemsShowLimit: 1,
      allowSearchFilter: true,
    };
  }

  getStations(){
    this.Service.getStations().subscribe((result) => {
      this.stations = result;
      console.log(result);
    })
  }

  getFullInfo(id: number){
    this.Service.getFullStationInfo(id).subscribe((result) =>{
      this.selectedStation = result;
      console.log(result);
      this.child.populateForm(this.selectedStation);
      
    })
  }

  onItemSelect(item: any){
    console.log(item.Id);
    this.getFullInfo(item.Id);
  }

  @ViewChild(StationEditInfoComponent) child: StationEditInfoComponent;
  
}
