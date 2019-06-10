import { Time } from "@angular/common";
import { StationModel } from "./StationModel";

export class NetworkLineModel{
    LineNumber: number;
    Stations: [];
    Type: string;
    Departures: Time[];
}