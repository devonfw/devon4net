import { HttpClient } from '@angular/common/http';
import { Injector, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BackendType } from './../../../app/config';
import { BackendConfig } from '../../config';
import { LoginRestService } from './login-rest.service';
import { LoginInMemoryService } from './login-in-memory.service';
import { ILoginDataService } from './login-data-service-interface';
import { LoginInfo } from '../backendModels/interfaces';

@Injectable()
export class LoginDataService implements ILoginDataService {

    private usedImplementation: ILoginDataService;

    constructor(public injector: Injector, private http: HttpClient) {
        const backendConfig: BackendConfig = this.injector.get(BackendConfig);
        if (backendConfig.environmentType === BackendType.IN_MEMORY) {
            this.usedImplementation = new LoginInMemoryService();
        } else { // default
            this.usedImplementation = new LoginRestService(http);
        }
    }

    login(username: string, password: string): Observable<string> {
        return this.usedImplementation.login(username, password);
    }

    getCurrentUser(): Observable<LoginInfo> {
        return this.usedImplementation.getCurrentUser();
    }

    register(email: string, password: string): Observable<LoginInfo> {
        return this.usedImplementation.register(email, password);
    }

    changePassword(username: string, oldPassword: string, newPassword: string): Observable<any> {
        return this.usedImplementation.changePassword(username, oldPassword, newPassword);
    }

}
