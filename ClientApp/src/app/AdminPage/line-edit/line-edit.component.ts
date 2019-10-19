import { Component, OnInit, ViewChild } from '@angular/core';
import { ConnectionService } from 'src/app/connection.service';
import { FormBuilder, FormArray } from '@angular/forms';
import { Observable } from 'rxjs';
import { StationModel } from 'src/app/StationModel';
import { SchaduleType } from 'src/app/SchaduleType';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { readElementValue } from '@angular/core/src/render3/util';
import { timeInterval } from 'rxjs/operators';
import { EditLineInfoModel } from '../EditLineInfoModel';
import { LineEditInfoComponent } from '../line-edit-info/line-edit-info.component';

@Component({
  selector: 'app-line-edit',
  templateUrl: './line-edit.component.html',
  styleUrls: ['./line-edit.component.css']
})
export class LineEditComponent implements OnInit {

  lines: NetworkLineModel[];
  selectedLine: NetworkLineModel;
  FullLineInfo: EditLineInfoModel;
  dropdownSettings;

  LineEditForm = this.fb.group({
      LineNumber: [''],
      Stations: [''],
      Type: [''],
      Departures: this.fb.array([
        this.fb.control('')
      ]),
      ScheduleDays: ['']
  });

  onItemSelect(item: any) {
    console.log(item);
    this.getFullInfo(this.lines.find(x=> x.LineNumber == item).LineNumber);
  }

  constructor(private Service: ConnectionService, private fb: FormBuilder) { }

  getFullInfo(id: number){
    this.Service.getFullInfo(id).subscribe((res)=>{
      this.FullLineInfo = res;
      console.log(res);
    });
  }

  ngOnInit() {
    this.getLines();
    this.dropdownSettings = {
      singleSelection: true,
      idField: 'LineNumber',
      textField: 'LineNumber',
      itemsShowLimit: 1,
      allowSearchFilter: true,
    };
  }

// //Linija koju menjamo
  getLines()
  {
    this.Service.getLines().subscribe((res) => {
      this.lines = res;
      console.log(res);
    });
  }

  @ViewChild(LineEditInfoComponent) child: LineEditInfoComponent;

// //Zapamti departure time iz prethodnog inputa
// get Departures(){
//   return this.LineEditForm.get('Departures') as FormArray;
// }

// //Dodaj jos jedan input za departure time
// addTime(){
//   this.Departures.push(this.fb.control(''));
// }

// //Gradska / prigradska
// getLineTypes(): void {
//   this.Service.getLineTypes().subscribe((result) => this.lineTypes = result);
// }

// //Ucitaj stanice
// getStations(){
//   this.Service.getStations().subscribe((result) =>
//   { 
//     this.nlStations = result
//     console.log(result);
//   });
// }

// // Radni dan / vikend / praznik 
// getScheduleTypes() {
//   this.Service.getScheduleTypes().subscribe((result) =>
//   {
//     this.schedule = result
//     console.log(result);
//   });
// }

//   updateLine(){
//     let lineData = {
//       LineNumber: this.LineEditForm.controls['LineNumber'].value,
//       Stations: this.LineEditForm.controls['Stations'].value,
//       Type: this.LineEditForm.controls['Type'].value,
//       Departures: this.LineEditForm.controls['Departures'].value,
//       ScheduleDays: this.LineEditForm.controls['ScheduleDays'].value
//     }

//     console.log(lineData);
//     this.Service.updateLine(lineData).subscribe((result) => console.log(result));
//   }
}
