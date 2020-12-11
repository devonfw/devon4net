import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Injector, Injectable } from '@angular/core';
import { BackendType, BackendConfig } from './../../../app/config';
import { OrderInMemoryService } from './order-in-memory.service';
import { OrderRestService } from './order-rest.service';
import { IOrderDataService } from './order-data-service-interface';
import { OrderListView, OrderResponse, SaveOrderResponse } from '../../shared/viewModels/interfaces';
import { FilterCockpit, OrderListInfo } from '../backendModels/interfaces';

@Injectable()
export class OrderDataService implements IOrderDataService {

    private usedImplementation: IOrderDataService;

    constructor(private injector: Injector, private http: HttpClient) {
        const backendConfig: BackendConfig = this.injector.get(BackendConfig);
        if (backendConfig.environmentType === BackendType.IN_MEMORY) {
            this.usedImplementation = new OrderInMemoryService();
        } else { // default
            this.usedImplementation = new OrderRestService(http);
        }
    }

    getBookingOrders(filter: FilterCockpit): Observable<OrderResponse[]> {
        return this.usedImplementation.getBookingOrders(filter);
    }

    saveOrders(orders: OrderListInfo): Observable<SaveOrderResponse> {
        return this.usedImplementation.saveOrders(orders);
    }

    cancelOrder(token: string): Observable<boolean> {
         return this.usedImplementation.cancelOrder(token);
     }
}
