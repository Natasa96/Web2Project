import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { validateConfig } from '@angular/router/src/config';
import { NetworkLineModel } from 'src/app/NetworkLineModel';

@Component({
  selector: 'app-timetable-add',
  templateUrl: './timetable-add.component.html',
  styleUrls: ['./timetable-add.component.css']
})
export class TimetableAddComponent implements OnInit {

  TimetableAddForm = this.fb.group({
    Day: ['', Validators.required],
    Lines: [''],
    Departures: ['', Validators.required]
  })

  constructor(private fb: FormBuilder, private Service: ConnectionService) { }

  ngOnInit() {
  }

  lines: NetworkLineModel[]

  getLines(){
    this.Service.getLines().subscribe(result => this.lines = result);
  }

  addTimetable(){
    let timetableData = {
      Day: this.TimetableAddForm.controls["Day"].value,
      Lines: this.lines,
      Departures: this.TimetableAddForm.controls["Departures"].value
    }
    this.Service.addTimetable(timetableData).subscribe(result => console.log(result));
  }
}
