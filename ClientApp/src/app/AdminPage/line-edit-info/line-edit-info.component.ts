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

  dropdownSettings

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
    this.dropdownSettings = {
      singleSelection: true,
      idField: 'Type',
      textField: 'Type',
      itemsShowLimit: 1,
      allowSearchFilter: true,
    };
  }

  onItemSelect(item: any) {
    console.log(item);
    this.selectedLine.SelectedType = item;
  }

  get Departures(){
       return this.EditLineForm.get('Departures') as FormArray;
 }

 DeleteDeparture(id: number){
   const index = this.selectedLine.Departures.indexOf(this.selectedLine.Departures.find(x => x.Id == id), 0);
    if(index > -1){
      this.selectedLine.Departures.splice(index, 1);
    }
 }

  //Dodaj jos jedan input za departure time
  addTime(){
    this.Departures.push(this.fb.control(''));
  }

  populateForm(selectedLine: EditLineInfoModel){
    this.EditLineForm.get("LineNumber").patchValue(selectedLine.LineNumber);
    this.EditLineForm.get("Stations").patchValue(selectedLine.SelectedStations);
    this.EditLineForm.get("Type").patchValue(selectedLine.SelectedType);
    this.EditLineForm.get("Departures").patchValue(selectedLine.Departures);
    this.EditLineForm.get("ScheduleDays").patchValue(selectedLine.SelectedSchedule);
  }

  updateLine(){
    let lineData = new NetworkLineModel();

    lineData.Id = this.selectedLine.Id;
    lineData.LineNumber = this.EditLineForm.controls["LineNumber"].value;
    lineData.Stations = this.EditLineForm.controls["Stations"].value;
    lineData.Type = this.EditLineForm.controls["Type"].value[0];
    lineData.Departures = this.EditLineForm.controls["Departures"].value;
    lineData.ScheduleDays = this.EditLineForm.controls["ScheduleDays"].value;

    this.selectedLine.Departures.map(time => {
      lineData.Departures.push(time.Time);
    });

    console.log(lineData);
    this.Service.updateLine(lineData).subscribe((result) => console.log(result));
  }

}
