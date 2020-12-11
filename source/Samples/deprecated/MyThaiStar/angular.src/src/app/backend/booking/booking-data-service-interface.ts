import { Observable } from 'rxjs/Observable';
import {
    BookingResponse,
    BookingTableResponse,
    InvitationResponse,
    ReservationView,
} from '../../shared/viewModels/interfaces';
import { BookingInfo, FilterCockpit } from '../backendModels/interfaces';

export interface IBookingDataService {

    getReservations(filter: FilterCockpit): Observable<BookingResponse[]>;
    bookTable(booking: BookingInfo): Observable<BookingTableResponse>;
    acceptInvite(token: string): Observable<InvitationResponse>;
    cancelInvite(token: string): Observable<InvitationResponse>;
    cancelReserve(token: string): Observable<InvitationResponse>;
}
