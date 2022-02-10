// DISHES
import { Pagination } from '../../backend/backendModels/interfaces';
export interface DishView {
    dish: PlateView;
    image: { content: string };
    extras: ExtraView[];
    likes: number;
    isfav: boolean;
    categories?: { id: string }[];
}

export interface PlateView {
    id: number;
    name: string;
    description: string;
    price: number;
}

export interface ExtraView {
    id: number;
    name: string;
    price: number;
    selected?: boolean;
}

export interface DishResponse {
    pagination: Pagination;
    result: DishView;
}

// BOOKING
export interface ReservationView {
    booking: BookingView;
    invitedGuests?: FriendsInvite[];
}

export interface BookingView {
    bookingDate: string;
    name: string;
    email: string;
    assistants?: number;
    tableId?: number;
    bookingToken?: number;
    creationDate?: string;
}

export interface FriendsInvite {
    email: string;
    accepted: boolean;
}

export interface OrderView {
    dish: {
        id: number;
        name: string;
        price: number;
    };
    orderLine: {
        amount: number;
        comment: string;
    };
    extras: ExtraView[];
}

export interface OrderListView {
    orderLines: OrderView[];
    booking: BookingView;
}

// Interface to recieve responeses from the server using httpclient for getReservations
export interface BookingResponse {
    pagination: Pagination;
    result: ReservationView;
}

// Interface to recieve responeses from the server using httpclient for get orders
export interface OrderResponse {
    pagination: Pagination;
    result: OrderListView;
}

// Interface to recieve responeses from the server using httpclient for email invitations
export interface InvitationResponse {
    id: number;
    modificationCounter: number;
    accepted: boolean;
    guestToken: string;
    modificationDate: any;
    revision: any;
}

// Not sure if this should just use bookingview interface, just in case Im creating a new interface that extends booking view
export interface BookingTableResponse extends BookingView {
    bookingType: string;
    canceled: false;
    comment: string;
    expeditionDate: string;
    id: number;
    modificationCounter: number;
    orderId: number;
    revision: any;
    userId: number;
}

// Interface to recieve responeses from the server using httpclient for SaveOrders
export interface SaveOrderResponse {
    bookingId: number;
    bokingToken: string;
    hostId: number;
    id: number;
    invitedGuestId: number;
    modificationCounter: number;
    revision: any;
}
