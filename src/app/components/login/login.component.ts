import { Component, OnInit } from '@angular/core';
import { LoginRequest } from 'src/app/models/LoginRequest';
import { AuthService } from 'src/app/services/auth.service';
import { SessionService } from 'src/app/services/session.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
form: LoginRequest = {
email : '',
password : ''
}; 
isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';

  constructor(private authService: AuthService, private sessionService: SessionService) { }

  ngOnInit() {
    if (this.sessionService.getAuthToken()) {
      this.isLoggedIn = true;
      //this.role = this.sessionService.getUser()?.role ?? '';
    }
  }

  onSubmit(): void {
    this.authService.login(this.form.email, this.form.password).subscribe(
      data => {
        console.log(data);
        data = data.data;
        this.sessionService.saveAuthToken(data.token, data.expiration);
        this.sessionService.saveUser(data);
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        //this.role = this.sessionService.getUser()?.role ?? '';
        this.reloadPage();
      },
      err => {
        console.log(err);
        this.errorMessage = err.error.errorMessage;
        this.isLoginFailed = true;
      }
    );
  }
  reloadPage(): void {
    window.location.reload();
  }

}
