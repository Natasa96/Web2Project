import { Component, OnInit, NgZone, Input, Output, EventEmitter } from '@angular/core';
import { MarkerInfo } from './Model/markerInfoModel';
import { Polyline } from './Model/Polyline';
import { GeoLocation } from './Model/geolocation';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map{height: 500px; width: 700px}']
})
export class MapComponent implements OnInit {

  markerInfo: MarkerInfo;
  public polyline: Polyline
  public zoom: number
  hideElement: boolean;

  @Output() public childEvent = new EventEmitter();
  @Input() busLocation: GeoLocation;
  @Input() busStation: GeoLocation;
  @Input() stationsCoord: Array<Geolocation>

  constructor(private ngZone: NgZone) { }

  ngOnInit() {
    this.markerInfo = new MarkerInfo(new GeoLocation(45.242268, 19.842954), "assets/images/busicon.png", "", "", "");
    this.polyline = new Polyline([], 'blue', { url:"assets/images/busicon.png", scaledSize: {width: 50, height: 50}});
    this.hideElement = true;
  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    this.markerInfo.location = new GeoLocation($event.coords.lat,$event.coords.lng);
    this.childEvent.emit(this.markerInfo.location);
    console.log(this.markerInfo);
  }
}
