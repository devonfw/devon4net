import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable, Injector } from '@angular/core';
import { Response, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { IOrderDataService } from './order-data-service-interface';
import { FilterCockpit, OrderListInfo } from '../backendModels/interfaces';
import { OrderListView, OrderResponse, SaveOrderResponse } from '../../shared/viewModels/interfaces';
import { config } from '../../config';

@Injectable()
export class OrderRestService implements IOrderDataService {

  private readonly getOrdersRestPath: string = 'ordermanagement/v1/order/search';
  private readonly filterOrdersRestPath: string = 'ordermanagement/v1/order/search';
  private readonly cancelOrderRestPath: string = 'ordermanagement/v1/order/cancelorder/';
  private readonly saveOrdersPath: string = 'ordermanagement/v1/order';

  constructor(private http: HttpClient) { }

  getBookingOrders(filter: FilterCockpit): Observable<OrderResponse[]> {
    let path: string;
    if (filter.email || filter.bookingToken) {
      path = this.filterOrdersRestPath;
    } else {
      delete filter.email;
      delete filter.bookingToken;
      path = this.getOrdersRestPath;
    }
    return this.http.post(`${environment.restServiceRoot}${path}`, filter)
      .map((res: OrderResponse[]) => res);
  }

  saveOrders(orders: OrderListInfo): Observable<SaveOrderResponse> {
    return this.http.post(`${environment.restServiceRoot}${this.saveOrdersPath}`, orders)
      .map((res: SaveOrderResponse) => res);
  }

  cancelOrder(token: string): Observable<boolean> {
    return this.http.get(`${environment.restServiceRoot}${this.cancelOrderRestPath}` + token)
      .map((res: Response) => true);
  }

}
