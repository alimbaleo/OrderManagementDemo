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
  role: string = '';

  constructor(private authService: AuthService, private sessionService: SessionService) { }

  ngOnInit() {
    if (this.sessionService.getAuthToken()) {
      this.isLoggedIn = true;
      this.role = this.sessionService.getUser()?.role ?? '';
    }
  }

  onSubmit(): void {
    this.authService.login(this.form.email, this.form.password).subscribe(
      data => {
        this.sessionService.saveAuthToken(data.accessToken);
        this.sessionService.saveUser(data);
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.role = this.sessionService.getUser()?.role ?? '';
        this.reloadPage();
      },
      err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }
  reloadPage(): void {
    window.location.reload();
  }

}
