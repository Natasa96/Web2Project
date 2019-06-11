import { Component, OnInit } from '@angular/core';
import { MarkerInfo } from 'src/app/map/Model/markerInfoModel';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  markerInfo: MarkerInfo;
  constructor() { }

  ngOnInit() {
  }

}
