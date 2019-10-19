export class EditStationsModel{
    Id: number;
    Name: string;
    Address: string;
    NLine: number[] = [];
    SelectedLines: number[] = [];
    Longitude: number;
    Latitude: number;
}