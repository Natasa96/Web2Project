import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { MarkerInfo } from 'src/app/map/Model/markerInfoModel';
import { GeoLocation } from 'src/app/map/Model/geolocation';

@Component({
  selector: 'app-station-add',
  templateUrl: './station-add.component.html',
  styleUrls: ['./station-add.component.css']
})
export class StationAddComponent implements OnInit {

  StationAddForm = this.fb.group({
    Name: ['', Validators.required],
    Address: [''],
    NLine: [''],
    Longitude: [''],
    Latitude: ['']
  })

  //linije dobijene iz back-enda
  stationLines: NetworkLineModel[];
  stationInfo: MarkerInfo;
  selectedItems: NetworkLineModel[];
  dropdownSettings = {};

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }

  constructor(private fb: FormBuilder, private Service: ConnectionService) { }

  ngOnInit() {
    this.getNetworkLines();
    this.stationInfo = new MarkerInfo(new GeoLocation(0,0),"","","","");
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'LineNumber',
      textField: 'LineNumber',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
  }

  getCoordsFromMap(coords: GeoLocation){
    this.stationInfo.location = coords;
  }

  getNetworkLines(){
    this.Service.getLines().subscribe((result) => 
        {
          this.stationLines = result 
          console.log(this.stationLines)
        });
  }

  addStation(){
    let stationData ={
      Id: 0,
      Name: this.StationAddForm.controls["Name"].value,
      Address: this.StationAddForm.controls["Address"].value,
      NLine: this.StationAddForm.controls["NLine"].value,
      Longitude: this.stationInfo.location.longitude,
      Latitude: this.stationInfo.location.latitude
    }
    this.Service.addStations(stationData).subscribe((result) => console.log(result));
  }
}
