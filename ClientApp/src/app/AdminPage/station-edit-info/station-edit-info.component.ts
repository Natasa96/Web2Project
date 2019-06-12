import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { EditStationsModel } from '../EditStationsMode';
import { StationEditComponent } from '../station-edit/station-edit.component';
import { FormBuilder } from '@angular/forms';
import { ConnectableObservable } from 'rxjs';
import { ConnectionService } from 'src/app/connection.service';
import { GeoLocation } from 'src/app/map/Model/geolocation';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { MapComponent } from 'src/app/map/map.component';

@Component({
  selector: 'app-station-edit-info',
  templateUrl: './station-edit-info.component.html',
  styleUrls: ['./station-edit-info.component.css'],
})
export class StationEditInfoComponent implements OnInit {

  @Input() selectedStation: EditStationsModel
  dropdownSettings;
  dropdownItems: any;

  EditStationForm = this.fb.group({
    Name: [''],
    Address: [''],
    Longitude: [''],
    Latitude: [''],
    NLine: ['']
  })

  constructor(private fb: FormBuilder, private Service: ConnectionService) { }

  

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: false,
      itemsShowLimit: 1,
      idField: 'Id',
      textField: 'Id',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      allowSearchFilter: true,
    };
  }
  @ViewChild('app-map') map: MapComponent;
  populateForm(selectedStation: EditStationsModel){
    this.EditStationForm.get("Name").patchValue(selectedStation.Name);
    this.EditStationForm.get("Address").patchValue(selectedStation.Address);
    this.EditStationForm.get("Longitude").patchValue(selectedStation.Longitude);
    this.EditStationForm.get("Latitude").patchValue(selectedStation.Latitude);
    this.EditStationForm.get("NLine").patchValue(selectedStation.NLine);
    this.dropdownItems = [
      {Id: selectedStation.Id}
    ];
    this.map.initMarker(this.selectedStation.Latitude,this.selectedStation.Longitude);
  }
  getCoordsFromMap(coords: GeoLocation){
    this.EditStationForm.get('Latitude').patchValue(coords.latitude);
    this.EditStationForm.get('Longitude').patchValue(coords.longitude);
  }

  ItemSelect(item: any){
    //this.dropdownItems.push(item);
    console.log(item);
  }
  SelectAll(items: any) {
    //this.dropdownItems.push(items);
    console.log(items);
  }
  updateStation(){
    let stationData = new EditStationsModel();
    stationData.Id = this.selectedStation.Id;
    stationData.Address = this.EditStationForm.get('Address').value;
    stationData.Name = this.EditStationForm.get('Name').value;
    stationData.Latitude = this.EditStationForm.get('Latitude').value;
    stationData.Longitude = this.EditStationForm.get('Longitude').value;
    stationData.NLine = this.EditStationForm.get('NLine').value;
    this.Service.updateStation(stationData).subscribe((res) => {
      console.log(res);
    });
  }
  

}
