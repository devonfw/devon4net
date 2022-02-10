import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable, Injector } from '@angular/core';
import { Response, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { LoginInfo } from '../backendModels/interfaces';
import { ILoginDataService } from './login-data-service-interface';
import { config } from '../../config';

@Injectable()
export class LoginRestService implements ILoginDataService {

  private readonly loginRestPath: string = 'login';
  private readonly currentUserRestPath: string = 'security/v1/currentuser/';
  private readonly logoutRestPath: string = 'logout';
  private readonly registerRestPath: string = 'register';
  private readonly changePasswordRestPath: string = 'changepassword';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${environment.restPathRoot}${this.loginRestPath}`,
    {username: username, password: password}, {responseType: 'text', observe: 'response'})
      /* .map((res) => {debugger; res}) */;
  }

  getCurrentUser(): Observable<LoginInfo> {
    return this.http.get(`${environment.restServiceRoot}${this.currentUserRestPath}`)
      .map((res: LoginInfo) => res);
  }

  register(email: string, password: string): Observable<LoginInfo> {
    return this.http.post(`${environment.restServiceRoot}${this.registerRestPath}`, {email: email, password: password})
      .map((res: LoginInfo) => res);
  }

  changePassword(username: string, oldPassword: string, newPassword: string): Observable<any> {
    return this.http.post(`${environment.restServiceRoot}${this.changePasswordRestPath}`,
        {username: username, oldPassword: oldPassword, newPassword: newPassword},
      )
      .map((res: Response) => res);
  }

}
