import { Time } from "@angular/common";
import { StationModel } from "./StationModel";

export class NetworkLineModel{
    Id: number;
    LineNumber: number;
    Stations: [];
    Type: string;
    Departures: string[];
    ScheduleDays: string[];
}