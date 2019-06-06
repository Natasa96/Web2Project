import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { resource } from 'selenium-webdriver/http';
import { ConditionalExpr } from '@angular/compiler';
import { NetworkLineModel } from 'src/app/NetworkLineModel';

@Component({
  selector: 'app-station-add',
  templateUrl: './station-add.component.html',
  styleUrls: ['./station-add.component.css']
})
export class StationAddComponent implements OnInit {

  StationAddForm = this.fb.group({
    Name: ['', Validators.required],
    Address: [''],
    NLine: [''],
    Longitude: [''],
    Latitude: ['']
  })

  lines: NetworkLineModel[];

  constructor(private fb: FormBuilder, private Service: ConnectionService) { }

  ngOnInit() {
    this.getNetworkLines();
  }

  getNetworkLines(){
    this.Service.getLines().subscribe((result) => this.lines = result);
    console.log(this.lines);
  }

  addStation(){
    let stationData ={
      Name: this.StationAddForm.controls["Name"].value,
      Address: this.StationAddForm.controls["Address"].value,
      NLine: this.StationAddForm.controls["NLine"].value,
      Longitude: 0,
      Latitude: 0
    }
    this.Service.addStations(stationData).subscribe((result) => console.log(result));
  }
}
