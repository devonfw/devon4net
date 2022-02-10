import { TestBed, inject } from '@angular/core/testing';

import { SidenavService } from './sidenav.service';
import { BookingInMemoryService } from '../../backend/booking/booking-in-memory.service';
import { BookingDataService } from '../../backend/booking/booking-data-service';
import { OrderInMemoryService } from '../../backend/order/order-in-memory.service';
import { OrderDataService } from '../../backend/order/order-data-service';
import { CoreModule } from '../../core/core.module';

describe('SidenavSharedService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        SidenavService,
        {provide: BookingDataService, useClass: BookingInMemoryService},
        {provide: OrderDataService, useClass: OrderInMemoryService}],
      imports: [
        CoreModule,
      ],
    });
  });

  it('should create', inject([SidenavService], (service: SidenavService) => {
    expect(service).toBeTruthy();
  }));
});
