import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  message: string;

  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(public authService: AuthService, public router: Router, private fb: FormBuilder) { }

  login() {
    this.authService.login(this.loginForm.value).subscribe((data) => {console.log("Logged in:" + data) });
  }
}
