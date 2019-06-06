import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { MyInfo } from '../UserInfo';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  MyProfileForm = this.fb.group({
    Username: ['', Validators.required],
    Password: ['', Validators.required],
    Email: ['', Validators.required],
    Firstname: ['', Validators.required],
    Lastname: ['', Validators.required],
    Address: ['', Validators.required],
    Birthdate: ['', Validators.required],
    Type: ['', Validators.required],
    Document: [''],
    Validation: ['']
  });
  
  user :MyInfo;
  constructor(private fb: FormBuilder, private connectionService: ConnectionService) { }

  ngOnInit() {
    this.GetUserInfo();
  }
  GetUserInfo(){
    this.connectionService.getUserInfo().subscribe((result) =>{
      console.log(result);
      this.MyProfileForm.controls['Username'].patchValue(result.Username);
    });
    console.log(this.user);
  }

}
