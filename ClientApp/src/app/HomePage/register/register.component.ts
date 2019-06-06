import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/app/auth/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm = this.fb.group({
    Username: ['', Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    Email: ['', Validators.required],
    Firstname: ['', Validators.required],
    Lastname: ['', Validators.required],
    Address: [''],
    Birthdate: ['']
  });

  constructor(private fb:FormBuilder, private connectionService : ConnectionService, private router: Router, private authService: AuthService) { }
  user = {
    username : '',
    password : '',
    grant_type: ''
  }
  ngOnInit() {
  }

  register(){
    this.connectionService.addUser(this.registerForm.value).subscribe((res) => {
      this.user.username = this.registerForm.controls['Username'].value;
      this.user.password = this.registerForm.controls['Password'].value;
      this.user.grant_type = "password";
      this.authService.login(this.user).subscribe((p) => this.router.navigate(['Documentation']));
    });

  }

}
