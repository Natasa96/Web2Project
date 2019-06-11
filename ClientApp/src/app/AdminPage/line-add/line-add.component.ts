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
    Departures: this.fb.array([
      this.fb.control('')
    ]),
    ScheduleDays: ['']
  });


  constructor(private fb: FormBuilder, private Service: ConnectionService) {}

  ngOnInit() {
    this.getLineTypes();
    this.getStations();
    this.getScheduleTypes();
  }

  lineTypes = [];
  nlStations: StationModel[];
  schedule: SchaduleType[];

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

  // Radni dan / vikend / praznik 
  getScheduleTypes() {
    this.Service.getScheduleTypes().subscribe((result) =>
    {
      this.schedule = result
      console.log(result);
    });
  }

  addLine(){
    let lineData = new NetworkLineModel();
    lineData.LineNumber = this.LineAddForm.controls["LineNumber"].value;
    lineData.Stations = this.LineAddForm.controls["Stations"].value;
    lineData.Type = this.LineAddForm.controls["Type"].value;
    lineData.Departures = this.LineAddForm.controls["Departures"].value;
    lineData.ScheduleDays = this.LineAddForm.controls["ScheduleDays"].value;

    console.log(lineData);
    this.Service.addLine(lineData).subscribe((result) => console.log(result));
  }
}
