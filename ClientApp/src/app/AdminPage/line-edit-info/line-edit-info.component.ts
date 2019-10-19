import { Component, OnInit, Input } from '@angular/core';
import { NetworkLineModel } from 'src/app/NetworkLineModel';
import { ConnectionService } from 'src/app/connection.service';
import { FormBuilder, FormArray } from '@angular/forms';
import { EditLineInfoModel } from '../EditLineInfoModel';
import { timeInterval } from 'rxjs/operators';
import { DepartureModel } from '../DepartureModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-line-edit-info',
  templateUrl: './line-edit-info.component.html',
  styleUrls: ['./line-edit-info.component.css']
})
export class LineEditInfoComponent implements OnInit {

  dropdownSettings;
  dropdownSettings2;
  dropdownSettings3;
  selectedStationItem: [];
  selectedType: string;
  d: string[];

  @Input() selectedLine: EditLineInfoModel;


  EditLineForm = this.fb.group({
    Departures: this.fb.array([
    ])
  });

  constructor(private Service: ConnectionService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: true,
      idField: 'Type',
      textField: 'Type',
      itemsShowLimit: 1,
      allowSearchFilter: false,
      selectedItems: "Gradska"
    };
    this.dropdownSettings2 = {
      singleSelection: false,
      idField: 'Id',
      textField: 'Name',
      itemsShowLimit: 3,
      allowSearchFilter: false,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
    };
    this.dropdownSettings3 = {
      singleSelection: false,
      idField: 'Id',
      textField: 'Name',
      itemsShowLimit: 3,
      allowSearchFilter: false,
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
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
    this.Departures.removeAt(id);

    console.log(this.EditLineForm.get('Departures').value);
  }
  
  DeleteExistingDeparture(id:number){
    const index = this.selectedLine.Departures.indexOf(this.selectedLine.Departures.find(x => x.Id == id), 0);
      if(index > -1){
        this.selectedLine.Departures.splice(index, 1);
      }
  }

  //Dodaj jos jedan input za departure time
  addTime(){
    this.Departures.push(this.fb.control(''));
  }

  updateLine(){
    let lineData = new NetworkLineModel();

    lineData.Id = this.selectedLine.Id;
    lineData.LineNumber = this.selectedLine.LineNumber
    lineData.Stations = this.selectedLine.SelectedStations
    lineData.Type = this.selectedLine.SelectedType
    lineData.ScheduleDays = this.selectedLine.SelectedSchedule
    lineData.Departures = [...this.Departures.value]
    this.selectedLine.Departures.map(item =>{
      lineData.Departures = [...lineData.Departures, item.Time];
    });
    console.log(lineData.Departures);
    this.Service.updateLine(lineData).subscribe((result) => {
      console.log(result)
      this.router.navigate(['/Admin/LineEdit']);
    });
  }

}
