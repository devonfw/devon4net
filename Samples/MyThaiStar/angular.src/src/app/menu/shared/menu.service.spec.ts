import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { DishView, OrderView } from '../../shared/viewModels/interfaces';
import { MenuService } from './menu.service';
import { DishesInMemoryService } from '../../backend/dishes/dishes-in-memory.service';
import { DishesDataService } from '../../backend/dishes/dishes-data-service';

describe('MenuService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpModule,
      ],
      providers: [
        MenuService,
        {provide: DishesDataService, useClass: DishesInMemoryService}],
    });
  });

  it('should create', inject([MenuService], (service: MenuService) => {
    expect(service).toBeTruthy();
  }));
});
