import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormArray } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { LineType } from 'src/app/LineType';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { LineEditComponent } from '../line-edit/line-edit.component';
import { StationModel } from 'src/app/StationModel';
import { SchaduleType } from 'src/app/SchaduleType';

@Component({
  selector: 'app-line-add',
  templateUrl: './line-add.component.html',
  styleUrls: ['./line-add.component.css']
})
export class LineAddComponent implements OnInit {

  LineAddForm = this.fb.group({
    LineNumber: ['', Validators.required],
    Stations: [''],
    Type: ['', Validators.required],
    Departures: this.fb.array([]),
    ScheduleDays: ['']
  });
  dropdownSettings;
  dropdownSettings2;
  dropdownSettings3;


  constructor(private fb: FormBuilder, private Service: ConnectionService) {}

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: true,
      idField: 'LineNumber',
      textField: 'Type',
      itemsShowLimit: 1,
      allowSearchFilter: false
    };
    this.dropdownSettings2 = {
      singleSelection: false,
      idField: 'Id',
      textField: 'Name',
      itemsShowLimit: 3,     
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      allowSearchFilter: false
    };
    this.dropdownSettings3 = {
      singleSelection: false,
      idField: 'Type',
      textField: 'Type',
      itemsShowLimit: 3,
      allowSearchFilter: false
    };

    this.getLineTypes();
    this.getStations();
    this.getScheduleTypes();
  }

  lineTypes = [];
  nlStations: StationModel[];
  schedule: SchaduleType[];
  selectedNumber: number;
  selectedType: any;
  selectedStations: any[] = [];
  selectedSchedule: string[] = [];


  //Zapamti departure time iz prethodnog inputa
  get Departures(){
    return this.LineAddForm.get('Departures') as FormArray;
  }

  //Dodaj jos jedan input za departure time
  addTime(){
    this.Departures.push(this.fb.control(''));
  }

  //Gradska / prigradska
  getLineTypes(): void {
    this.Service.getLineTypes().subscribe((result) => this.lineTypes = result);
  }
 
  //Ucitaj stanice
  getStations(){
    this.Service.getStations().subscribe((result) =>
    { 
      this.nlStations = result
      console.log(result);
    });
  }
  onTypeSelect(item: any){
    this.selectedType = item;
  }
  
  onStationSelect(item: any){
    this.selectedStations.push(item);
  }

  onStationAllSelect(items: any){
    this.selectedStations = items;
  }
  onStationAllDeSelect(){
    this.selectedStations = [];
  }
  onStationDeSelect(item:any){
    const index = this.selectedStations.indexOf(this.selectedStations.find(x=> x.Id ===item.Id),0);
    this.selectedStations.splice(index,1);
    console.log(this.selectedStations);
    console.log(item);
    console.log(index);
  }

  onScheduleSelect(item: any){
    this.selectedSchedule.push(item);
  }
  onScheduleDeSelect(item: any){
    this.selectedSchedule.splice(item,1);
  }
  onScheduleAllSelect(items: any){
    this.selectedSchedule = items;
  }
  onScheduleAllDeSelect(){
    this.selectedSchedule = [];
  }

  deleteDeparture(id: number){
    this.Departures.removeAt(id);
  }

  // Radni dan / vikend / praznik 
  getScheduleTypes() {
    this.Service.getScheduleTypes().subscribe((result) =>
    {
      this.schedule = result
      console.log(result);
    });
  }
  addNewLine(){
    let lineData = new NetworkLineModel();
    lineData.Departures = this.Departures.value;
    lineData.LineNumber = this.selectedNumber;
    lineData.ScheduleDays = this.selectedSchedule;
    lineData.Stations = this.selectedStations.map(item=> item.Name);  
    lineData.Type = this.selectedType;
    this.Service.addLine(lineData).subscribe((result) => console.log(result));
    console.log(lineData);
  }
}
