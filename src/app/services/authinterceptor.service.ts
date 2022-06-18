import { Injectable } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SessionService } from './session.service';

const KEY_FOR_AUTHORIZATION = 'Authorization';
@Injectable({
  providedIn: 'root'
})
export class AuthinterceptorService implements HttpInterceptor {

constructor(private token: SessionService) { }
intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  let authReq = req;
  const token = this.token.getAuthToken();
  if (token != null) {
    authReq = req.clone({ headers: req.headers.set(KEY_FOR_AUTHORIZATION, 'Bearer ' + token) });
  }
  return next.handle(authReq);
}
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthinterceptorService, multi: true }
];
