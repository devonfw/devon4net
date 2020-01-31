import { BackendType, BackendConfig } from './../../../app/config';
import { Observable } from 'rxjs/Observable';
import { Injector, Injectable } from '@angular/core';
import { BookingInMemoryService } from './booking-in-memory.service';
import { BookingRestService } from './booking-rest.service';
import { IBookingDataService } from './booking-data-service-interface';
import {
    BookingResponse,
    BookingTableResponse,
    InvitationResponse,
    ReservationView,
} from '../../shared/viewModels/interfaces';
import { BookingInfo, FilterCockpit } from '../backendModels/interfaces';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BookingDataService implements IBookingDataService {

    private usedImplementation: IBookingDataService;

    constructor(private injector: Injector, private http: HttpClient) {
        const backendConfig: BackendConfig = this.injector.get(BackendConfig);
        if (backendConfig.environmentType === BackendType.IN_MEMORY) {
            this.usedImplementation = new BookingInMemoryService();
        } else { // default
            this.usedImplementation = new BookingRestService(http);
        }
    }

    bookTable(booking: BookingInfo): Observable<BookingTableResponse> {
        return this.usedImplementation.bookTable(booking);
    }

    getReservations(filter: FilterCockpit): Observable<BookingResponse[]> {
        return this.usedImplementation.getReservations(filter);
    }

    acceptInvite(token: string): Observable<InvitationResponse> {
        return this.usedImplementation.acceptInvite(token);
    }

    cancelInvite(token: string): Observable<InvitationResponse> {
        return this.usedImplementation.cancelInvite(token);
    }

    cancelReserve(token: string): Observable<InvitationResponse> {
        return this.usedImplementation.cancelReserve(token);
    }
}
