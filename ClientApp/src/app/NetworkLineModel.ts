import { Time } from "@angular/common";

export class NetworkLineModel{
    LineNumber: number;
    Stations: [];
    Type: string;
    Departures: Time[];
}