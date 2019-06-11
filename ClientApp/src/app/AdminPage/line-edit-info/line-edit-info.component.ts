import { Component, OnInit, Input } from '@angular/core';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { ConnectionService } from 'src/app/connection.service';
import { FormBuilder, FormArray } from '@angular/forms';
import { EditLineInfoModel } from '../EditLineInfoModel';

@Component({
  selector: 'app-line-edit-info',
  templateUrl: './line-edit-info.component.html',
  styleUrls: ['./line-edit-info.component.css']
})
export class LineEditInfoComponent implements OnInit {

  @Input() selectedLine: EditLineInfoModel;

  EditLineForm = this.fb.group({
    LineNumber: [''],
    Stations: [''],
    Type: [''],
    Departures: this.fb.array([
      this.fb.control('')
    ]),
    ScheduleDays: ['']
  });

  constructor(private Service: ConnectionService, private fb: FormBuilder) { }

  ngOnInit() {
    
  }

  get Departures(){
       return this.EditLineForm.get('Departures') as FormArray;
 }

  /*updateLine(){
    let lineData = {
      LineNumber: this.EditLineForm.controls['LineNumber'].value,
      Stations: this.EditLineForm.controls['Stations'].value,
      Type: this.EditLineForm.controls['Type'].value,
      Departures: this.EditLineForm.controls['Departures'].value,
      ScheduleDays: this.EditLineForm.controls['ScheduleDays'].value
    }

    this.Service.updateLine(lineData).subscribe((result) => console.log(result));
  }*/


}
