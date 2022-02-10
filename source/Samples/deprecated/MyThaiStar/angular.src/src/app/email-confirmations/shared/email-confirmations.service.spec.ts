import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed, inject } from '@angular/core/testing';
import { HttpModule } from '@angular/http';
import { BookingInMemoryService } from '../../backend/booking/booking-in-memory.service';
import { EmailConfirmationsService } from './email-confirmations.service';
import { OrderInMemoryService } from '../../backend/order/order-in-memory.service';
import { OrderDataService } from '../../backend/order/order-data-service';
import { BookingDataService } from '../../backend/booking/booking-data-service';

describe('EmailConfirmationsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpModule, HttpClientModule],
      providers: [
        EmailConfirmationsService,
        HttpClient,
        { provide: BookingDataService, useClass: BookingInMemoryService },
        { provide: OrderDataService, useClass: OrderInMemoryService }],
    });
  });

  it('should create', inject([EmailConfirmationsService], (service: EmailConfirmationsService) => {
    expect(service).toBeTruthy();
  }));
});
