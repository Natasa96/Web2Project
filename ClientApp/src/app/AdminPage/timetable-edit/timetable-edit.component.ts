import { Component, OnInit, Input } from '@angular/core';
import { ConnectionService } from 'src/app/connection.service';
import { SchaduleType } from 'src/app/SchaduleType';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { ScheduleLineModel } from '../ScheduleLineModel';
import { DepartureModel } from '../DepartureModel';
import { FormBuilder, FormArray } from '@angular/forms';
import { NewDepartures } from '../NewDepartures';
import { Time } from "@angular/common";

@Component({
  selector: 'app-timetable-edit',
  templateUrl: './timetable-edit.component.html',
  styleUrls: ['./timetable-edit.component.css']
})
export class TimetableEditComponent implements OnInit {

  days: SchaduleType[];
  dropdownSettings1;
  dropdownSettings2;
  nlines: ScheduleLineModel[];
  departures: DepartureModel[];
  selectedLine: ScheduleLineModel;

  DepartureForm = this.fb.group({
    id: [''],
    time: this.fb.array([
      this.fb.control('')
    ])
  }) 

  constructor(private Service: ConnectionService, private fb: FormBuilder) { }


  ngOnInit() {
    this.getSchedule();
    this.dropdownSettings1 = {
      singleSelection: true,
      idField: 'Type',
      textField: 'Type',
      itemsShowLimit: 1,
      allowSearchFilter: true,
    };
    this.dropdownSettings2 = {
      singleSelection: true,
      idField: 'Id',
      textField: 'LineNumber',
      itemsShowLimit: 1,
      allowSearchFilter: true,
    };
  }

  get time(){
    return this.DepartureForm.get('time') as FormArray;
  } 

  addTime(){
    this.time.push(this.fb.control(''));
  }

  DeleteDeparture(id: number){
   this.time.controls.splice(id,1);
   console.log(this.DepartureForm.get('time').value);
  }
  DeleteExistingDeparture(id: number){
    const index = this.departures.indexOf(this.departures.find(x => x.Id == id), 0);
    if(index > -1){
      this.departures.splice(index, 1);
    }
    console.log(this.departures);
  }

  getSchedule(){
    this.Service.getScheduleTypes().subscribe((result) =>{
      this.days = result
      console.log(result)
    });
  }

  //For Schedule multi-select
  onItemSelect(type: string){
    console.log(type);
    this.Service.getLinesSchedule(type).subscribe((result) => {
      this.nlines = result,
      console.log(result)
    })
  }

  //For Lines multi-select
  onSelectLine(item: any){
    console.log(item.Id);
    this.Service.getDeparturesLine(item.Id).subscribe((result) => {
      this.departures = result,
      console.log(result),
      this.selectedLine = this.nlines.find(x => x.Id == item.Id)
    })
  }

  saveChanges(){
    console.log(this.DepartureForm.value);
    let data = new NewDepartures();
    data.selectedNLine = this.selectedLine.Id;
    data.Departures = this.DepartureForm.get('time').value;
    this.departures.map(t => {
      data.Departures.push(t.Time.toString());
    });
    console.log(data);
    this.Service.updateTimetable(data).subscribe((res) => {
      console.log(res);
    })
  }

}
