import { Time } from "@angular/common";
import { StationModel } from "./StationModel";

export class NetworkLineModel{
    Id: number;
    LineNumber: number;
    Stations: any[] = [];
    Type: string;
    Departures: string[] = [];
    ScheduleDays: string[] = [];
}