import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { WindowService } from './windowService.service';

describe('WindowService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpModule],
      providers: [WindowService],
    });
  });

  it('should create', inject([WindowService], (service: WindowService) => {
    expect(service).toBeTruthy();
  }));
});
