import { Injectable, NgModule } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from '../authentication/auth.service';

@Injectable()
export class HttpRequestInterceptorService implements HttpInterceptor {
    constructor(private auth: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Get the auth header from the service.
        const authHeader: string = this.auth.getToken();
        if (authHeader) {
            const authReq: HttpRequest<any> = req.clone({ setHeaders: { Authorization: authHeader } });
            return next.handle(authReq);
        } else {
            return next.handle(req);
        }
    }
}
