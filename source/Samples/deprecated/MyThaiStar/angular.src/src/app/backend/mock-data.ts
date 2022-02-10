import { ExtraView, OrderListView, ReservationView } from '../shared/viewModels/interfaces';
import { LoginInfo, Role } from './backendModels/interfaces';
import { DishView } from '../shared/viewModels/interfaces';

export const extras: ExtraView[] = [{
                id: 0,
                name: 'Tofu',
                price: 1,
                selected: false,
        }, {
                id: 1,
                name: 'Chiken',
                price: 1,
                selected: false,
        }, {
                id: 2,
                name: 'Pork',
                price: 2,
                selected: false,
        }];

export const dishes: DishView[] = [{
                dish: {
                        id: 0,
                        description:
                        'Lorem ipsum dolor sit amet. Proin fermentum lobortis neque. ' +
                        'Pellentesque habitant morbi tristique.',
                        name: 'Red Curry',
                        price: 5.90,
                },
                isfav: false,
                image: {content: '../../../assets/images/basil-fried.jpg'},
                likes: 21,
                extras: [
                        { id: 0, name: 'Tofu', price: 1, selected: false },
                        { id: 1, name: 'Chiken', price: 1, selected: false },
                        { id: 2, name: 'Pork', price: 2, selected: false }],
                categories: [
                        {id: '0'},
                        {id: '3'},
                        {id: '5'},
                        {id: '6'},
                        {id: '7'}],
        }, {
                dish: {
                        id: 1,
                        description:
                                'Consectetur adipiscing elit. Nulla id viverra turpis, sed eleifend dui. ' +
                                'Proin fermentum lobortis neque. Pellentesque habitant morbi tristique.',
                        name: 'Purple Curry',
                        price: 9.00,
                },
                isfav: false,
                image: {content: '../../../assets/images/garlic-paradise.jpg'},
                likes: 10,
                extras: [
                        { id: 0, name: 'Tofu', price: 1, selected: false },
                        { id: 1, name: 'Chiken', price: 1, selected: false },
                        { id: 2, name: 'Pork', price: 2, selected: false }],
                categories: [
                        {id: '1'},
                        {id: '6'}],
        }, {
                dish: {
                        id: 2,
                        description:
                                'Lorem ipsum dolor sit amet, consectetur adipiscing elit. ' +
                                'Nulla id viverra turpis, sed eleifend dui. Proin fermentum lobortis neque.',
                        name: 'Green Curry',
                        price: 7.60,
                },
                isfav: false,
                image: {content: '../../../assets/images/green-curry.jpg'},
                likes: 61,
                extras: [
                        { id: 0, name: 'Tofu', price: 1, selected: false },
                        { id: 1, name: 'Chiken', price: 1, selected: false },
                        { id: 2, name: 'Pork', price: 2, selected: false }],
                categories: [
                        {id: '2'},
                        {id: '4'},
                        {id: '6'}],
        }, {
                dish: {
                        id: 3,
                        description: 'Lorem ipsum dolor. Pellentesque habitant morbi tristique.',
                        name: 'Yellow Curry',
                        price: 8.50,
                },
                isfav: false,
                image: {content: '../../../assets/images/dish.png'},
                likes: 48,
                extras: [
                        { id: 0, name: 'Tofu', price: 1, selected: false },
                        { id: 1, name: 'Chiken', price: 1, selected: false },
                        { id: 2, name: 'Pork', price: 2, selected: false }],
                categories: [
                        {id: '1'},
                        {id: '4'},
                        {id: '7'}],
        }];

export let currentUser: LoginInfo[] = [];

export const users: LoginInfo[] = [{
                username: 'user',
                password: 'pass',
                role: 'user',
        }, {
                username: 'waiter',
                password: 'pass',
                role: 'waiter',
        }];

export const roles: Role[] = [
        {name: 'user', permission: 0},
        {name: 'waiter', permission: 1},
];

