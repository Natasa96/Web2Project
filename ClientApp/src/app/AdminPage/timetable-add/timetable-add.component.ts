import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { validateConfig } from '@angular/router/src/config';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { resource } from 'selenium-webdriver/http';
import { SchaduleType } from 'src/app/SchaduleType';

@Component({
  selector: 'app-timetable-add',
  templateUrl: './timetable-add.component.html',
  styleUrls: ['./timetable-add.component.css']
})
export class TimetableAddComponent implements OnInit {

  TimetableAddForm = this.fb.group({
    Day: ['', Validators.required],
    Lines: ['']
  })

  constructor(private fb: FormBuilder, private Service: ConnectionService) { }

  ngOnInit() {
    this.getLines();
    this.getSchedule();
  }

  lines: NetworkLineModel[];
  days: SchaduleType[];

  getLines(){
    this.Service.getLines().subscribe((result) => 
    {
      this.lines = result
      console.log(result);
    });
  }

  getSchedule(){
    this.Service.getScheduleTypes().subscribe((result) =>{
      this.days = result
      console.log(result)
    });
  }

  addTimetable(){
    let timetableData = {
      Day: this.TimetableAddForm.controls["Day"].value,
      NLine: this.TimetableAddForm.controls["Lines"].value,
    }
    this.Service.addTimetable(timetableData).subscribe(result => console.log(result));
  }
}
