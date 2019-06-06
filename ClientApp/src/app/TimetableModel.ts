import { NetworkLineModel } from "./NetworkLineModel";
import { BrowserDynamicTestingModule } from "@angular/platform-browser-dynamic/testing";
import { Time } from "@angular/common";

export class TimetableModel{
    Day: Date;
    Lines: NetworkLineModel[];
    Departures: Time[]; 
}