export const bookedTables: ReservationView[] = [{
                booking: {
                        assistants: 3,
                        bookingDate: '19/03/2017 22:00',
                        name: 'Brok',
                        email: 'email1@email.com',
                        tableId: 0,
                        creationDate: '11/03/2017 12:45',
                        bookingToken: 500,
                },
                invitedGuests: [{email: 'emailFriend1@email.com', accepted: true},
                                {email: 'emailFriend2@email.com', accepted: true},
                                {email: 'emailFriend3@email.com', accepted: false}],
        }, {
                booking: {
                        bookingDate: '13/03/2017 21:45',
                        name: 'Jesse',
                        email: 'email2@email.com',
                        assistants: 2,
                        tableId: 1,
                        creationDate: '17/03/2017 23:30',
                        bookingToken: 501,
                },
                invitedGuests: [{email: 'emailFriend1@email.com', accepted: true},
                          {email: 'emailFriend2@email.com', accepted: false}],
        }, {
                booking: {
                        bookingDate: '15/03/2017 21:00',
                        name: 'James',
                        email: 'email3@email.com',
                        assistants: 4,
                        tableId: 2,
                        creationDate: '17/03/2017 17:12',
                        bookingToken: 502,
                },
                invitedGuests: [],
        }, {
                booking: {
                        bookingDate: '16/03/2017 20:45',
                        name: 'Mara',
                        email: 'email4@email.com',
                        assistants: 1,
                        tableId: 3,
                        creationDate: '17/03/2017 18:45',
                        bookingToken: 503,
                },
                invitedGuests: [{email: 'emailFriend1@email.com', accepted: true},
                                {email: 'emailFriend2@email.com', accepted: true},
                                {email: 'emailFriend3@email.com', accepted: false},
                                {email: 'emailFriend4@email.com', accepted: false},
                                {email: 'emailFriend5@email.com', accepted: true}],
                }];

export const orderList: OrderListView[] = [{
                booking: {
                        bookingToken: 500,
                        name: 'Name 1',
                        bookingDate: '13/03/2017 15:00',
                        creationDate: '10/03/2017 10:00',
                        email: 'user1@mail.com',
                        tableId: 0,
                },
                orderLines: [{
                        dish: {
                                id: 0,
                                name: 'Pad Kee Mao',
                                price: 5.90,
                        },
                        orderLine: {
                                amount: 1,
                                comment: 'Hello mom!',
                        },
                        extras: [{id: 1, name: 'Chicken', price: 2, selected: true}],
                }, {
                        dish: {
                                id: 1,
                                name: 'Red Curry',
                                price: 5.90,
                        },
                        orderLine:  {
                                amount: 1,
                                comment: 'I want it really red',
                        },
                        extras: [],
                        }],
        }, {
                booking: {
                        bookingToken: 501,
                        name: 'Name 2',
                        bookingDate: '27/05/2017 22:00',
                        creationDate: '12/05/2017 23:00',
                        email: 'user2@mail.com',
                        tableId: 1,
                },
                orderLines: [{
                        dish: {
                                id: 1,
                                name: 'Red Curry',
                                price: 5.90,
                        },
                        orderLine: {
                        amount: 1,
                        comment: 'I hope this curry worths the price',
                        },
                        extras: [{id: 2, name: 'Pork', price: 1, selected: true},
                                {id: 0, name: 'Tofu', price: 1, selected: true},
                                {id: 1, name: 'Chicken', price: 2, selected: true}],
                }, {
                        dish: {
                                id: 1,
                                name: 'Red Curry',
                                price: 5.90,
                        },
                        orderLine: {
                                amount: 1,
                                comment: 'hot sauce',
                        },
                        extras: [{id: 2, name: 'Pork', price: 1, selected: true}],
                }],
        }, {
                booking: {
                       bookingToken: 502,
                        name: 'user 3',
                        bookingDate: '29/05/2017 21:00',
                        creationDate: '29/05/2017 10:00',
                        email: 'user0@mail.com',
                        tableId: 2,
                },
                orderLines: [{
                        dish: {
                                id: 1,
                                name: 'Red Curry',
                                price: 5.90,
                        },
                        orderLine: {
                                amount: 1,
                                comment: 'it would be nice if the pork can be well-cooked',
                        },
                        extras: [{id: 2, name: 'Pork', price: 1, selected: true},
                                {id: 0, name: 'Tofu', price: 1, selected: true}],
                }],
        }, {
                booking: {
                        bookingToken: 503,
                        name: 'user 4',
                        bookingDate: '27/05/2017 20:30',
                        creationDate: '20/05/2017 17:00',
                        email: 'user4@mail.com',
                        tableId: 3,
                },
                orderLines: [{
                        dish: {
                                id: 3,
                                name: 'Brown Curry',
                                price: 5.40,
                        },
                        orderLine: {
                                amount: 1,
                                comment: '',
                        },
                        extras: [],
                }, {
                        dish: {
                                id: 5,
                                name: 'Yellow Curry',
                                price: 8.20,
                        },
                        orderLine: {
                                amount: 1,
                                comment: '',
                        },
                        extras: [{id: 1, name: 'Chicken', price: 1, selected: true}],
                }, {
                        dish: {
                                id: 4,
                                name: 'Purple Curry',
                                price: 6.70,
                        },
                        orderLine: {
                                amount: 2,
                                comment: 'one without tomatoe',
                        },
                        extras: [],
                }, {
                        dish: {
                                id: 2,
                                name: 'Green Curry',
                                price: 7.90,
                        },
                        orderLine: {
                                amount: 1,
                                comment: '',
                        },
                        extras: [{id: 0, name: 'Tofu', price: 1, selected: true}],
                }],
        },
        ];
