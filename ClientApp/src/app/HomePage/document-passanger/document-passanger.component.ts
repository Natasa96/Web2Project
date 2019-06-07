import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { DocumentModel } from 'src/app/documentModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOprions = {
  headers: new HttpHeaders({
    'Content-Type' : 'multipart/form-data'
  })
};

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

  constructor(private fb: FormBuilder, private connectionService: ConnectionService, private http: HttpClient) { }

  ngOnInit() {
    this.getTypes();
  }

  processFile(event){
    this.selectedFile = event.target.files[0];
    this.DocumentForm.get("Document").setValue(this.selectedFile);
  }

  getTypes() {
    this.connectionService.getPassangerTypes().subscribe((p) => 
    {
      this.passangerTypes = p;
      console.log(p);
    });
  }

  addDocumentation(){
    const formData = new FormData();
    formData.append('file', this.DocumentForm.get('Document').value);
    formData.append('UserType',this.DocumentForm.get('Type').value);
    this.http.post<any>("http://localhost:52295/api/AppUser/AddDocumentation", formData).subscribe(
      (res) => console.log(res),
      (err) => console.log(err)
    );
  }


}
