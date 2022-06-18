import { Injectable } from '@angular/core';
import { User } from '../models/User';

const KEY_FOR_TOKEN = 'auth-token';
const KEY_FOR_USER = 'auth-user';
@Injectable({
  providedIn: 'root'
})
export class SessionService {

constructor() { }
signOut(): void {
  window.sessionStorage.clear();
}
public saveAuthToken(token: string): void {
  window.sessionStorage.removeItem(KEY_FOR_TOKEN);
  window.sessionStorage.setItem(KEY_FOR_TOKEN, token);
}
public getAuthToken(): string | null {
  return window.sessionStorage.getItem(KEY_FOR_TOKEN);
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
