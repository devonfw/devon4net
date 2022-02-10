import { Observable } from 'rxjs/Observable';
import { OrderListView, OrderResponse, SaveOrderResponse } from '../../shared/viewModels/interfaces';
import { FilterCockpit, OrderListInfo } from '../backendModels/interfaces';

export interface IOrderDataService {

    getBookingOrders(filter: FilterCockpit): Observable<OrderResponse[]>;
    saveOrders(orders: OrderListInfo): Observable<SaveOrderResponse>;
    cancelOrder(token: string): Observable<boolean>;
}
