import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { MyInfo } from '../UserInfo';
import { HttpHeaders, HttpClient } from '@angular/common/http';

const httpdataOption ={
  headers: new HttpHeaders({
    'Content-Type' : 'multipart/form-data'
  })
};

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  Feedback:string;

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

  ChangePasswordForm = this.fb.group({
    OldPassword: ['',Validators.required],
    NewPassword: ['',Validators.required],
    ConfirmPassword: ['',Validators.required]
  });

  user= new MyInfo;
  imageSrc: any;
  typeList: [];
  selectedFile: null;
  
  constructor(private fb: FormBuilder, private connectionService: ConnectionService, private http: HttpClient) { }

  newImage(event){
    if (event.target.files && event.target.files[0]) {
      this.selectedFile = event.target.files[0];

      const reader = new FileReader();
      reader.onload = e => this.imageSrc = reader.result;

      reader.readAsDataURL(this.selectedFile);
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
      this.typeList = result.Types;
      this.MyProfileForm.controls['Type'].patchValue(result.Type);
      this.imageSrc = result.Document;
    });
  }

  onSubmit(){
    if(this.selectedFile === null || this.selectedFile === undefined){
      this.Feedback= "Please select a document";
    }else{
      this.MyProfileForm.get('Document').setValue(this.selectedFile);
    this.connectionService.updateProfile(this.MyProfileForm.value).subscribe((res)=>
    {
      console.log(res)
      this.Feedback = res;
    });
    }
  }
  ChangePassword(){
    this.connectionService.changePassword(this.ChangePasswordForm.value).subscribe((res)=>{
      console.log(res);
      this.Feedback = res;
    });
  }

  onChange(type){
    this.MyProfileForm.get('Type').setValue(type);
  }

}
