import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/User';
import { AuthService } from 'src/app/services/auth.service';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: User = {
    firstName: null,
    surname: null,
    email: null,
    password: null,
    role: null
  };
  isSuccessful = false;
  errorMessage = '';
  isSignUpFailed = false;
  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }
  onSubmit(): void {
    this.authservice.register(this.form).subscribe(
      data => {
        console.log(data);
        this.isSuccessful = true;
      },
      err => {
        this.errorMessage = err.error.errorMessage;
        this.isSuccessful = false;
        this.isSignUpFailed = true;
      }
    );
  }
}
