import { Injectable } from '@angular/core';
import { User } from '../models/User';

const KEY_FOR_TOKEN = 'auth-token';
const KEY_FOR_TOKEN_EXPIRY = 'auth-token-expiry';
const KEY_FOR_USER = 'auth-user';
@Injectable({
  providedIn: 'root'
})
export class SessionService {

constructor() { }
signOut(): void {
  window.sessionStorage.clear();
}
public saveAuthToken(token: string, expiry: Date): void {
  window.sessionStorage.removeItem(KEY_FOR_TOKEN);
  window.sessionStorage.removeItem(KEY_FOR_TOKEN_EXPIRY);
  window.sessionStorage.setItem(KEY_FOR_TOKEN, token);
  window.sessionStorage.setItem(KEY_FOR_TOKEN_EXPIRY, expiry.toDateString());
}
public getAuthToken(): string | null {
var authenticationToken = window.sessionStorage.getItem(KEY_FOR_TOKEN);
if(authenticationToken){
  var expiryDate = new Date(window.sessionStorage.getItem(KEY_FOR_TOKEN_EXPIRY) ?? '');
  if(expiryDate && expiryDate.getDate < Date.now){
    return null;
  }
}
  return authenticationToken;
}
public saveUser(user: User): void {
  window.sessionStorage.removeItem(KEY_FOR_USER);
  window.sessionStorage.setItem(KEY_FOR_USER, JSON.stringify(user));
}
public getUser(): User | null {
  const user = window.sessionStorage.getItem(KEY_FOR_USER);
  if (user) {
    return JSON.parse(user);
  }
  return null;
}
}
