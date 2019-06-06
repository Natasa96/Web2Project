import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-line-add',
  templateUrl: './line-add.component.html',
  styleUrls: ['./line-add.component.css']
})
export class LineAddComponent implements OnInit {

  LineAddForm = this.fb.group({
    LineNumber: ['', Validators.required],
    Stations: [''],
    Type: ['', Validators.required],
    Buses: ['']
  })

  constructor(private fb: FormBuilder, private Service: ConnectionService) {}

  ngOnInit() {
    this.getLineTypes();
  }

  lineTypes = []
  selectedLine = ""

  getLineTypes(): void {
    this.Service.getLineTypes().subscribe((result) => this.lineTypes = result);
  }

  addLine(){
    let lineData = {
      LineNumber: this.LineAddForm.controls["LineNumber"].value,
      Type: this.LineAddForm.controls["Type"].value
    }
    this.Service.addLine(lineData).subscribe((result) => console.log(result));
  }
}
