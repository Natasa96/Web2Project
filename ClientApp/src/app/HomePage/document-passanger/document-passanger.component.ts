import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { DocumentModel } from 'src/app/documentModel';

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
  selectedFile = null;

  constructor(private fb: FormBuilder, private connectionService: ConnectionService) { }

  ngOnInit() {
    this.getTypes();
  }

  processFile(event){
    this.selectedFile = event.target.files[0];
  }

  getTypes() {
    this.connectionService.getPassangerTypes().subscribe((p) => 
    {
      this.passangerTypes = p;
      console.log(p);
    });
  }

  addDocumentation(){
    // this.connectionService.addDocumentation(this.DocumentForm.value).subscribe((result) => console.log(result));
    const fd : FormData = new FormData();
    fd.append('Image',this.selectedFile,this.selectedFile.name);
    fd.append('AppUserType',this.DocumentForm.controls['Type'].value);
    console.log(fd.get('Image'));
    this.connectionService.addDocumentation(this.selectedFile,"test").subscribe((res) => console.log(res));
  }


}
