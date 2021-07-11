import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Router } from '@angular/router';

/** Pass untouched request through to the next request handler. */
@Injectable()
export class RequestInterceptor implements HttpInterceptor {

  constructor(private router: Router){}

  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
      let authToken = localStorage.getItem('token') || '';
      const secureReq = req.clone({
        url:'https://localhost:5001/api'+ req.url,
        headers: req.headers.set('Authorization', authToken)
      });
      let ok: string;
      return next.handle(secureReq).pipe(
        
        catchError((error: HttpErrorResponse) => {
          let errorMsg = '';
          if(error.status === 401){
            this.router.navigate(['/login']);
          }
          if(error.status === 400){
            alert(error.error.message);
          }
          if(error.status === 500){
            alert("there was an unexpected error. Please try again");
          }
          console.log(errorMsg);
          return throwError(errorMsg);
        })
      )
  }
}

function tap(arg0: (event: any) => "" | "succeeded", arg1: (error: any) => string): import("rxjs").OperatorFunction<HttpEvent<any>, unknown> {
  throw new Error('Function not implemented.');
}

function finalize(arg0: () => void): import("rxjs").OperatorFunction<unknown, HttpEvent<any>> {
  throw new Error('Function not implemented.');
}

