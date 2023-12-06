import {
   HttpEvent,
   HttpHandler,
   HttpInterceptor,
   HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if(error) {
          switch (error.status) {
            case 400:
            case 404:
              if(error.error) {
                this.toastr.error(error.error);
              } else {
                  this.toastr.error("Did you not forget something?");
              }
              this.toastr.error(error.error);
              break;
            case 401:
            case 403:
              if(error.error) {
                  this.toastr.error(error.error);
              } else {
                  this.toastr.error("You shall not pass");
              }
              break;
            default:
              this.toastr.error('Something went wrong');
              this.router.navigateByUrl('/');
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
