import { TestBed, inject } from '@angular/core/testing';

import { ExtraView, OrderView } from '../../shared/viewModels/interfaces';
import { PriceCalculatorService } from './price-calculator.service';

describe('PriceCalculatorService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PriceCalculatorService],
    });
  });

  it('should be properly injected', inject([PriceCalculatorService], (service: PriceCalculatorService) => {
    expect(service).toBeTruthy();
  }));

  describe('check getPrice method', () => {

    it('should calculate price for single order without extras', inject([PriceCalculatorService], (service: PriceCalculatorService) => {
      const order: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order without extras',
        },
        orderLine: {
          comment: '',
          amount: 1,
        },
        extras: [],
      };

      expect(service.getPrice(order)).toEqual(order.dish.price);
    }));

    it('should calculate price for single order with single extra ingredient selected',
        inject([PriceCalculatorService], (service: PriceCalculatorService) => {
          const extraRice: ExtraView = {
              id: 0,
              name: 'Rice',
              price: 2.00,
              selected: true,
          };
          const order: OrderView = {
            dish: {
              id: 0,
              price: 12.50,
              name: 'Order with extra rice',
            },
            orderLine: {
              comment: '',
              amount: 1,
            },
            extras: [extraRice],
          };

          expect(service.getPrice(order)).toEqual(order.dish.price + extraRice.price);
    }));

    it('should calculate price for single order with multiple extra ingredients selected',
        inject([PriceCalculatorService], (service: PriceCalculatorService) => {
      const extraRice: ExtraView = {
          id: 0,
          name: 'Rice',
          price: 2.00,
          selected: true,
      };
      const extraTofu: ExtraView = {
          id: 1,
          name: 'Tofu',
          price: 3.00,
          selected: true,
      };
      const order: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order with extra rice and tofu',
        },
        orderLine: {
          comment: '',
          amount: 1,
        },
        extras: [extraRice, extraTofu],
      };

      expect(service.getPrice(order)).toEqual(order.dish.price + extraRice.price + extraTofu.price);
    }));

    it('should calculate price for multiple orders without extras', inject([PriceCalculatorService], (service: PriceCalculatorService) => {
      const order: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order without extras',
        },
        orderLine: {
          comment: '',
          amount: 4,
        },
        extras: [],
      };

      expect(service.getPrice(order)).toEqual(order.dish.price * order.orderLine.amount);
    }));

    it('should calculate price for multiple orders with single extra ingredient selected',
      inject([PriceCalculatorService], (service: PriceCalculatorService) => {
      const extraRice: ExtraView = {
          id: 0,
          name: 'Rice',
          price: 2.00,
          selected: true,
      };
      const order: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order with extra rice',
        },
        orderLine: {
          comment: '',
          amount: 4,
        },
        extras: [extraRice],
      };

      expect(service.getPrice(order)).toEqual((order.dish.price + extraRice.price) * order.orderLine.amount);
    }));

    it('should calculate price for multiple orders with multiple extra ingredients selected',
        inject([PriceCalculatorService], (service: PriceCalculatorService) => {
      const extraRice: ExtraView = {
          id: 0,
          name: 'Rice',
          price: 2.00,
          selected: true,
      };
      const extraTofu: ExtraView = {
          id: 1,
          name: 'Tofu',
          price: 3.00,
          selected: true,
      };
      const order: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order with extra rice and tofu',
        },
        orderLine: {
          comment: '',
          amount: 3,
        },
        extras: [extraRice, extraTofu],
      };

      expect(service.getPrice(order)).toEqual((order.dish.price + extraRice.price + extraTofu.price) * order.orderLine.amount);
    }));

  });
  describe('check getTotalPrice method', () => {
    it('should calculate price for mutliple orders of different kind without extras',
        inject([PriceCalculatorService], (service: PriceCalculatorService) => {

      const orderWithoutExtras: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order without extras',
        },
        orderLine : {
          comment: '',
          amount: 1,
        },
        extras: [],
      };
      const expensiveOrderWithoutExtras: OrderView = {
        dish: {
          id: 0,
          price: 15.50,
          name: 'Expensive order without extras',
        },
        orderLine: {
          comment: '',
          amount: 1,
        },
        extras: [],
      };

      const orders: OrderView[] = [orderWithoutExtras, expensiveOrderWithoutExtras];

      expect(service.getTotalPrice(orders))
        .toEqual(orderWithoutExtras.dish.price + expensiveOrderWithoutExtras.dish.price * expensiveOrderWithoutExtras.orderLine.amount);
    }));

    it('should calculate price for mutliple orders of different kind with extras',
        inject([PriceCalculatorService], (service: PriceCalculatorService) => {

      const extraRice: ExtraView = {
          id: 0,
          name: 'Rice',
          price: 2.00,
          selected: true,
      };
      const extraTofu: ExtraView = {
          id: 1,
          name: 'Tofu',
          price: 3.00,
          selected: true,
      };

      const order: OrderView = {
        dish: {
          id: 0,
          price: 12.50,
          name: 'Order',
        },
        orderLine: {
          comment: '',
          amount: 2,
        },
        extras: [extraRice, extraTofu],
      };
      const expensiveOrder: OrderView = {
        dish: {
          id: 0,
          price: 15.50,
          name: 'Expensive order',
        },
        orderLine: {
          comment: '',
          amount: 1,
        },
        extras: [extraTofu],
      };

      const orders: OrderView[] = [order, expensiveOrder];

      expect(service.getTotalPrice(orders))
        .toEqual((order.dish.price + extraRice.price + extraTofu.price) * order.orderLine.amount
        + (expensiveOrder.dish.price + extraTofu.price) * expensiveOrder.orderLine.amount);
    }));
  });
});
