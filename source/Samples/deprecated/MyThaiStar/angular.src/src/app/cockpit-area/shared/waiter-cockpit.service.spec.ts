import { TestBed, inject } from '@angular/core/testing';

import { OrderView } from '../../shared/viewModels/interfaces';
import { OrderInMemoryService } from '../../backend/order/order-in-memory.service';
import { OrderDataService } from '../../backend/order/order-data-service';
import { BookingInMemoryService } from '../../backend/booking/booking-in-memory.service';
import { BookingDataService } from '../../backend/booking/booking-data-service';
import { PriceCalculatorService } from '../../sidenav/shared/price-calculator.service';
import { WaiterCockpitService } from './waiter-cockpit.service';

describe('WaiterCockpitService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        WaiterCockpitService,
        PriceCalculatorService,
        {provide: BookingDataService, useClass: BookingInMemoryService},
        {provide: OrderDataService, useClass: OrderInMemoryService}],
    });
  });

  it('should create', inject([WaiterCockpitService], (service: WaiterCockpitService) => {
    expect(service).toBeTruthy();
  }));

  describe('Form composer', () => {
    it('should compose correctly order info', inject([WaiterCockpitService], (service: WaiterCockpitService) => {

      const orderData: OrderView[] = [{
          dish: {
              id: 0,
              name: 'dish1',
              price: 14.75,
          },
          extras: [{id: 0, price: 1, name: 'Extra Curry'},
                   {id: 1, price: 2, name: 'Extra pork'}],
          orderLine: {
            amount: 2,
            comment: 'comment',
          },
        }, {
          dish: {
              id: 0,
              name: 'dish2',
              price: 12.15,
          },
          extras: [{id: 0, price: 1, name: 'Extra Curry'}],
          orderLine: {
            amount: 1,
            comment: '',
          },
        }];

      const orderResult: any = [{
          dish: {
              id: 0,
              name: 'dish1',
              price: (14.75 + 1 + 2) * 2, // 2 dishes + 1 extra curry + 2 extra pork
          },
          extras: 'Extra Curry, Extra pork',
          orderLine: {
            amount: 2,
            comment: 'comment',
          },
        }, {
          dish: {
              id: 0,
              name: 'dish2',
              price: 12.15 + 1, // 1 extra curry
          },
          extras: 'Extra Curry',
          orderLine: {
            amount: 1,
            comment: '',
          },
        }];

      expect(service.orderComposer(orderData)).toEqual(orderResult);
    }));
  });
});
