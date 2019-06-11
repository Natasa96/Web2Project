import { StationModel } from "../StationModel";
import { TimetableModel } from "../TimetableModel";
import { DepartureModel } from "./DepartureModel";

export class EditLineInfoModel{
    LineNumber: number;
    SelectedType: string;
    AllTypes: string[];
    SelectedStations: StationModel[];
    AllStations: StationModel[];
    SelectedSchedule: string[];
    AllSchedules: string[];
    Departures: DepartureModel[];
}