import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { IBookingDataService } from './booking-data-service-interface';
import { ReservationView } from '../../shared/viewModels/interfaces';
import { BookingInfo, FilterCockpit } from '../backendModels/interfaces';
import { bookedTables } from '../mock-data';
import * as moment from 'moment';
import { assign, maxBy, filter, toString, orderBy } from 'lodash';

@Injectable()
export class BookingInMemoryService implements IBookingDataService {

    bookTable(booking: BookingInfo): Observable<any> {
        let bookTable: ReservationView;
        bookTable = assign(bookTable, booking);
        bookTable.booking.creationDate = moment().format('LLL');
        bookTable.booking.bookingToken = maxBy(bookedTables, (table: ReservationView) => table.booking.bookingToken).booking.bookingToken + 1;
        bookTable.booking.tableId = maxBy(bookedTables, (table: ReservationView) => table.booking.tableId).booking.tableId + 1;
        if (!bookTable.invitedGuests) {
            bookTable.invitedGuests = [];
        }
        return Observable.of(bookedTables.push(bookTable));
    }

    getReservations(filters: FilterCockpit): Observable<any> {
        if (!filters.sort[0]) {
            filters.sort = [{ name: '', direction: '' }];
        } else {
            filters.sort = [{ name: filters.sort[0].name, direction: filters.sort[0].direction }];
        }
        return Observable.of({
            pagination: {
                size: filters.pagination.size,
                page: filters.pagination.page,
                total: bookedTables.length,
            },
            result: orderBy(bookedTables, [filters.sort[0].name], [filters.sort[0].direction])
                .filter((booking: ReservationView) => {
                    if (filters.bookingDate) {
                        return booking.booking.bookingDate.toLowerCase().includes(filters.bookingDate.toLowerCase());
                    } else {
                        return true;
                    }
                }).filter((booking: ReservationView) => {
                    if (filters.email) {
                        return booking.booking.email.toLowerCase().includes(filters.email.toLowerCase());
                    } else {
                        return true;
                    }
                }).filter((booking: ReservationView) => {
                    if (filters.bookingToken) {
                        return toString(booking.booking.bookingToken).includes(toString(filters.bookingToken));
                    } else {
                        return true;
                    }
                }),
        });
    }

    acceptInvite(token: string): Observable<any> {
        return Observable.of(1);
    }

    cancelInvite(token: string): Observable<any> {
        return Observable.of(1);
    }

    cancelReserve(token: string): Observable<any> {
        return Observable.of(1);
    }

}
