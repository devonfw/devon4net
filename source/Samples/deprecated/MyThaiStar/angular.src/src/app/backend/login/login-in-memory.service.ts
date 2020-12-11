import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { LoginInfo } from '../backendModels/interfaces';
import { ILoginDataService } from './login-data-service-interface';
import { users, currentUser } from '../mock-data';
import { omit, find } from 'lodash';

@Injectable()
export class LoginInMemoryService implements ILoginDataService {

  login(username: string, password: string): Observable <string> {
    const user: LoginInfo = this.findUser(username, password);
    if (!user) {
       return Observable.throw({errorCode: 2, message: 'User name or password wrong'});
    }
    currentUser[0] = user;
    return Observable.of('JWTTOKENMOCK');
  }

  getCurrentUser(): Observable <LoginInfo> {
    return Observable.of(omit(currentUser[0], 'password'));
  }

  register(email: string, password: string): Observable <LoginInfo> {
    const existingUser: LoginInfo = find(users, (user: LoginInfo) => user.username ===  email);
    if (existingUser) {
      return Observable.throw({errorCode: 1, message: 'User already exists'});
    }
    const newUser: LoginInfo = {username: email, password: password, role: 'user'};
    users.push(newUser);
    return Observable.of(omit(newUser, 'password'));
  }

  changePassword(username: string, oldPassword: string, newPassword: string): Observable<any> {
    const userToChange: LoginInfo = this.findUser(username, oldPassword);
    if (!userToChange) {
       return Observable.throw({errorCode: 1, message: 'Change password error. Old password do not match'});
    }
    userToChange.password = newPassword;
    return Observable.of({message: 'Password changed'});
  }

  private findUser(username: string, password: string): LoginInfo {
    return find(users, { username: username, password: password });
  }

}
