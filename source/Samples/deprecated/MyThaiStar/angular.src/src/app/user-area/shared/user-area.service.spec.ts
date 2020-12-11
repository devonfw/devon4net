import { RouterTestingModule } from '@angular/router/testing';
import { TestBed, inject } from '@angular/core/testing';

import { UserAreaService } from './user-area.service';
import { LoginInMemoryService } from '../../backend/login/login-in-memory.service';
import { LoginDataService } from '../../backend/login/login-data-service';
import { SnackBarService } from '../../core/snackService/snackService.service';
import { AuthService } from '../../core/authentication/auth.service';
import { CoreModule } from '../../core/core.module';

describe('UserAreaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        UserAreaService,
        AuthService,
        SnackBarService,
        {provide: LoginDataService, useClass: LoginInMemoryService}],
      imports: [
        CoreModule,
        RouterTestingModule,
      ],
    });
  });

  it('should create', inject([UserAreaService], (service: UserAreaService) => {
    expect(service).toBeTruthy();
  }));
});
