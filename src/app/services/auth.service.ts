import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';

const accountsRootUrl = 'https://localhost:7061/api/accounts/'
const options = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private httpClient: HttpClient) { }
login(email: string, password: string): Observable<any> {
  console.log('I am here...');
  return this.httpClient.post(accountsRootUrl + 'login', {
    email,
    password
  }, options);
}
register(createUser: User): Observable<any> {
  return this.httpClient.post(accountsRootUrl + 'register', createUser, options);
}
}
