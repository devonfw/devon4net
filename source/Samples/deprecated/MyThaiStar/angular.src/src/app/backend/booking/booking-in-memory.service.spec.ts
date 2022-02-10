import { TestBed, inject } from '@angular/core/testing';

import { BookingInMemoryService } from './booking-in-memory.service';

describe('BookingInMemoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BookingInMemoryService],
    });
  });

  it('should create', inject([BookingInMemoryService], (service: BookingInMemoryService) => {
    expect(service).toBeTruthy();
  }));
});
