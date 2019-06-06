import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-controller',
  templateUrl: './controller.component.html',
  styleUrls: ['./controller.component.css']
})
export class ControllerComponent implements OnInit {

  controllerForm = this.fb.group({
    Username: ['', Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    Email: ['', Validators.required],
    Firstname: ['', Validators.required],
    Lastname: ['', Validators.required],
    Address: ['', Validators.required],
    Birthdate: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private connectionService: ConnectionService ) { }

  ngOnInit() {
  }

  addController(){
    console.log("Obrisano");
  }
}
