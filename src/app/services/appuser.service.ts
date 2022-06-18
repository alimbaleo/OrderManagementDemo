import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


const userBaseUrl = 'https://localhost:7061/appusers/';
@Injectable({
  providedIn: 'root'
})
export class AppuserService {

constructor(private httpClient: HttpClient) { }
getUsers(): Observable<any> {
  return this.httpClient.get(userBaseUrl + 'getlist', { responseType: 'json' });
}
}
