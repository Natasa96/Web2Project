import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { EditStationsModel } from '../EditStationsMode';
import { StationEditComponent } from '../station-edit/station-edit.component';
import { FormBuilder } from '@angular/forms';
import { ConnectableObservable } from 'rxjs';
import { ConnectionService } from 'src/app/connection.service';
import { GeoLocation } from 'src/app/map/Model/geolocation';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { MapComponent } from 'src/app/map/map.component';
import { MarkerInfo } from 'src/app/map/Model/markerInfoModel';
import { Polyline } from 'src/app/map/Model/Polyline';

@Component({
  selector: 'app-station-edit-info',
  templateUrl: './station-edit-info.component.html',
  styleUrls: ['./station-edit-info.component.css'],
})
export class StationEditInfoComponent implements OnInit {

  @Input() selectedStation: EditStationsModel
  dropdownSettings;
  dropdownItems: any;
  selectedItems:number[] = [];
  lines: Array<any> = [];

  markerInfo: MarkerInfo;
  public polyline: Polyline;
  public zoom: number;


  EditStationForm = this.fb.group({
    Name: [''],
    Address: [''],
    Longitude: [''],
    Latitude: [''],
    NLine: []
  })

  constructor(private fb: FormBuilder, private Service: ConnectionService) { }  

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: false,
      itemsShowLimit: 5,
      idField: 'item_id',
      textField: 'item_text'
    };
  }

  stationLocation: any;

  populateForm(selectedStation: EditStationsModel){
    this.EditStationForm.get("Name").patchValue(selectedStation.Name);
    this.EditStationForm.get("Address").patchValue(selectedStation.Address);
    this.EditStationForm.get("Longitude").patchValue(selectedStation.Longitude);
    this.EditStationForm.get("Latitude").patchValue(selectedStation.Latitude);
    this.EditStationForm.get("NLine").patchValue(selectedStation.NLine);
    selectedStation.NLine.forEach(element => {
      this.lines = [...this.lines,
      {item_id: element, item_text: element}];
    });
    this.selectedItems = selectedStation.SelectedLines;
    
    console.log('lines?? :', this.lines);
    this.stationLocation = new GeoLocation(selectedStation.Latitude, selectedStation.Longitude);
    //this.map.initMarker(this.selectedStation.Latitude,this.selectedStation.Longitude);
  }

  getCoordsFromMap(coords: GeoLocation){
    this.EditStationForm.get('Latitude').patchValue(coords.latitude);
    this.EditStationForm.get('Longitude').patchValue(coords.longitude);
  }

  onItemSelect(item: any){
    this.selectedItems.push(item);
    console.log(this.selectedItems);
  }

  onItemDeSelect(item: any){
    this.selectedItems.filter(el => el !== item);
    console.log(this.selectedItems);
  }

  onItemSelectAll(items: any) {
    this.selectedItems = items;
    console.log(this.selectedItems);
  }

  onItemDeSelectAll(items: any){
    this.selectedItems = [];
    console.log(this.selectedItems);
  }

  updateStation(){
    let stationData = new EditStationsModel();
    stationData.Id = this.selectedStation.Id;
    stationData.Address = this.EditStationForm.get('Address').value;
    stationData.Name = this.EditStationForm.get('Name').value;
    stationData.Latitude = this.EditStationForm.get('Latitude').value;
    stationData.Longitude = this.EditStationForm.get('Longitude').value;
    this.selectedItems.map(item => {
      stationData.NLine.push(item);
    });
    console.log(stationData);
    this.Service.updateStation(stationData).subscribe((res) => {
      console.log(res);
    });
  }
  
}
