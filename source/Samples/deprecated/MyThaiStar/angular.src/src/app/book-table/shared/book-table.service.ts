import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { FriendsInvite, ReservationView } from '../../shared/viewModels/interfaces';
import { BookingDataService } from '../../backend/booking/booking-data-service';
import { BookingInfo, ReservationInfo } from '../../backend/backendModels/interfaces';
import { map, assign, forEach } from 'lodash';

@Injectable()
export class BookTableService {

  constructor(private bookingDataService: BookingDataService) {
  }

  postBooking(bookInfo: BookingInfo): Observable<any> {
    return this.bookingDataService.bookTable(bookInfo);
  }

  composeBooking(invitationData: any, type: number): BookingInfo {
    let composedBooking: BookingInfo = {
      booking: {
        bookingDate: invitationData.bookingDate,
        name: invitationData.name,
        email: invitationData.email,
        bookingType: type,
      },
    };

    if (type) {
      composedBooking.invitedGuests = map(invitationData.invitedGuests, (guest: FriendsInvite) => { return {email : guest}; } );
    } else {
      composedBooking.booking.assistants = invitationData.assistants;
    }

    return composedBooking;
  }

}
