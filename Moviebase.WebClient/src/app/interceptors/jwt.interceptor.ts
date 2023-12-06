import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs/operators';
import { Logged } from '../models/logged';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentLogged: Logged;

    this.accountService.currentLogged$
        .pipe(take(1))
        .subscribe(logged => {currentLogged = logged!});
    
    if(currentLogged!) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentLogged.token}`
        }
      })
    }

    return next.handle(request);
  }
}