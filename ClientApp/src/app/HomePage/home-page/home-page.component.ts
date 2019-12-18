import { Component, OnInit, ViewChild } from '@angular/core';
import { MarkerInfo } from 'src/app/map/Model/markerInfoModel';
import { GeoLocation } from 'src/app/map/Model/geolocation';
import { MapComponent } from 'src/app/map/map.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  markerInfo: MarkerInfo;
  location: GeoLocation;
  stations: Array<Geolocation>;
  constructor() { }

  getStationLocation(item: any){
    this.stations = item;
    console.log(this.stations);
  }

  getLocation(coords: GeoLocation){
    console.log("Home: lat"+ coords.latitude + " lng: " + coords.longitude);
    this.location = coords;
  }
  @ViewChild(MapComponent) child: MapComponent;

  ngOnInit() {
  }

}
