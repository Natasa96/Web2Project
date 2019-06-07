import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormArray } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { LineType } from 'src/app/LineType';

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
    ])
  });


  constructor(private fb: FormBuilder, private Service: ConnectionService) {}

  ngOnInit() {
    this.getLineTypes();
  }

  lineTypes = []
  selectedLine = ""
  addedDeparture = []    //Dodamo sve polaske kroz
                         //klijenta i stavimo ih u addedDepartures

  get Departures(){
    return this.LineAddForm.get('Departures') as FormArray;
  }

  addTime(){
    this.Departures.push(this.fb.control(''));
  }

  getLineTypes(): void {
    this.Service.getLineTypes().subscribe((result) => this.lineTypes = result);
  }

  addLine(){
    let lineData = {
      LineNumber: this.LineAddForm.controls["LineNumber"].value,
      Type: this.LineAddForm.controls["Type"].value,
      Departures: this.LineAddForm.controls["Departures"].value
    }
    console.log(lineData);
    this.Service.addLine(lineData).subscribe((result) => console.log(result));
  }
}
