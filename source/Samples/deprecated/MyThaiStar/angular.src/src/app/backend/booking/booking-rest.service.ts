import { environment } from './../../../environments/environment';
import { Injectable, Injector } from '@angular/core';
import { Response, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { IBookingDataService } from './booking-data-service-interface';
import { BookingInfo, FilterCockpit } from '../backendModels/interfaces';
import {
    BookingResponse,
    BookingTableResponse,
    InvitationResponse,
    ReservationView,
} from '../../shared/viewModels/interfaces';
import { config } from '../../config';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BookingRestService implements IBookingDataService {

  private readonly booktableRestPath: string = 'bookingmanagement/v1/booking';
  private readonly acceptReserveRestPath: string = 'bookingmanagement/v1/invitedguest/accept/';
  private readonly rejectReserveRestPath: string = 'bookingmanagement/v1/invitedguest/decline/';
  private readonly cancelReserveRestPath: string = 'bookingmanagement/v1/booking/cancel/';
  private readonly getReservationsRestPath: string = 'bookingmanagement/v1/booking/search';

  // private http: HttpClientService;
  // private http: HttpClient;

  constructor(private http: HttpClient) {}

  bookTable(booking: BookingInfo): Observable < BookingTableResponse > {
    return this.http.post(`${environment.restServiceRoot}${this.booktableRestPath}`, booking)
      .map((res: BookingTableResponse) => res);

  }

  getReservations(filter: FilterCockpit): Observable < BookingResponse[] > {
    return this.http.post(`${environment.restServiceRoot}${this.getReservationsRestPath}`, filter)
      .map((res: BookingResponse[]) => res);
  }

  acceptInvite(token: string): Observable < InvitationResponse > {
    return this.http.get(`${environment.restServiceRoot}${this.acceptReserveRestPath}` + token)
      .map((res: InvitationResponse) => res);

  }

  cancelInvite(token: string): Observable < InvitationResponse > {
    return this.http.get(`${environment.restServiceRoot}${this.rejectReserveRestPath}` + token)
      .map((res: InvitationResponse) => res);

  }

  cancelReserve(token: string): Observable < InvitationResponse > {
    return this.http.get(`${environment.restServiceRoot}${this.cancelReserveRestPath}` + token)
      .map((res: InvitationResponse) => res);

  }

}
