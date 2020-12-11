import { TestBed, inject } from '@angular/core/testing';

import { LoginInMemoryService } from './login-in-memory.service';

describe('LoginInMemoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoginInMemoryService],
    });
  });

  it('should create', inject([LoginInMemoryService], (service: LoginInMemoryService) => {
    expect(service).toBeTruthy();
  }));
});
