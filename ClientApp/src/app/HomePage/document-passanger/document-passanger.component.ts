import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-document-passanger',
  templateUrl: './document-passanger.component.html',
  styleUrls: ['./document-passanger.component.css']
})
export class DocumentPassangerComponent implements OnInit {

  DocumentForm = this.fb.group({
    Type: ['', Validators.required],
    Document: ['']
  })
  passangerTypes = [];
  selectedType = '';
  constructor(private fb: FormBuilder, private connectionService: ConnectionService) { }

  ngOnInit() {
    this.getTypes();
  }

  getTypes() {
    this.connectionService.getPassangerTypes().subscribe((p) => 
    {
      this.passangerTypes = p;
      console.log(p);
    });
  }

  addDocumentation(){
    this.connectionService.addDocumentation(this.DocumentForm.value).subscribe((result) => console.log(result));
  }


}
