import {GeoLocation} from "./geolocation";

export class Polyline{
    public path: GeoLocation[]
    public color: string
    public icon: any

    constructor(path: GeoLocation[], color: string, icon: any){
        this.path = path;
        this.color = color;
        this.icon = icon;
    }

    addLocation(location: GeoLocation){
        this.path.pop();
        this.path.push(location);
    }
}