import {GeoLocation} from "./geolocation";

export class MarkerInfo{

    iconURL: string;
    title: string;
    label: string;
    location: GeoLocation;
    link: string;

    constructor(location: GeoLocation, icon: string, title: string, label: string, link: string){
        this.iconURL = icon;
        this.title = title;
        this.label = label;
        this.location = location;
        this.link = link;
    }
}