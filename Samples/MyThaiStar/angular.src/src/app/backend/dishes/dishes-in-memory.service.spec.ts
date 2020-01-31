import { TestBed, inject } from '@angular/core/testing';

import { DishesInMemoryService } from './dishes-in-memory.service';

describe('DishesInMemoryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DishesInMemoryService],
    });
  });

  it('should create', inject([DishesInMemoryService], (service: DishesInMemoryService) => {
    expect(service).toBeTruthy();
  }));
});
