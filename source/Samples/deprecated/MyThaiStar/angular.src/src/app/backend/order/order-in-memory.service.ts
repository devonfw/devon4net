import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { IOrderDataService } from './order-data-service-interface';
import { ReservationView, DishView, ExtraView, OrderListView } from '../../shared/viewModels/interfaces';
import { FilterCockpit, OrderInfo, OrderListInfo } from '../backendModels/interfaces';
import { bookedTables, extras, dishes, orderList } from '../mock-data';
import * as moment from 'moment';
import { find, filter, toString, toNumber, orderBy } from 'lodash';

@Injectable()
export class OrderInMemoryService implements IOrderDataService {

    getBookingOrders(filters: FilterCockpit): Observable<any> {
        if (!filters.sort[0]) {
            filters.sort = [{ name: '', direction: '' }];
        } else {
            filters.sort = [{ name: filters.sort[0].name, direction: filters.sort[0].direction }];
        }
        return Observable.of({
            pagination: {
                size: filters.pagination.size,
                page: filters.pagination.page,
                total: orderList.length,
            },
            result: orderBy(orderList, [filters.sort[0].name], [filters.sort[0].direction])
                .filter((order: OrderListView) => {
                    if (filters.bookingDate) {
                        return order.booking.bookingDate.toLowerCase().includes(filters.bookingDate.toLowerCase());
                    } else {
                        return true;
                    }
                }).filter((order: OrderListView) => {
                    if (filters.email) {
                        return order.booking.email.toLowerCase().includes(filters.email.toLowerCase());
                    } else {
                        return true;
                    }
                }).filter((order: OrderListView) => {
                    if (filters.bookingToken) {
                        return toString(order.booking.bookingToken).includes(toString(filters.bookingToken));
                    } else {
                        return true;
                    }
                }),
        });
    }

    saveOrders(order: OrderListInfo): Observable<any> {
        return Observable.of(orderList.push(this.composeOrderList(order)));
    }

    findExtraById(id: number): ExtraView {
        return find(extras, (extra: ExtraView) => extra.id === id);
    }

    findDishById(id: number): DishView {
        return find(dishes, (plate: DishView) => plate.dish.id === id);
    }

    findReservationById(id: { bookingToken: string }): ReservationView {
        return find(bookedTables, (booking: ReservationView) => booking.booking.bookingToken === toNumber(id.bookingToken));
    }

    composeOrderList(orders: OrderListInfo): OrderListView {
        let composedOrders: OrderListView;
        let orderLines: any = [];
        orders.orderLines.forEach((order: OrderInfo) => {
            let plate: DishView = this.findDishById(order.orderLine.dishId);
            let _extras: ExtraView[] = [];
            order.extras.forEach((extraId: any) => {
                _extras.push(this.findExtraById(extraId.id));
            });
            orderLines.push({
                dish: {
                    dishId: order.orderLine.dishId,
                    name: plate.dish.name,
                    price: plate.dish.price,
                },
                orderLine: {
                    comment: order.orderLine.comment,
                    amount: order.orderLine.amount,
                },
                extras: _extras,
            });
        });

        let bookedTable: ReservationView = this.findReservationById(orders.booking);

        return {
            booking: {
                bookingToken: toNumber(orders.booking.bookingToken),
                name: bookedTable.booking.name,
                bookingDate: bookedTable.booking.bookingDate,
                creationDate: bookedTable.booking.creationDate,
                email: bookedTable.booking.email,
                tableId: bookedTable.booking.tableId,
            },
            orderLines: orderLines,
        };
    }

    cancelOrder(token: string): Observable<boolean> {
        return Observable.of(true);
    }

}
