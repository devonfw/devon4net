import { TestBed, inject } from '@angular/core/testing';

import { OrderInMemoryService } from './order-in-memory.service';

describe('OrderInMemoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OrderInMemoryService],
    });
  });

  it('should create', inject([OrderInMemoryService], (service: OrderInMemoryService) => {
    expect(service).toBeTruthy();
  }));
});
