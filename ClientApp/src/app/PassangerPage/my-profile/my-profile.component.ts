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
    Email: ['', Validators.required],
    Firstname: ['', Validators.required],
    Lastname: ['', Validators.required],
    Address: ['', Validators.required],
    Type: [''],
    Document: [''],
    Validation: ['']
  });

  user= new MyInfo;
  imageSrc: any;
  typeList: [];
  
  constructor(private fb: FormBuilder, private connectionService: ConnectionService) { }

  newImage(event){
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];

      const reader = new FileReader();
      reader.onload = e => this.imageSrc = reader.result;

      reader.readAsDataURL(file);
  }
  }

  ngOnInit() {
    this.GetUserInfo();
  }
  //Sredi za tip putnika
  GetUserInfo(){
    this.connectionService.getUserInfo().subscribe((result) =>{
      console.log(result);
      this.MyProfileForm.controls['Username'].patchValue(result.Username);
      this.MyProfileForm.controls['Firstname'].patchValue(result.Firstname);
      this.MyProfileForm.controls['Lastname'].patchValue(result.Lastname);
      this.MyProfileForm.controls['Email'].patchValue(result.Email);
      this.MyProfileForm.controls['Address'].patchValue(result.Address);
      this.user.Birthdate = result.Birthdate;
      this.MyProfileForm.controls['Validation'].patchValue(result.Validation);
      this.MyProfileForm.controls['Document'].patchValue(result.Document);
      this.typeList = result.TypeList;
    });
  }

  onSubmit(){
    let newData = new MyInfo();
  }

}
