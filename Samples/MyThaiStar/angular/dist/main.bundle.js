webpackJsonp([1],{

/***/ "../../../../../src async recursive":
/***/ (function(module, exports) {

function webpackEmptyContext(req) {
	throw new Error("Cannot find module '" + req + "'.");
}
webpackEmptyContext.keys = function() { return []; };
webpackEmptyContext.resolve = webpackEmptyContext;
module.exports = webpackEmptyContext;
webpackEmptyContext.id = "../../../../../src async recursive";

/***/ }),

/***/ "../../../../../src/app/app-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__home_home_component__ = __webpack_require__("../../../../../src/app/home/home.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__menu_menu_component__ = __webpack_require__("../../../../../src/app/menu/menu.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__book_table_book_table_component__ = __webpack_require__("../../../../../src/app/book-table/book-table.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__cockpit_area_reservation_cockpit_reservation_cockpit_component__ = __webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__cockpit_area_order_cockpit_order_cockpit_component__ = __webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__core_not_found_not_found_component__ = __webpack_require__("../../../../../src/app/core/not-found/not-found.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__core_authentication_auth_guard_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth-guard.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var appRoutes = [
    { path: '', redirectTo: '/restaurant', pathMatch: 'full' },
    { path: 'restaurant', component: __WEBPACK_IMPORTED_MODULE_2__home_home_component__["a" /* HomeComponent */] },
    { path: 'menu', component: __WEBPACK_IMPORTED_MODULE_3__menu_menu_component__["a" /* MenuComponent */] },
    { path: 'bookTable', component: __WEBPACK_IMPORTED_MODULE_4__book_table_book_table_component__["a" /* BookTableComponent */] },
    { path: 'orders', component: __WEBPACK_IMPORTED_MODULE_6__cockpit_area_order_cockpit_order_cockpit_component__["a" /* OrderCockpitComponent */], canActivate: [__WEBPACK_IMPORTED_MODULE_8__core_authentication_auth_guard_service__["a" /* AuthGuardService */]] },
    { path: 'reservations', component: __WEBPACK_IMPORTED_MODULE_5__cockpit_area_reservation_cockpit_reservation_cockpit_component__["a" /* ReservationCockpitComponent */], canActivate: [__WEBPACK_IMPORTED_MODULE_8__core_authentication_auth_guard_service__["a" /* AuthGuardService */]] },
    { path: '**', component: __WEBPACK_IMPORTED_MODULE_7__core_not_found_not_found_component__["a" /* NotFoundComponent */] }
];
var AppRoutingModule = (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forRoot(appRoutes, { enableTracing: true }),
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */],
            ],
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());

//# sourceMappingURL=app-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<td-layout-nav>\r\n  <public-header (openCloseSidenavMobile)=\"mobileSidenavOpened = !mobileSidenavOpened\" td-toolbar-content flex></public-header>\r\n\r\n  <td-layout-footer class=\"forDesktop\">\r\n    <div layout=\"row\" layout-align=\"center center\" flex>\r\n      <span>MY THAI STAR 2017</span>\r\n      <span flex></span>\r\n      <span>Devonfw</span>\r\n    </div>\r\n  </td-layout-footer>\r\n\r\n  <md-sidenav-container style= \"height:100%\">\r\n\r\n    <md-sidenav class=\"sidenavMobile\" (close)=\"openCloseSideNav(true)\" [opened]=\"sidenav.opened\" align=\"end\">\r\n      <public-sidenav></public-sidenav>\r\n    </md-sidenav>\r\n\r\n    <md-sidenav style=\"width:75%\" (close)=\"mobileSidenavOpened=false\" [opened]=\"mobileSidenavOpened\" #mobilesidenav>\r\n      <md-nav-list menu-items *ngIf=\"auth.isPermited('CUSTOMER')\">\r\n        <a md-list-item (click)=\"navigateTo('restaurant')\">\r\n          <md-icon>home</md-icon>\r\n          HOME\r\n        </a>\r\n        <a md-list-item (click)=\"navigateTo('menu')\">\r\n          <md-icon>restaurant_menu</md-icon>\r\n          MENU\r\n        </a>\r\n        <a md-list-item (click)=\"navigateTo('bookTable')\">\r\n          <md-icon>bookmark</md-icon>\r\n          BOOK TABLE\r\n        </a>\r\n      </md-nav-list>\r\n      <md-nav-list menu-items *ngIf=\"auth.isPermited('WAITER')\">\r\n        <a md-list-item (click)=\"navigateTo('orders')\">\r\n          <md-icon>home</md-icon>\r\n          ORDERS\r\n        </a>\r\n        <a md-list-item (click)=\"navigateTo('reservations')\">\r\n          <md-icon>restaurant_menu</md-icon>\r\n          RESERVATIONS\r\n        </a>\r\n      </md-nav-list>\r\n    </md-sidenav>\r\n\r\n    <router-outlet></router-outlet>\r\n\r\n  </md-sidenav-container>\r\n</td-layout-nav>"

/***/ }),

/***/ "../../../../../src/app/app.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "nav {\n  border-bottom: none;\n  margin-right: 20px; }\n  nav a {\n    min-width: 100px !important; }\n\n.navBottomBorder {\n  border-bottom: 3px solid white; }\n\nmd-sidenav {\n  width: 400px; }\n\n@media (max-width: 600px) {\n  .forDesktop {\n    display: none; }\n  .sidenavMobile {\n    width: 100%; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/app.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__sidenav_shared_sidenav_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/sidenav.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var AppComponent = (function () {
    function AppComponent(router, sidenav, auth) {
        this.router = router;
        this.sidenav = sidenav;
        this.auth = auth;
        this.mobileSidenavOpened = false;
    }
    AppComponent.prototype.openCloseSideNav = function (sidenavOpened) {
        sidenavOpened ? this.sidenav.closeSideNav() : this.sidenav.openSideNav();
    };
    AppComponent.prototype.navigateTo = function (route) {
        this.router.navigate([route]);
        this.mobileSidenavOpened = false;
    };
    AppComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-main',
            template: __webpack_require__("../../../../../src/app/app.component.html"),
            styles: [__webpack_require__("../../../../../src/app/app.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__sidenav_shared_sidenav_service__["a" /* SidenavService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__sidenav_shared_sidenav_service__["a" /* SidenavService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__core_authentication_auth_service__["a" /* AuthService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_authentication_auth_service__["a" /* AuthService */]) === "function" && _c || Object])
    ], AppComponent);
    return AppComponent;
    var _a, _b, _c;
}());

//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ "../../../../../src/app/app.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_platform_browser__ = __webpack_require__("../../../platform-browser/@angular/platform-browser.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_platform_browser_animations__ = __webpack_require__("../../../platform-browser/@angular/platform-browser/animations.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__backend_backend_module__ = __webpack_require__("../../../../../src/app/backend/backend.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__sidenav_sidenav_module__ = __webpack_require__("../../../../../src/app/sidenav/sidenav.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__book_table_book_table_module__ = __webpack_require__("../../../../../src/app/book-table/book-table.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__cockpit_area_cockpit_module__ = __webpack_require__("../../../../../src/app/cockpit-area/cockpit.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__user_area_user_area_module__ = __webpack_require__("../../../../../src/app/user-area/user-area.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__email_confirmations_email_confirmations_module__ = __webpack_require__("../../../../../src/app/email-confirmations/email-confirmations.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__header_header_module__ = __webpack_require__("../../../../../src/app/header/header.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_12__home_home_module__ = __webpack_require__("../../../../../src/app/home/home.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_13__menu_menu_module__ = __webpack_require__("../../../../../src/app/menu/menu.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_14__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_15__shared_shared_module__ = __webpack_require__("../../../../../src/app/shared/shared.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_16__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_17__app_routing_module__ = __webpack_require__("../../../../../src/app/app-routing.module.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


















var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["b" /* NgModule */])({
            declarations: [__WEBPACK_IMPORTED_MODULE_16__app_component__["a" /* AppComponent */]],
            imports: [
                __WEBPACK_IMPORTED_MODULE_3__angular_platform_browser__["a" /* BrowserModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_platform_browser_animations__["a" /* BrowserAnimationsModule */],
                __WEBPACK_IMPORTED_MODULE_11__header_header_module__["a" /* HeaderModule */],
                __WEBPACK_IMPORTED_MODULE_12__home_home_module__["a" /* HomeModule */],
                __WEBPACK_IMPORTED_MODULE_13__menu_menu_module__["a" /* MenuModule */],
                __WEBPACK_IMPORTED_MODULE_7__book_table_book_table_module__["a" /* BookTableModule */],
                __WEBPACK_IMPORTED_MODULE_6__sidenav_sidenav_module__["a" /* SidenavModule */],
                __WEBPACK_IMPORTED_MODULE_8__cockpit_area_cockpit_module__["a" /* WaiterCockpitModule */],
                __WEBPACK_IMPORTED_MODULE_9__user_area_user_area_module__["a" /* UserAreaModule */],
                __WEBPACK_IMPORTED_MODULE_14__core_core_module__["a" /* CoreModule */],
                __WEBPACK_IMPORTED_MODULE_15__shared_shared_module__["a" /* SharedModule */],
                __WEBPACK_IMPORTED_MODULE_5__backend_backend_module__["a" /* BackendModule */].forRoot({ restServiceRoot: __WEBPACK_IMPORTED_MODULE_0__config__["a" /* config */].restServiceRoot, environmentType: __WEBPACK_IMPORTED_MODULE_1__environments_environment__["a" /* environment */].backendType }),
                __WEBPACK_IMPORTED_MODULE_10__email_confirmations_email_confirmations_module__["a" /* EmailConfirmationModule */],
                __WEBPACK_IMPORTED_MODULE_17__app_routing_module__["a" /* AppRoutingModule */],
            ],
            providers: [],
            bootstrap: [__WEBPACK_IMPORTED_MODULE_16__app_component__["a" /* AppComponent */]],
        })
    ], AppModule);
    return AppModule;
}());

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ "../../../../../src/app/backend/backend.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__dishes_dishes_data_service__ = __webpack_require__("../../../../../src/app/backend/dishes/dishes-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__login_login_data_service__ = __webpack_require__("../../../../../src/app/backend/login/login-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__booking_booking_data_service__ = __webpack_require__("../../../../../src/app/backend/booking/booking-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__order_order_data_service__ = __webpack_require__("../../../../../src/app/backend/order/order-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__graphql_client__ = __webpack_require__("../../../../../src/app/backend/graphql-client.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8_apollo_angular__ = __webpack_require__("../../../../apollo-angular/build/src/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__core_httpClient_httpClient_service__ = __webpack_require__("../../../../../src/app/core/httpClient/httpClient.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return BackendConfig; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BackendModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};










// export enum BackendType {
//   IN_MEMORY,
//   REST,
//   GRAPHQL,
// }
var BackendConfig = (function () {
    function BackendConfig() {
    }
    return BackendConfig;
}());

var BackendModule = (function () {
    var BackendModule = BackendModule_1 = function BackendModule(parentModule) {
        if (parentModule) {
            throw new Error('BackendModule is already loaded. Import it in the AppModule only');
        }
    };
    BackendModule.forRoot = function (backendConfig) {
        return {
            ngModule: BackendModule_1,
            providers: [
                { provide: BackendConfig, useValue: backendConfig },
            ],
        };
    };
    BackendModule = BackendModule_1 = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_2__angular_http__["c" /* HttpModule */],
                __WEBPACK_IMPORTED_MODULE_8_apollo_angular__["a" /* ApolloModule */].forRoot(__WEBPACK_IMPORTED_MODULE_7__graphql_client__["a" /* provideClient */]),
            ],
            declarations: [],
            providers: [
                __WEBPACK_IMPORTED_MODULE_9__core_httpClient_httpClient_service__["a" /* HttpClientService */],
                __WEBPACK_IMPORTED_MODULE_3__dishes_dishes_data_service__["a" /* DishesDataService */],
                __WEBPACK_IMPORTED_MODULE_4__login_login_data_service__["a" /* LoginDataService */],
                __WEBPACK_IMPORTED_MODULE_5__booking_booking_data_service__["a" /* BookingDataService */],
                __WEBPACK_IMPORTED_MODULE_6__order_order_data_service__["a" /* OrderDataService */],
            ],
        }),
        __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["A" /* Optional */])()), __param(0, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["D" /* SkipSelf */])()),
        __metadata("design:paramtypes", [BackendModule])
    ], BackendModule);
    return BackendModule;
    var BackendModule_1;
}());

//# sourceMappingURL=backend.module.js.map

/***/ }),

/***/ "../../../../../src/app/backend/booking/booking-data-service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__app_config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_module__ = __webpack_require__("../../../../../src/app/backend/backend.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__booking_in_memory_service__ = __webpack_require__("../../../../../src/app/backend/booking/booking-in-memory.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__booking_rest_service__ = __webpack_require__("../../../../../src/app/backend/booking/booking-rest.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookingDataService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var BookingDataService = (function () {
    function BookingDataService(injector) {
        this.injector = injector;
        var backendConfig = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__backend_module__["b" /* BackendConfig */]);
        if (backendConfig.environmentType === __WEBPACK_IMPORTED_MODULE_0__app_config__["b" /* BackendType */].IN_MEMORY) {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_3__booking_in_memory_service__["a" /* BookingInMemoryService */]();
        }
        else {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_4__booking_rest_service__["a" /* BookingRestService */](this.injector);
        }
    }
    BookingDataService.prototype.bookTable = function (booking) {
        return this.usedImplementation.bookTable(booking);
    };
    BookingDataService.prototype.getReservations = function (filter) {
        return this.usedImplementation.getReservations(filter);
    };
    BookingDataService.prototype.acceptInvite = function (token) {
        return this.usedImplementation.acceptInvite(token);
    };
    BookingDataService.prototype.cancelInvite = function (token) {
        return this.usedImplementation.cancelInvite(token);
    };
    BookingDataService.prototype.cancelReserve = function (token) {
        return this.usedImplementation.cancelReserve(token);
    };
    BookingDataService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], BookingDataService);
    return BookingDataService;
    var _a;
}());

//# sourceMappingURL=booking-data-service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/booking/booking-in-memory.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__ = __webpack_require__("../../../../rxjs/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__mock_data__ = __webpack_require__("../../../../../src/app/backend/mock-data.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_moment__ = __webpack_require__("../../../../moment/moment.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_moment___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_moment__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookingInMemoryService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var BookingInMemoryService = (function () {
    function BookingInMemoryService() {
    }
    BookingInMemoryService.prototype.bookTable = function (booking) {
        var bookTable;
        bookTable = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["assign"])(bookTable, booking);
        bookTable.booking.creationDate = __WEBPACK_IMPORTED_MODULE_3_moment__().format('LLL');
        bookTable.booking.bookingToken = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["maxBy"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["a" /* bookedTables */], function (table) { return table.booking.bookingToken; }).booking.bookingToken + 1;
        bookTable.booking.tableId = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["maxBy"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["a" /* bookedTables */], function (table) { return table.booking.tableId; }).booking.tableId + 1;
        if (!bookTable.invitedGuests) {
            bookTable.invitedGuests = [];
        }
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(__WEBPACK_IMPORTED_MODULE_2__mock_data__["a" /* bookedTables */].push(bookTable));
    };
    BookingInMemoryService.prototype.getReservations = function (filters) {
        if (!filters.sort[0]) {
            filters.sort = [{ name: '', direction: '' }];
        }
        else {
            filters.sort = [{ name: filters.sort[0].name, direction: filters.sort[0].direction }];
        }
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of({
            pagination: {
                size: filters.pagination.size,
                page: filters.pagination.page,
                total: __WEBPACK_IMPORTED_MODULE_2__mock_data__["a" /* bookedTables */].length,
            },
            result: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["orderBy"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["a" /* bookedTables */], [filters.sort[0].name], [filters.sort[0].direction])
                .filter(function (booking) {
                if (filters.bookingDate) {
                    return booking.booking.bookingDate.toLowerCase().includes(filters.bookingDate.toLowerCase());
                }
                else {
                    return true;
                }
            }).filter(function (booking) {
                if (filters.email) {
                    return booking.booking.email.toLowerCase().includes(filters.email.toLowerCase());
                }
                else {
                    return true;
                }
            }).filter(function (booking) {
                if (filters.bookingToken) {
                    return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["toString"])(booking.booking.bookingToken).includes(__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["toString"])(filters.bookingToken));
                }
                else {
                    return true;
                }
            }),
        });
    };
    BookingInMemoryService.prototype.acceptInvite = function (token) {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(1);
    };
    BookingInMemoryService.prototype.cancelInvite = function (token) {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(1);
    };
    BookingInMemoryService.prototype.cancelReserve = function (token) {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(1);
    };
    BookingInMemoryService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])()
    ], BookingInMemoryService);
    return BookingInMemoryService;
}());

//# sourceMappingURL=booking-in-memory.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/booking/booking-rest.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__ = __webpack_require__("../../../../../src/app/core/httpClient/httpClient.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookingRestService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var BookingRestService = (function () {
    function BookingRestService(injector) {
        this.injector = injector;
        this.booktableRestPath = 'bookingmanagement/v1/booking';
        this.acceptReserveRestPath = 'bookingmanagement/v1/invitedguest/accept/';
        this.rejectReserveRestPath = 'bookingmanagement/v1/invitedguest/decline/';
        this.cancelReserveRestPath = 'bookingmanagement/v1/booking/cancel/';
        this.getReservationsRestPath = 'bookingmanagement/v1/booking/search';
        this.http = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__["a" /* HttpClientService */]);
    }
    BookingRestService.prototype.bookTable = function (booking) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.booktableRestPath, booking)
            .map(function (res) { return res.json(); });
    };
    BookingRestService.prototype.getReservations = function (filter) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.getReservationsRestPath, filter)
            .map(function (res) { return res.json(); });
    };
    BookingRestService.prototype.acceptInvite = function (token) {
        return this.http.get("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.acceptReserveRestPath + token)
            .map(function (res) { return res.json(); });
    };
    BookingRestService.prototype.cancelInvite = function (token) {
        return this.http.get("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.rejectReserveRestPath + token)
            .map(function (res) { return res.json(); });
    };
    BookingRestService.prototype.cancelReserve = function (token) {
        return this.http.get("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.cancelReserveRestPath + token)
            .map(function (res) { return res.json(); });
    };
    BookingRestService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], BookingRestService);
    return BookingRestService;
    var _a;
}());

//# sourceMappingURL=booking-rest.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/dishes/dishes-data-service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__app_config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_module__ = __webpack_require__("../../../../../src/app/backend/backend.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__dishes_graph_ql_service__ = __webpack_require__("../../../../../src/app/backend/dishes/dishes-graph-ql.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__dishes_in_memory_service__ = __webpack_require__("../../../../../src/app/backend/dishes/dishes-in-memory.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__dishes_rest_service__ = __webpack_require__("../../../../../src/app/backend/dishes/dishes-rest.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DishesDataService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var DishesDataService = (function () {
    function DishesDataService(injector) {
        this.injector = injector;
        var backendConfig = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__backend_module__["b" /* BackendConfig */]);
        if (backendConfig.environmentType === __WEBPACK_IMPORTED_MODULE_1__app_config__["b" /* BackendType */].IN_MEMORY) {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_4__dishes_in_memory_service__["a" /* DishesInMemoryService */]();
        }
        else if (backendConfig.environmentType === __WEBPACK_IMPORTED_MODULE_1__app_config__["b" /* BackendType */].GRAPHQL) {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_3__dishes_graph_ql_service__["a" /* DishesGraphQlService */](this.injector);
        }
        else {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_5__dishes_rest_service__["a" /* DishesRestService */](this.injector);
        }
    }
    DishesDataService.prototype.filter = function (filters) {
        return this.usedImplementation.filter(filters);
    };
    DishesDataService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], DishesDataService);
    return DishesDataService;
    var _a;
}());

//# sourceMappingURL=dishes-data-service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/dishes/dishes-graph-ql.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_apollo_angular__ = __webpack_require__("../../../../apollo-angular/build/src/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_graphql_tag__ = __webpack_require__("../../../../graphql-tag/src/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_graphql_tag___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_graphql_tag__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map__ = __webpack_require__("../../../../rxjs/add/operator/map.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_rxjs_add_operator_map__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DishesGraphQlService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




// We use the gql tag to parse our query string into a query document
var getDishesQuery = (_a = ["\n  query getDishes {\n    dishes {\n      name\n      description\n      image\n      likes\n      ingredients {\n        price\n        name\n      }\n      price\n    }\n  }\n"], _a.raw = ["\n  query getDishes {\n    dishes {\n      name\n      description\n      image\n      likes\n      ingredients {\n        price\n        name\n      }\n      price\n    }\n  }\n"], __WEBPACK_IMPORTED_MODULE_2_graphql_tag___default()(_a));
// TODO: This is a temporary type as graphQL backend was developed in parallel to angular app
// and java server implementation, model was not yet established so for now conversion
// will be implemented as a part of this service to expose consistient service API
var GqlDish = (function () {
    function GqlDish() {
    }
    return GqlDish;
}());
var DishesQueryRepsonse = (function () {
    function DishesQueryRepsonse() {
    }
    return DishesQueryRepsonse;
}());
var DishesGraphQlService = (function () {
    function DishesGraphQlService(injector) {
        this.injector = injector;
        this.apollo = injector.get(__WEBPACK_IMPORTED_MODULE_1_apollo_angular__["b" /* Apollo */]);
    }
    // added by Roberto, please, revise
    DishesGraphQlService.prototype.filter = function (filters) {
        var _this = this;
        return this.apollo.watchQuery({ query: getDishesQuery })
            .map(function (result) { return result.data.dishes; })
            .map(function (dishes) { return dishes.map(_this.convertToBackendDish); });
    };
    // TODO: see the comment above
    DishesGraphQlService.prototype.convertToBackendDish = function (dish) {
        return {
            dish: {
                id: dish.id,
                description: dish.description,
                name: dish.name,
                price: dish.price,
            },
            isfav: false,
            image: { content: dish.image },
            likes: dish.likes,
            extras: dish.ingredients.map(function (extra) { return ({ id: extra.id, name: extra.name, price: extra.price, selected: false }); }),
            categories: dish.categories,
        };
    };
    DishesGraphQlService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], DishesGraphQlService);
    return DishesGraphQlService;
    var _a;
}());

var _a;
//# sourceMappingURL=dishes-graph-ql.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/dishes/dishes-in-memory.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_Observable__ = __webpack_require__("../../../../rxjs/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__mock_data__ = __webpack_require__("../../../../../src/app/backend/mock-data.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DishesInMemoryService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var DishesInMemoryService = (function () {
    function DishesInMemoryService() {
    }
    DishesInMemoryService.prototype.filter = function (filters) {
        if (!filters.sort[0]) {
            filters.sort.push({ name: '', direction: '' });
        }
        return __WEBPACK_IMPORTED_MODULE_0_rxjs_Observable__["Observable"].of(__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["orderBy"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["d" /* dishes */], [filters.sort[0].name], [filters.sort[0].direction])
            .filter(function (plate) {
            if (filters.searchBy) {
                return plate.dish.name.toLowerCase().includes(filters.searchBy.toLowerCase());
            }
            else {
                return true;
            }
        }).filter(function (plate) {
            if (filters.maxPrice) {
                return plate.dish.price < filters.maxPrice;
            }
            else {
                return true;
            }
        }).filter(function (plate) {
            if (filters.minLikes) {
                return plate.likes > filters.minLikes;
            }
            else {
                return true;
            }
        }).filter(function (plate) {
            if (filters.categories) {
                return filters.categories.every(function (category) {
                    return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["find"])(plate.categories, category);
                });
            }
            else {
                return true;
            }
        }));
    };
    DishesInMemoryService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])()
    ], DishesInMemoryService);
    return DishesInMemoryService;
}());

//# sourceMappingURL=dishes-in-memory.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/dishes/dishes-rest.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__ = __webpack_require__("../../../../../src/app/core/httpClient/httpClient.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return DishesRestService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var DishesRestService = (function () {
    function DishesRestService(injector) {
        this.injector = injector;
        this.filtersRestPath = 'dishmanagement/v1/dish/search';
        this.http = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__["a" /* HttpClientService */]);
    }
    DishesRestService.prototype.filter = function (filters) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.filtersRestPath, filters)
            .map(function (res) { return res.json().result; });
    };
    DishesRestService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], DishesRestService);
    return DishesRestService;
    var _a;
}());

//# sourceMappingURL=dishes-rest.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/graphql-client.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_apollo_client__ = __webpack_require__("../../../../apollo-client/index.js");
/* harmony export (immutable) */ __webpack_exports__["a"] = provideClient;

// by default, this client will send queries to `/graphql` (relative to the URL of your app)
var client = new __WEBPACK_IMPORTED_MODULE_0_apollo_client__["a" /* ApolloClient */]({
    networkInterface: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0_apollo_client__["b" /* createNetworkInterface */])({
        uri: '/graphql',
        opts: {
            credentials: 'same-origin',
        },
    }),
    dataIdFromObject: function (obj) { return obj.__typename + "-" + obj.id + ","; },
});
function provideClient() {
    return client;
}
//# sourceMappingURL=graphql-client.js.map

/***/ }),

/***/ "../../../../../src/app/backend/login/login-data-service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__app_config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_module__ = __webpack_require__("../../../../../src/app/backend/backend.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__login_rest_service__ = __webpack_require__("../../../../../src/app/backend/login/login-rest.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__login_in_memory_service__ = __webpack_require__("../../../../../src/app/backend/login/login-in-memory.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginDataService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var LoginDataService = (function () {
    function LoginDataService(injector) {
        this.injector = injector;
        var backendConfig = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__backend_module__["b" /* BackendConfig */]);
        if (backendConfig.environmentType === __WEBPACK_IMPORTED_MODULE_1__app_config__["b" /* BackendType */].IN_MEMORY) {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_4__login_in_memory_service__["a" /* LoginInMemoryService */]();
        }
        else {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_3__login_rest_service__["a" /* LoginRestService */](this.injector);
        }
    }
    LoginDataService.prototype.login = function (username, password) {
        return this.usedImplementation.login(username, password);
    };
    LoginDataService.prototype.getCurrentUser = function () {
        return this.usedImplementation.getCurrentUser();
    };
    LoginDataService.prototype.register = function (email, password) {
        return this.usedImplementation.register(email, password);
    };
    LoginDataService.prototype.changePassword = function (username, oldPassword, newPassword) {
        return this.usedImplementation.changePassword(username, oldPassword, newPassword);
    };
    LoginDataService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], LoginDataService);
    return LoginDataService;
    var _a;
}());

//# sourceMappingURL=login-data-service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/login/login-in-memory.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__ = __webpack_require__("../../../../rxjs/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__mock_data__ = __webpack_require__("../../../../../src/app/backend/mock-data.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginInMemoryService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var LoginInMemoryService = (function () {
    function LoginInMemoryService() {
    }
    LoginInMemoryService.prototype.login = function (username, password) {
        var user = this.findUser(username, password);
        if (!user) {
            return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].throw({ errorCode: 2, message: 'User name or password wrong' });
        }
        __WEBPACK_IMPORTED_MODULE_2__mock_data__["b" /* currentUser */][0] = user;
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of('JWTTOKENMOCK');
    };
    LoginInMemoryService.prototype.getCurrentUser = function () {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["omit"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["b" /* currentUser */][0], 'password'));
    };
    LoginInMemoryService.prototype.register = function (email, password) {
        var existingUser = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["find"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["c" /* users */], function (user) { return user.username === email; });
        if (existingUser) {
            return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].throw({ errorCode: 1, message: 'User already exists' });
        }
        var newUser = { username: email, password: password, role: 'user' };
        __WEBPACK_IMPORTED_MODULE_2__mock_data__["c" /* users */].push(newUser);
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["omit"])(newUser, 'password'));
    };
    LoginInMemoryService.prototype.changePassword = function (username, oldPassword, newPassword) {
        var userToChange = this.findUser(username, oldPassword);
        if (!userToChange) {
            return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].throw({ errorCode: 1, message: 'Change password error. Old password do not match' });
        }
        userToChange.password = newPassword;
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of({ message: 'Password changed' });
    };
    LoginInMemoryService.prototype.findUser = function (username, password) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["find"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["c" /* users */], { username: username, password: password });
    };
    LoginInMemoryService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])()
    ], LoginInMemoryService);
    return LoginInMemoryService;
}());

//# sourceMappingURL=login-in-memory.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/login/login-rest.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__ = __webpack_require__("../../../../../src/app/core/httpClient/httpClient.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginRestService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var LoginRestService = (function () {
    function LoginRestService(injector) {
        this.injector = injector;
        this.loginRestPath = 'login';
        this.currentUserRestPath = 'security/v1/currentuser/';
        this.logoutRestPath = 'logout';
        this.registerRestPath = 'register';
        this.changePasswordRestPath = 'changepassword';
        this.http = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__["a" /* HttpClientService */]);
    }
    LoginRestService.prototype.login = function (username, password) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restPathRoot + this.loginRestPath, { username: username, password: password });
    };
    LoginRestService.prototype.getCurrentUser = function () {
        return this.http.get("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.currentUserRestPath)
            .map(function (res) { return res.json(); });
    };
    LoginRestService.prototype.register = function (email, password) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.registerRestPath, { email: email, password: password })
            .map(function (res) { return res.json(); });
    };
    LoginRestService.prototype.changePassword = function (username, oldPassword, newPassword) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.changePasswordRestPath, { username: username, oldPassword: oldPassword, newPassword: newPassword })
            .map(function (res) { return res.json(); });
    };
    LoginRestService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], LoginRestService);
    return LoginRestService;
    var _a;
}());

//# sourceMappingURL=login-rest.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/mock-data.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "f", function() { return extras; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "d", function() { return dishes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return currentUser; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "c", function() { return users; });
/* unused harmony export roles */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return bookedTables; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "e", function() { return orderList; });
var extras = [{
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
var dishes = [{
        dish: {
            id: 0,
            description: 'Lorem ipsum dolor sit amet. Proin fermentum lobortis neque. ' +
                'Pellentesque habitant morbi tristique.',
            name: 'Red Curry',
            price: 5.90,
        },
        isfav: false,
        image: { content: '../../../assets/images/basil-fried.jpg' },
        likes: 21,
        extras: [
            { id: 0, name: 'Tofu', price: 1, selected: false },
            { id: 1, name: 'Chiken', price: 1, selected: false },
            { id: 2, name: 'Pork', price: 2, selected: false }
        ],
        categories: [
            { id: '0' },
            { id: '3' },
            { id: '5' },
            { id: '6' },
            { id: '7' }
        ],
    }, {
        dish: {
            id: 1,
            description: 'Consectetur adipiscing elit. Nulla id viverra turpis, sed eleifend dui. ' +
                'Proin fermentum lobortis neque. Pellentesque habitant morbi tristique.',
            name: 'Purple Curry',
            price: 9.00,
        },
        isfav: false,
        image: { content: '../../../assets/images/garlic-paradise.jpg' },
        likes: 10,
        extras: [
            { id: 0, name: 'Tofu', price: 1, selected: false },
            { id: 1, name: 'Chiken', price: 1, selected: false },
            { id: 2, name: 'Pork', price: 2, selected: false }
        ],
        categories: [
            { id: '1' },
            { id: '6' }
        ],
    }, {
        dish: {
            id: 2,
            description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. ' +
                'Nulla id viverra turpis, sed eleifend dui. Proin fermentum lobortis neque.',
            name: 'Green Curry',
            price: 7.60,
        },
        isfav: false,
        image: { content: '../../../assets/images/green-curry.jpg' },
        likes: 61,
        extras: [
            { id: 0, name: 'Tofu', price: 1, selected: false },
            { id: 1, name: 'Chiken', price: 1, selected: false },
            { id: 2, name: 'Pork', price: 2, selected: false }
        ],
        categories: [
            { id: '2' },
            { id: '4' },
            { id: '6' }
        ],
    }, {
        dish: {
            id: 3,
            description: 'Lorem ipsum dolor. Pellentesque habitant morbi tristique.',
            name: 'Yellow Curry',
            price: 8.50,
        },
        isfav: false,
        image: { content: '../../../assets/images/dish.png' },
        likes: 48,
        extras: [
            { id: 0, name: 'Tofu', price: 1, selected: false },
            { id: 1, name: 'Chiken', price: 1, selected: false },
            { id: 2, name: 'Pork', price: 2, selected: false }
        ],
        categories: [
            { id: '1' },
            { id: '4' },
            { id: '7' }
        ],
    }];
var currentUser = [];
var users = [{
        username: 'user',
        password: 'pass',
        role: 'user',
    }, {
        username: 'waiter',
        password: 'pass',
        role: 'waiter',
    }];
var roles = [
    { name: 'user', permission: 0 },
    { name: 'waiter', permission: 1 },
];
var bookedTables = [{
        booking: {
            assistants: 3,
            bookingDate: '19/03/2017 22:00',
            name: 'Brok',
            email: 'email1@email.com',
            tableId: 0,
            creationDate: '11/03/2017 12:45',
            bookingToken: 500,
        },
        invitedGuests: [{ email: 'emailFriend1@email.com', accepted: true },
            { email: 'emailFriend2@email.com', accepted: true },
            { email: 'emailFriend3@email.com', accepted: false }],
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
        invitedGuests: [{ email: 'emailFriend1@email.com', accepted: true },
            { email: 'emailFriend2@email.com', accepted: false }],
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
        invitedGuests: [{ email: 'emailFriend1@email.com', accepted: true },
            { email: 'emailFriend2@email.com', accepted: true },
            { email: 'emailFriend3@email.com', accepted: false },
            { email: 'emailFriend4@email.com', accepted: false },
            { email: 'emailFriend5@email.com', accepted: true }],
    }];
var orderList = [{
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
                extras: [{ id: 1, name: 'Chicken', price: 2, selected: true }],
            }, {
                dish: {
                    id: 1,
                    name: 'Red Curry',
                    price: 5.90,
                },
                orderLine: {
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
                extras: [{ id: 2, name: 'Pork', price: 1, selected: true },
                    { id: 0, name: 'Tofu', price: 1, selected: true },
                    { id: 1, name: 'Chicken', price: 2, selected: true }],
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
                extras: [{ id: 2, name: 'Pork', price: 1, selected: true }],
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
                extras: [{ id: 2, name: 'Pork', price: 1, selected: true },
                    { id: 0, name: 'Tofu', price: 1, selected: true }],
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
                extras: [{ id: 1, name: 'Chicken', price: 1, selected: true }],
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
                extras: [{ id: 0, name: 'Tofu', price: 1, selected: true }],
            }],
    },
];
//# sourceMappingURL=mock-data.js.map

/***/ }),

/***/ "../../../../../src/app/backend/order/order-data-service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__app_config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_module__ = __webpack_require__("../../../../../src/app/backend/backend.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__order_in_memory_service__ = __webpack_require__("../../../../../src/app/backend/order/order-in-memory.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__order_rest_service__ = __webpack_require__("../../../../../src/app/backend/order/order-rest.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrderDataService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var OrderDataService = (function () {
    function OrderDataService(injector) {
        this.injector = injector;
        var backendConfig = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__backend_module__["b" /* BackendConfig */]);
        if (backendConfig.environmentType === __WEBPACK_IMPORTED_MODULE_1__app_config__["b" /* BackendType */].IN_MEMORY) {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_3__order_in_memory_service__["a" /* OrderInMemoryService */]();
        }
        else {
            this.usedImplementation = new __WEBPACK_IMPORTED_MODULE_4__order_rest_service__["a" /* OrderRestService */](this.injector);
        }
    }
    OrderDataService.prototype.getBookingOrders = function (filter) {
        return this.usedImplementation.getBookingOrders(filter);
    };
    OrderDataService.prototype.saveOrders = function (orders) {
        return this.usedImplementation.saveOrders(orders);
    };
    OrderDataService.prototype.cancelOrder = function (token) {
        return this.usedImplementation.cancelOrder(token);
    };
    OrderDataService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], OrderDataService);
    return OrderDataService;
    var _a;
}());

//# sourceMappingURL=order-data-service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/order/order-in-memory.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__ = __webpack_require__("../../../../rxjs/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__mock_data__ = __webpack_require__("../../../../../src/app/backend/mock-data.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_3_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrderInMemoryService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var OrderInMemoryService = (function () {
    function OrderInMemoryService() {
    }
    OrderInMemoryService.prototype.getBookingOrders = function (filters) {
        if (!filters.sort[0]) {
            filters.sort = [{ name: '', direction: '' }];
        }
        else {
            filters.sort = [{ name: filters.sort[0].name, direction: filters.sort[0].direction }];
        }
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of({
            pagination: {
                size: filters.pagination.size,
                page: filters.pagination.page,
                total: __WEBPACK_IMPORTED_MODULE_2__mock_data__["e" /* orderList */].length,
            },
            result: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["orderBy"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["e" /* orderList */], [filters.sort[0].name], [filters.sort[0].direction])
                .filter(function (order) {
                if (filters.bookingDate) {
                    return order.booking.bookingDate.toLowerCase().includes(filters.bookingDate.toLowerCase());
                }
                else {
                    return true;
                }
            }).filter(function (order) {
                if (filters.email) {
                    return order.booking.email.toLowerCase().includes(filters.email.toLowerCase());
                }
                else {
                    return true;
                }
            }).filter(function (order) {
                if (filters.bookingToken) {
                    return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["toString"])(order.booking.bookingToken).includes(__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["toString"])(filters.bookingToken));
                }
                else {
                    return true;
                }
            }),
        });
    };
    OrderInMemoryService.prototype.saveOrders = function (order) {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(__WEBPACK_IMPORTED_MODULE_2__mock_data__["e" /* orderList */].push(this.composeOrderList(order)));
    };
    OrderInMemoryService.prototype.findExtraById = function (id) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["find"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["f" /* extras */], function (extra) { return extra.id === id; });
    };
    OrderInMemoryService.prototype.findDishById = function (id) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["find"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["d" /* dishes */], function (plate) { return plate.dish.id === id; });
    };
    OrderInMemoryService.prototype.findReservationById = function (id) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["find"])(__WEBPACK_IMPORTED_MODULE_2__mock_data__["a" /* bookedTables */], function (booking) { return booking.booking.bookingToken === __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["toNumber"])(id.bookingToken); });
    };
    OrderInMemoryService.prototype.composeOrderList = function (orders) {
        var _this = this;
        var composedOrders;
        var orderLines = [];
        orders.orderLines.forEach(function (order) {
            var plate = _this.findDishById(order.orderLine.dishId);
            var _extras = [];
            order.extras.forEach(function (extraId) {
                _extras.push(_this.findExtraById(extraId.id));
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
        var bookedTable = this.findReservationById(orders.booking);
        return {
            booking: {
                bookingToken: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_3_lodash__["toNumber"])(orders.booking.bookingToken),
                name: bookedTable.booking.name,
                bookingDate: bookedTable.booking.bookingDate,
                creationDate: bookedTable.booking.creationDate,
                email: bookedTable.booking.email,
                tableId: bookedTable.booking.tableId,
            },
            orderLines: orderLines,
        };
    };
    OrderInMemoryService.prototype.cancelOrder = function (token) {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["Observable"].of(1);
    };
    OrderInMemoryService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])()
    ], OrderInMemoryService);
    return OrderInMemoryService;
}());

//# sourceMappingURL=order-in-memory.service.js.map

/***/ }),

/***/ "../../../../../src/app/backend/order/order-rest.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__ = __webpack_require__("../../../../../src/app/core/httpClient/httpClient.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrderRestService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var OrderRestService = (function () {
    function OrderRestService(injector) {
        this.injector = injector;
        this.getOrdersRestPath = 'ordermanagement/v1/order/search';
        this.filterOrdersRestPath = 'ordermanagement/v1/order/filter';
        this.cancelOrderRestPath = 'ordermanagement/v1/order/cancelorder/';
        this.saveOrdersPath = 'ordermanagement/v1/order';
        this.http = this.injector.get(__WEBPACK_IMPORTED_MODULE_2__core_httpClient_httpClient_service__["a" /* HttpClientService */]);
    }
    OrderRestService.prototype.getBookingOrders = function (filter) {
        var path;
        if (filter.email || filter.bookingToken) {
            path = this.filterOrdersRestPath;
        }
        else {
            delete filter.email;
            delete filter.bookingToken;
            path = this.getOrdersRestPath;
        }
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + path, filter)
            .map(function (res) { return res.json(); });
    };
    OrderRestService.prototype.saveOrders = function (orders) {
        return this.http.post("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.saveOrdersPath, orders)
            .map(function (res) { return res.json(); });
    };
    OrderRestService.prototype.cancelOrder = function (token) {
        return this.http.get("" + __WEBPACK_IMPORTED_MODULE_0__environments_environment__["a" /* environment */].restServiceRoot + this.cancelOrderRestPath + token)
            .map(function (res) { return res.json(); });
    };
    OrderRestService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_core__["y" /* Injector */]) === "function" && _a || Object])
    ], OrderRestService);
    return OrderRestService;
    var _a;
}());

//# sourceMappingURL=order-rest.service.js.map

/***/ }),

/***/ "../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<span md-dialog-title class=\"text-upper\">Book table</span>\r\n<md-dialog-content>\r\n  <div layout=\"column\" class=\"push-bottom-sm\" flex>\r\n    <div layout=\"row\" class=\"justify-space-between\" flex>\r\n      <span>Date</span>\r\n      <span>{{date}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n    <div layout=\"row\" class=\"justify-space-between\" flex>\r\n      <span>Name</span>\r\n      <span>{{data.name}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n    <div layout=\"row\" class=\"justify-space-between\" flex>\r\n      <span>Email</span>\r\n      <span>{{data.email}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n    <div layout=\"row\" class=\"justify-space-between\" flex>\r\n      <span>Assitants</span>\r\n      <span>{{data.assistants}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n  </div>\r\n</md-dialog-content>\r\n<md-dialog-actions>\r\n  <div class=\"align-right\" flex>\r\n    <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n    <button md-button (click)=\"sendBooking()\" color=\"accent\" class=\"text-upper\">Send</button>\r\n  </div>\r\n</md-dialog-actions>"

/***/ }),

/***/ "../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_book_table_service__ = __webpack_require__("../../../../../src/app/book-table/shared/book-table.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_moment__ = __webpack_require__("../../../../moment/moment.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_moment___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_moment__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookTableDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var BookTableDialogComponent = (function () {
    function BookTableDialogComponent(snackBar, bookingService, dialog, dialogData) {
        this.snackBar = snackBar;
        this.bookingService = bookingService;
        this.dialog = dialog;
        this.data = dialogData;
    }
    BookTableDialogComponent.prototype.ngOnInit = function () {
        this.date = __WEBPACK_IMPORTED_MODULE_4_moment__(this.data.bookingDate).format('LLL');
    };
    BookTableDialogComponent.prototype.sendBooking = function () {
        var _this = this;
        this.bookingService.postBooking(this.bookingService.composeBooking(this.data, 0)).subscribe(function () {
            _this.snackBar.openSnack('Table succesfully booked', 4000, 'green');
        }, function (error) {
            _this.snackBar.openSnack('Error booking, please try again later', 4000, 'red');
        });
        this.dialog.close(true);
    };
    BookTableDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-book-table-dialog',
            template: __webpack_require__("../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.scss")],
        }),
        __param(3, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Inject */])(__WEBPACK_IMPORTED_MODULE_1__angular_material__["c" /* MD_DIALOG_DATA */])),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__shared_book_table_service__["a" /* BookTableService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__shared_book_table_service__["a" /* BookTableService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */]) === "function" && _c || Object, Object])
    ], BookTableDialogComponent);
    return BookTableDialogComponent;
    var _a, _b, _c;
}());

//# sourceMappingURL=book-table-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/book-table/book-table.component.html":
/***/ (function(module, exports) {

module.exports = "<td-layout>\r\n  <div class=\"property-text-center pad-top-sm pad-right-sm pad-left-sm\" flex>\r\n    <h3>You can invite your friends to lunch or book a table</h3>\r\n  </div>\r\n  <md-card class=\"push-lg\">\r\n    <md-tab-group md-stretch-tabs>\r\n\r\n      <md-tab label=\"Book a table\">\r\n        <div layout-gt-xs=\"row\" flex>\r\n\r\n          <div flex-gt-xs=\"50\" class= \"menu-image forDesktop\">\r\n          </div>\r\n          \r\n          <img md-card-image src='../../assets/images/slider-1.jpg' class=\"forMobile\">\r\n\r\n          <div flex-gt-xs=\"50\">\r\n            <form class=\"pad\" #bookForm=\"ngForm\">\r\n              <div layout=\"column\" flex>\r\n                <span class=\"title-card property-text-bold\">BOOK YOUR TABLE</span>\r\n                <span class =\"subtitle-card push-bottom\">You can book a table and an order menu</span>\r\n              </div>\r\n              <div layout= \"column\" flex>\r\n\r\n                <md2-datepicker \r\n                  ngModel\r\n                  name=\"bookingDate\"\r\n                  openOnFocus=\"true\"\r\n                  type=\"datetime\"\r\n                  container=\"dialog\"\r\n                  required\r\n                  [min]=\"minDate\"\r\n                  placeholder=\"Date and time\">\r\n                </md2-datepicker>\r\n\r\n                <md-input-container color=\"accent\" flex>\r\n                  <input mdInput placeholder=\"Name\" ngModel name=\"name\" required>\r\n                  <md-error>Required. Please insert a name to reserve the table</md-error>\r\n                </md-input-container>\r\n\r\n                <md-input-container color=\"accent\" flex>\r\n                  <input mdInput placeholder=\"Email\" ngModel name=\"email\" required validateEmail>\r\n                  <md-error>Required. Insert a valid email address to send reservation data. e.g. email@email.com.</md-error>\r\n                </md-input-container>\r\n\r\n                <md-input-container class=\"push-bottom-sm\" color=\"accent\" flex>\r\n                  <input mdInput placeholder=\"Assistants\" ngModel name=\"assistants\" type=\"number\" required validateAssistants>\r\n                  <md-error>Required. You cannot reserve more assistants than the size of the biggest table (8), if you need more tables, make more bookings.</md-error>\r\n                </md-input-container>\r\n\r\n                <md-checkbox class=\"push-top\" #termsd>Accept terms</md-checkbox>\r\n\r\n              </div>\r\n              <div class=\"align-right\">\r\n                <button md-button (click)=\"showBookTableDialog(bookForm.form)\" [disabled]=\"!(bookForm.form.valid && termsd.checked)\" color=\"accent\" class=\"text-upper\">Book table</button>\r\n              </div>\r\n            </form>\r\n          </div>\r\n        </div>\r\n      </md-tab>\r\n\r\n      <md-tab label=\"Invite friends\">\r\n        <div layout-gt-xs=\"row\" flex>\r\n\r\n          <div  flex-gt-xs=\"50\" class= \"invite-image forDesktop\">\r\n          </div>\r\n\r\n          <img md-card-image src='../../assets/images/slider-2.jpg' class=\"forMobile\">\r\n          \r\n          <div  flex-gt-xs=\"50\">\r\n            <div class=\"pad\">\r\n              <form #invitationForm=\"ngForm\">\r\n                <div layout=\"column\" class=\"push-bottom\" flex>\r\n                  <span class=\"title-card property-text-bold\">ADD YOUR INFORMATION AND FRIENDS</span>\r\n                </div>\r\n\r\n                <div layout= \"column\" flex>\r\n                  <md2-datepicker \r\n                    ngModel\r\n                    name=\"bookingDate\"\r\n                    openOnFocus=\"true\"\r\n                    type=\"datetime\"\r\n                    container=\"dialog\"\r\n                    required\r\n                    [min]=\"minDate\"\r\n                    placeholder=\"Date and time\">\r\n                  </md2-datepicker>\r\n\r\n                  <md-input-container color=\"accent\" flex>\r\n                    <input mdInput placeholder=\"Name\" ngModel name=\"name\" required>\r\n                    <md-error>Required. Please insert a name to reserve the table</md-error>\r\n                  </md-input-container>\r\n\r\n                  <md-input-container color=\"accent\" flex>\r\n                    <input mdInput placeholder=\"Email\" ngModel name=\"email\" required validateEmail>\r\n                    <md-error>Required. Insert a valid email address to send reservation data. e.g. email@email.com.</md-error>\r\n                  </md-input-container>\r\n                </div>\r\n\r\n                <h4>Guests</h4>\r\n                <td-chips\r\n                  class=\"pad-bottom-sm\"\r\n                  [(ngModel)]=\"invitationModel\"\r\n                  name=\"invitedGuests\"\r\n                  (add)=\"validateEmail()\"\r\n                  placeholder=\"Enter invitation email\">\r\n                </td-chips>\r\n                <md-checkbox #termsi>Accept terms</md-checkbox>\r\n              </form>\r\n              <div class=\"align-right\">\r\n                <button md-button (click)=\"showInviteDialog(invitationForm.form)\" [disabled]=\"!(invitationForm.form.valid && invitationModel.length > 0 && termsi.checked)\" color=\"accent\" class=\"text-upper\">Invite friends</button>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </md-tab>\r\n    </md-tab-group>\r\n  </md-card>\r\n</td-layout>"

/***/ }),

/***/ "../../../../../src/app/book-table/book-table.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".background-layout {\n  height: 100%;\n  background-color: white; }\n\n.bottom-border {\n  border-bottom: 1px solid lightgray; }\n\n.subtitle-card {\n  color: lightslategrey; }\n\n.title-card {\n  font-size: 1.17em; }\n\n.menu-image {\n  background-size: cover;\n  background-repeat: no-repeat;\n  background-image: url(" + __webpack_require__("../../../../../src/assets/images/slider-1.jpg") + "); }\n\n.invite-image {\n  width: 50%;\n  background-size: cover;\n  background-repeat: no-repeat;\n  background-image: url(" + __webpack_require__("../../../../../src/assets/images/slider-2.jpg") + "); }\n\n@media (max-width: 600px) {\n  .forDesktop {\n    display: none; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/book-table/book-table.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__book_table_dialog_book_table_dialog_component__ = __webpack_require__("../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__invitation_dialog_invitation_dialog_component__ = __webpack_require__("../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__shared_directives_email_validator_directive__ = __webpack_require__("../../../../../src/app/shared/directives/email-validator.directive.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_7_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookTableComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var BookTableComponent = (function () {
    function BookTableComponent(window, snackBarservice, dialog) {
        this.window = window;
        this.snackBarservice = snackBarservice;
        this.dialog = dialog;
        this.invitationModel = [];
        this.minDate = new Date();
    }
    BookTableComponent.prototype.showBookTableDialog = function (form) {
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_2__book_table_dialog_book_table_dialog_component__["a" /* BookTableDialogComponent */], {
            width: this.window.responsiveWidth(),
            data: form.value,
        });
        dialogRef.afterClosed().subscribe(function (res) {
            if (res) {
                form.reset();
            }
        });
    };
    BookTableComponent.prototype.showInviteDialog = function (form) {
        var _this = this;
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_3__invitation_dialog_invitation_dialog_component__["a" /* InvitationDialogComponent */], {
            width: this.window.responsiveWidth(),
            data: form.value,
        });
        dialogRef.afterClosed().subscribe(function (res) {
            if (res) {
                form.reset();
                _this.invitationModel = [];
            }
        });
    };
    BookTableComponent.prototype.validateEmail = function () {
        if (!__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_6__shared_directives_email_validator_directive__["a" /* emailValidator */])(__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_7_lodash__["last"])(this.invitationModel))) {
            this.invitationModel.pop();
            this.snackBarservice.openSnack('Email format not valid', 1000, 'red');
        }
    };
    BookTableComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-book-table',
            template: __webpack_require__("../../../../../src/app/book-table/book-table.component.html"),
            styles: [__webpack_require__("../../../../../src/app/book-table/book-table.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_4__core_windowService_windowService_service__["a" /* WindowService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_windowService_windowService_service__["a" /* WindowService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_5__core_snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__core_snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["a" /* MdDialog */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["a" /* MdDialog */]) === "function" && _c || Object])
    ], BookTableComponent);
    return BookTableComponent;
    var _a, _b, _c;
}());

//# sourceMappingURL=book-table.component.js.map

/***/ }),

/***/ "../../../../../src/app/book-table/book-table.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__book_table_component__ = __webpack_require__("../../../../../src/app/book-table/book-table.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_shared_module__ = __webpack_require__("../../../../../src/app/shared/shared.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_md2__ = __webpack_require__("../../../../md2/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__shared_book_table_service__ = __webpack_require__("../../../../../src/app/book-table/shared/book-table.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__core_windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__book_table_dialog_book_table_dialog_component__ = __webpack_require__("../../../../../src/app/book-table/book-table-dialog/book-table-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__invitation_dialog_invitation_dialog_component__ = __webpack_require__("../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookTableModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











var BookTableModule = (function () {
    function BookTableModule() {
    }
    BookTableModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_2__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_5_md2__["a" /* Md2Module */],
                __WEBPACK_IMPORTED_MODULE_3__shared_shared_module__["a" /* SharedModule */],
                __WEBPACK_IMPORTED_MODULE_4__core_core_module__["a" /* CoreModule */],
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_6__shared_book_table_service__["a" /* BookTableService */],
                __WEBPACK_IMPORTED_MODULE_7__core_windowService_windowService_service__["a" /* WindowService */],
                __WEBPACK_IMPORTED_MODULE_8__core_snackService_snackService_service__["a" /* SnackBarService */],
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_10__invitation_dialog_invitation_dialog_component__["a" /* InvitationDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_9__book_table_dialog_book_table_dialog_component__["a" /* BookTableDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_0__book_table_component__["a" /* BookTableComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_0__book_table_component__["a" /* BookTableComponent */],
            ],
            entryComponents: [
                __WEBPACK_IMPORTED_MODULE_10__invitation_dialog_invitation_dialog_component__["a" /* InvitationDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_9__book_table_dialog_book_table_dialog_component__["a" /* BookTableDialogComponent */],
            ],
        })
    ], BookTableModule);
    return BookTableModule;
}());

//# sourceMappingURL=book-table.module.js.map

/***/ }),

/***/ "../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<span md-dialog-title class=\"text-upper\">Book table</span>\r\n<md-dialog-content>\r\n  <div layout=\"column\" class=\"push-bottom-sm\" flex>\r\n    <div layout=\"row\" class=\"justify-space-between\">\r\n      <span>Date</span>\r\n      <span>{{date}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n    <div layout=\"row\" class=\"justify-space-between\">\r\n      <span>Name</span>\r\n      <span>{{data.name}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n    <div layout=\"row\" class=\"justify-space-between\">\r\n      <span>Email</span>\r\n      <span>{{data.email}}</span>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n    <div layout=\"row\" class=\"justify-space-between\">\r\n      <span>Friends</span>\r\n      <div layout=\"column\" style=\"text-align:right; width:50%\">\r\n        <span *ngFor=\"let inv of data.invitedGuests\">{{inv}}</span>\r\n      </div>\r\n    </div>\r\n    <span class=\"push-bottom-sm push-top-sm\"><md-divider></md-divider></span>\r\n  </div>\r\n</md-dialog-content>\r\n<md-dialog-actions>\r\n  <div class=\"align-right\" flex>\r\n    <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n    <button md-button (click)=\"sendInvitation()\" color=\"accent\" class=\"text-upper\">Send</button>\r\n  </div>\r\n</md-dialog-actions>"

/***/ }),

/***/ "../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_book_table_service__ = __webpack_require__("../../../../../src/app/book-table/shared/book-table.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_moment__ = __webpack_require__("../../../../moment/moment.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_moment___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_moment__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return InvitationDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var InvitationDialogComponent = (function () {
    function InvitationDialogComponent(snackBar, invitationService, dialog, dialogData) {
        this.snackBar = snackBar;
        this.invitationService = invitationService;
        this.dialog = dialog;
        this.data = dialogData;
    }
    InvitationDialogComponent.prototype.ngOnInit = function () {
        this.date = __WEBPACK_IMPORTED_MODULE_4_moment__(this.data.bookingDate).format('LLL');
    };
    InvitationDialogComponent.prototype.sendInvitation = function () {
        var _this = this;
        this.invitationService.postBooking(this.invitationService.composeBooking(this.data, 1)).subscribe(function () {
            _this.snackBar.openSnack('Table succesfully booked', 4000, 'green');
        }, function (error) {
            _this.snackBar.openSnack('Error booking, please try again later', 4000, 'red');
        });
        this.dialog.close(true);
    };
    InvitationDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-invitation-dialog',
            template: __webpack_require__("../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/book-table/invitation-dialog/invitation-dialog.component.scss")],
        }),
        __param(3, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Inject */])(__WEBPACK_IMPORTED_MODULE_1__angular_material__["c" /* MD_DIALOG_DATA */])),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__shared_book_table_service__["a" /* BookTableService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__shared_book_table_service__["a" /* BookTableService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */]) === "function" && _c || Object, Object])
    ], InvitationDialogComponent);
    return InvitationDialogComponent;
    var _a, _b, _c;
}());

//# sourceMappingURL=invitation-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/book-table/shared/book-table.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__backend_booking_booking_data_service__ = __webpack_require__("../../../../../src/app/backend/booking/booking-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return BookTableService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var BookTableService = (function () {
    function BookTableService(bookingDataService) {
        this.bookingDataService = bookingDataService;
    }
    BookTableService.prototype.postBooking = function (bookInfo) {
        return this.bookingDataService.bookTable(bookInfo);
    };
    BookTableService.prototype.composeBooking = function (invitationData, type) {
        var composedBooking = {
            booking: {
                bookingDate: invitationData.bookingDate,
                name: invitationData.name,
                email: invitationData.email,
                bookingType: type,
            },
        };
        if (type) {
            composedBooking.invitedGuests = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["map"])(invitationData.invitedGuests, function (guest) { return { email: guest }; });
        }
        else {
            composedBooking.booking.assistants = invitationData.assistants;
        }
        return composedBooking;
    };
    BookTableService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__backend_booking_booking_data_service__["a" /* BookingDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__backend_booking_booking_data_service__["a" /* BookingDataService */]) === "function" && _a || Object])
    ], BookTableService);
    return BookTableService;
    var _a;
}());

//# sourceMappingURL=book-table.service.js.map

/***/ }),

/***/ "../../../../../src/app/cockpit-area/cockpit.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_waiter_cockpit_service__ = __webpack_require__("../../../../../src/app/cockpit-area/shared/waiter-cockpit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__reservation_cockpit_reservation_cockpit_component__ = __webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__order_cockpit_order_cockpit_component__ = __webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__order_cockpit_order_dialog_order_dialog_component__ = __webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__reservation_cockpit_reservation_dialog_reservation_dialog_component__ = __webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return WaiterCockpitModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var WaiterCockpitModule = (function () {
    function WaiterCockpitModule() {
    }
    WaiterCockpitModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_2__core_core_module__["a" /* CoreModule */],
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_3__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */],
                __WEBPACK_IMPORTED_MODULE_4__core_windowService_windowService_service__["a" /* WindowService */],
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_5__reservation_cockpit_reservation_cockpit_component__["a" /* ReservationCockpitComponent */],
                __WEBPACK_IMPORTED_MODULE_6__order_cockpit_order_cockpit_component__["a" /* OrderCockpitComponent */],
                __WEBPACK_IMPORTED_MODULE_8__reservation_cockpit_reservation_dialog_reservation_dialog_component__["a" /* ReservationDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_7__order_cockpit_order_dialog_order_dialog_component__["a" /* OrderDialogComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_5__reservation_cockpit_reservation_cockpit_component__["a" /* ReservationCockpitComponent */],
                __WEBPACK_IMPORTED_MODULE_6__order_cockpit_order_cockpit_component__["a" /* OrderCockpitComponent */],
            ],
            entryComponents: [
                __WEBPACK_IMPORTED_MODULE_8__reservation_cockpit_reservation_dialog_reservation_dialog_component__["a" /* ReservationDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_7__order_cockpit_order_dialog_order_dialog_component__["a" /* OrderDialogComponent */],
            ],
        })
    ], WaiterCockpitModule);
    return WaiterCockpitModule;
}());

//# sourceMappingURL=cockpit.module.js.map

/***/ }),

/***/ "../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.html":
/***/ (function(module, exports) {

module.exports = "<td-layout>\r\n  <form (ngSubmit)=\"applyFilters()\" #filterForm=\"ngForm\">\r\n    <div style= \"background-color: white\">\r\n      <td-expansion-panel label=\"FILTER\">\r\n        <div layout-gt-xs=\"row\" class=\"pad-left-md pad-right-md\" style=\"align-items:center; border-bottom: 1px solid lightgrey\" flex>\r\n          <div layout=\"row\" class=\"justify-space-around\" style=\"align-items:center\" flex>\r\n            <md-input-container color=\"accent\" class=\"searchBy\">\r\n              <input mdInput placeholder=\"Email\" [(ngModel)]=\"filters.email\" name=\"email\">\r\n            </md-input-container>\r\n            <md-input-container color=\"accent\" class=\"searchBy\">\r\n              <input mdInput placeholder=\"Reference number\" [(ngModel)]=\"filters.bookingToken\" name=\"bookingToken\">\r\n            </md-input-container>\r\n          </div>\r\n        </div>\r\n        <div class=\"align-right\">\r\n          <button md-button type=\"button\" (click)=\"clearFilters(filterForm)\" class=\"text-upper property-text-bold\">Clear filters</button>\r\n          <button md-button type=\"submit\" color=\"accent\" class=\"text-upper property-text-bold\">Apply filters</button>\r\n        </div>\r\n      </td-expansion-panel>\r\n    </div>\r\n  </form>\r\n  <md-progress-bar\r\n    *ngIf=\"!orders\"\r\n    color=\"accent\"\r\n    mode=\"indeterminate\">\r\n  </md-progress-bar>\r\n  <md-card>\r\n    <md-card-title>ORDERS</md-card-title>\r\n    <md-divider></md-divider>\r\n    <td-data-table\r\n      #dataTable\r\n      [data]=\"orders\"\r\n      [columns]=\"columns\"\r\n      [sortable]=\"true\"\r\n      clickable=\"true\"\r\n      (rowClick)=\"selected($event)\"\r\n      (sortChange)=\"sort($event)\">\r\n    </td-data-table>\r\n    <div class=\"md-padding\" *ngIf=\"!dataTable.hasData\" layout=\"row\" layout-align=\"center center\">\r\n      <h3>No results to display.</h3>\r\n    </div>\r\n    <td-paging-bar #pagingBar [pageSizes]=\"pageSizes\" [total]=\"totalOrders\" (change)=\"page($event)\">\r\n      <span td-paging-bar-label hide-xs>Rows per page:</span>\r\n      {{pagingBar.range}} <span hide-xs>of {{pagingBar.total}}</span>\r\n    </td-paging-bar>\r\n  </md-card>\r\n</td-layout>\r\n"

/***/ }),

/***/ "../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "@media (max-width: 600px) {\n  .forDesktop {\n    display: none; }\n  .searchBy {\n    width: 100%; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; }\n  .sortingBy {\n    width: 30%; }\n  .searchBy {\n    width: 30%; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__ = __webpack_require__("../../../../../src/app/cockpit-area/shared/waiter-cockpit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__order_dialog_order_dialog_component__ = __webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_lodash__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrderCockpitComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var OrderCockpitComponent = (function () {
    function OrderCockpitComponent(dialog, waiterCockpitService) {
        this.dialog = dialog;
        this.waiterCockpitService = waiterCockpitService;
        this.pagination = {
            size: 8,
            page: 1,
            total: 1,
        };
        this.sorting = [];
        this.columns = [
            { name: 'booking.bookingDate', label: 'Reservation date' },
            { name: 'booking.email', label: 'Email' },
            { name: 'booking.bookingToken', label: 'Reference number' },
        ];
        this.pageSizes = __WEBPACK_IMPORTED_MODULE_5__config__["a" /* config */].pageSizes;
        this.filters = {
            bookingDate: undefined,
            email: undefined,
            bookingToken: undefined,
        };
    }
    OrderCockpitComponent.prototype.ngOnInit = function () {
        this.applyFilters();
    };
    OrderCockpitComponent.prototype.applyFilters = function () {
        var _this = this;
        this.waiterCockpitService.getOrders(this.pagination, this.sorting, this.filters)
            .subscribe(function (data) {
            _this.orders = data.result;
            _this.totalOrders = data.pagination.total;
        });
    };
    OrderCockpitComponent.prototype.clearFilters = function (filters) {
        filters.reset();
        this.applyFilters();
    };
    OrderCockpitComponent.prototype.page = function (pagingEvent) {
        this.pagination = {
            size: pagingEvent.pageSize,
            page: pagingEvent.page,
            total: 1,
        };
        this.applyFilters();
    };
    OrderCockpitComponent.prototype.sort = function (sortEvent) {
        this.sorting = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["reject"])(this.sorting, { 'name': sortEvent.name.split('.').pop() });
        this.sorting.push({ 'name': sortEvent.name.split('.').pop(), 'direction': '' + sortEvent.order });
        this.applyFilters();
    };
    OrderCockpitComponent.prototype.selected = function (selection) {
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_3__order_dialog_order_dialog_component__["a" /* OrderDialogComponent */], {
            width: '80%',
            data: selection,
        });
    };
    OrderCockpitComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'cockpit-order-cockpit',
            template: __webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.html"),
            styles: [__webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-cockpit.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["a" /* MdDialog */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["a" /* MdDialog */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */]) === "function" && _b || Object])
    ], OrderCockpitComponent);
    return OrderCockpitComponent;
    var _a, _b;
}());

//# sourceMappingURL=order-cockpit.component.js.map

/***/ }),

/***/ "../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<h3 md-dialog-title>ORDER DETAILS - REFERENCE NUMBER {{data.bookingId}} </h3>\r\n<md-divider></md-divider>\r\n<td-data-table\r\n  #dataTable\r\n  [data]=\"datat\"\r\n  [columns]=\"columnst\">\r\n</td-data-table>\r\n<td-data-table\r\n  #dataOrders\r\n  [data]=\"filteredData\"\r\n  [columns]=\"columnso\">\r\n</td-data-table>\r\n<div layout=\"row\" class=\"justify-space-between pad-right pad-left\">\r\n  <h4>TOTAL</h4>\r\n  <h4>{{totalPrice | number : '2.2-2'}}</h4>\r\n</div>\r\n<md-divider></md-divider>\r\n<td-paging-bar #pagingBar [firstLast]=\"false\" [pageSizes]=\"pageSizes\" [total]=\"datao.length\" (change)=\"page($event)\">\r\n  <span td-paging-bar-label hide-xs>Rows per page:</span>\r\n  {{pagingBar.range}} <span hide-xs>of {{pagingBar.total}}</span>\r\n</td-paging-bar>\r\n<div class=\"align-right\" flex>\r\n  <button md-button md-dialog-close color=\"accent\" class=\"text-upper\">Close</button>\r\n</div>"

/***/ }),

/***/ "../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__covalent_core__ = __webpack_require__("../../../../@covalent/core/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__ = __webpack_require__("../../../../../src/app/cockpit-area/shared/waiter-cockpit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return OrderDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var OrderDialogComponent = (function () {
    function OrderDialogComponent(_dataTableService, waiterCockpitService, dialogData) {
        this._dataTableService = _dataTableService;
        this.waiterCockpitService = waiterCockpitService;
        this.fromRow = 1;
        this.currentPage = 1;
        this.pageSize = 5;
        this.datat = [];
        this.columnst = [
            { name: 'bookingDate', label: 'Reservation date' },
            { name: 'creationDate', label: 'Creation date' },
            { name: 'name', label: 'Owner' },
            { name: 'email', label: 'Email' },
            { name: 'tableId', label: 'Table' },
        ];
        this.datao = [];
        this.columnso = [
            { name: 'dish.name', label: 'Dish' },
            { name: 'orderLine.comment', label: 'Comments' },
            { name: 'extras', label: 'Extra' },
            { name: 'orderLine.amount', label: 'Quantity' },
            { name: 'dish.price', label: 'Price', numeric: true, format: function (v) { return v.toFixed(2); } },
        ];
        this.pageSizes = __WEBPACK_IMPORTED_MODULE_4__config__["a" /* config */].pageSizesDialog;
        this.filteredData = this.datao;
        this.data = dialogData.row;
    }
    OrderDialogComponent.prototype.ngOnInit = function () {
        this.totalPrice = this.waiterCockpitService.getTotalPrice(this.data.orderLines);
        this.datao = this.waiterCockpitService.orderComposer(this.data.orderLines);
        this.datat.push(this.data.booking);
        this.filter();
    };
    OrderDialogComponent.prototype.page = function (pagingEvent) {
        this.fromRow = pagingEvent.fromRow;
        this.currentPage = pagingEvent.page;
        this.pageSize = pagingEvent.pageSize;
        this.filter();
    };
    OrderDialogComponent.prototype.filter = function () {
        var newData = this.datao;
        newData = this._dataTableService.pageData(newData, this.fromRow, this.currentPage * this.pageSize);
        this.filteredData = newData;
    };
    OrderDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'cockpit-order-dialog',
            template: __webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/cockpit-area/order-cockpit/order-dialog/order-dialog.component.scss")],
        }),
        __param(2, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Inject */])(__WEBPACK_IMPORTED_MODULE_3__angular_material__["c" /* MD_DIALOG_DATA */])),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__covalent_core__["a" /* TdDataTableService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__covalent_core__["a" /* TdDataTableService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */]) === "function" && _b || Object, Object])
    ], OrderDialogComponent);
    return OrderDialogComponent;
    var _a, _b;
}());

//# sourceMappingURL=order-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.html":
/***/ (function(module, exports) {

module.exports = "<td-layout>\r\n  <form (ngSubmit)=\"filter()\" #filterForm=\"ngForm\">\r\n    <div style= \"background-color: white\">\r\n      <td-expansion-panel label=\"FILTER\">\r\n        <div layout-gt-xs=\"row\" class=\"pad-left-md pad-right-md\" style=\"align-items:center; border-bottom: 1px solid lightgrey\" flex>\r\n          <div layout=\"row\" class=\"justify-space-around\" style=\"align-items:center\" flex>\r\n            <md-input-container color=\"accent\" class=\"searchBy\">\r\n              <input mdInput placeholder=\"Email\" [(ngModel)]=\"filters.email\" name=\"email\">\r\n            </md-input-container>\r\n            <md-input-container color=\"accent\" class=\"searchBy\">\r\n              <input mdInput placeholder=\"Reference number\" [(ngModel)]=\"filters.bookingToken\" name=\"bookingToken\">\r\n            </md-input-container>\r\n          </div>\r\n        </div>\r\n        <div class=\"align-right\">\r\n          <button md-button type=\"button\" (click)=\"clearFilters(filterForm)\" class=\"text-upper property-text-bold\">Clear filters</button>\r\n          <button md-button type=\"submit\" color=\"accent\" class=\"text-upper property-text-bold\">Apply filters</button>\r\n        </div>\r\n      </td-expansion-panel>\r\n    </div>\r\n  </form>\r\n  <md-progress-bar\r\n    *ngIf=\"!reservations\"\r\n    color=\"accent\"\r\n    mode=\"indeterminate\">\r\n  </md-progress-bar>\r\n  <md-card>\r\n    <md-card-title>RESERVATIONS</md-card-title>\r\n    <md-divider></md-divider>\r\n    <td-data-table\r\n      #dataTable\r\n      [data]=\"reservations\"\r\n      [columns]=\"columns\"\r\n      [sortable]=\"true\"\r\n      clickable=\"true\"\r\n      (rowClick)=\"selected($event)\"\r\n      (sortChange)=\"sort($event)\">\r\n    </td-data-table>\r\n    <div class=\"md-padding\" *ngIf=\"!dataTable.hasData\" layout=\"row\" layout-align=\"center center\">\r\n      <h3>No results to display.</h3>\r\n    </div>\r\n    <td-paging-bar #pagingBar [pageSizes]=\"pageSizes\" [total]=\"totalReservations\" (change)=\"page($event)\">\r\n      <span td-paging-bar-label hide-xs>Rows per page:</span>\r\n      {{pagingBar.range}} <span hide-xs>of {{pagingBar.total}}</span>\r\n    </td-paging-bar>\r\n  </md-card>\r\n</td-layout>"

/***/ }),

/***/ "../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "@media (max-width: 600px) {\n  .forDesktop {\n    display: none; }\n  .searchBy {\n    width: 100%; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; }\n  .sortingBy {\n    width: 30%; }\n  .searchBy {\n    width: 30%; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__shared_waiter_cockpit_service__ = __webpack_require__("../../../../../src/app/cockpit-area/shared/waiter-cockpit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__reservation_dialog_reservation_dialog_component__ = __webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_lodash__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ReservationCockpitComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var ReservationCockpitComponent = (function () {
    function ReservationCockpitComponent(waiterCockpitService, dialog) {
        this.waiterCockpitService = waiterCockpitService;
        this.dialog = dialog;
        this.pagination = {
            size: 8,
            page: 1,
            total: 1,
        };
        this.sorting = [];
        this.columns = [
            { name: 'booking.bookingDate', label: 'Reservation date' },
            { name: 'booking.email', label: 'Email' },
            { name: 'booking.bookingToken', label: 'Reference number' },
        ];
        this.pageSizes = __WEBPACK_IMPORTED_MODULE_5__config__["a" /* config */].pageSizes;
        this.filters = {
            bookingDate: undefined,
            email: undefined,
            bookingToken: undefined,
        };
    }
    ReservationCockpitComponent.prototype.ngOnInit = function () {
        this.applyFilters();
    };
    ReservationCockpitComponent.prototype.filter = function () {
        this.pagination.page = 1;
        this.applyFilters();
    };
    ReservationCockpitComponent.prototype.applyFilters = function () {
        var _this = this;
        this.waiterCockpitService.getReservations(this.pagination, this.sorting, this.filters)
            .subscribe(function (data) {
            _this.reservations = data.result;
            _this.totalReservations = data.pagination.total;
        });
    };
    ReservationCockpitComponent.prototype.clearFilters = function (filters) {
        filters.reset();
        this.applyFilters();
    };
    ReservationCockpitComponent.prototype.page = function (pagingEvent) {
        this.pagination = {
            size: pagingEvent.pageSize,
            page: pagingEvent.page,
            total: 1,
        };
        this.applyFilters();
    };
    ReservationCockpitComponent.prototype.sort = function (sortEvent) {
        this.sorting = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["reject"])(this.sorting, { 'name': sortEvent.name.split('.').pop() });
        this.sorting.push({ 'name': sortEvent.name.split('.').pop(), 'direction': '' + sortEvent.order });
        this.applyFilters();
    };
    ReservationCockpitComponent.prototype.selected = function (selection) {
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_3__reservation_dialog_reservation_dialog_component__["a" /* ReservationDialogComponent */], {
            width: '80%',
            data: selection,
        });
    };
    ReservationCockpitComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_11" /* Component */])({
            selector: 'cockpit-reservation-cockpit',
            template: __webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.html"),
            styles: [__webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-cockpit.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__angular_material__["a" /* MdDialog */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_material__["a" /* MdDialog */]) === "function" && _b || Object])
    ], ReservationCockpitComponent);
    return ReservationCockpitComponent;
    var _a, _b;
}());

//# sourceMappingURL=reservation-cockpit.component.js.map

/***/ }),

/***/ "../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<h3 md-dialog-title>RESERVATION DETAILS - REFERENCE NUMBER {{data.bookingId}} </h3>\r\n<md-divider></md-divider>\r\n<td-data-table\r\n  #dataTable\r\n  [data]=\"datat\"\r\n  [columns]=\"columnst\">\r\n</td-data-table>\r\n<td-data-table\r\n  *ngIf=\"filteredData.length > 0\"\r\n  #dataOrders\r\n  [data]=\"filteredData\"\r\n  [columns]=\"columnso\">\r\n  <ng-template tdDataTableTemplate=\"accepted\" let-value=\"value\" let-row=\"row\" let-column=\"column\">\r\n    <md-icon color=\"accent\" *ngIf=\"value\">check</md-icon>\r\n    <md-icon color=\"warn\" *ngIf=\"!value\">clear</md-icon>\r\n  </ng-template>\r\n</td-data-table>\r\n<td-paging-bar *ngIf=\"filteredData.length > 0\" #pagingBar [firstLast]=\"false\" [pageSizes]=\"pageSizes\" [total]=\"datao.length\" (change)=\"page($event)\">\r\n  <span td-paging-bar-label hide-xs>Rows per page:</span>\r\n  {{pagingBar.range}} <span hide-xs>of {{pagingBar.total}}</span>\r\n</td-paging-bar>\r\n<div class=\"align-right\" flex>\r\n  <button md-button md-dialog-close color=\"accent\" class=\"text-upper\">Close</button>\r\n</div>"

/***/ }),

/***/ "../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__covalent_core__ = __webpack_require__("../../../../@covalent/core/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__ = __webpack_require__("../../../../../src/app/cockpit-area/shared/waiter-cockpit.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ReservationDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var ReservationDialogComponent = (function () {
    function ReservationDialogComponent(_dataTableService, waiterCockpitService, dialogData) {
        this._dataTableService = _dataTableService;
        this.waiterCockpitService = waiterCockpitService;
        this.datao = [];
        this.columnso = [
            { name: 'email', label: 'Guest email' },
            { name: 'accepted', label: 'Acceptances and declines' },
        ];
        this.pageSizes = __WEBPACK_IMPORTED_MODULE_4__config__["a" /* config */].pageSizesDialog;
        this.fromRow = 1;
        this.currentPage = 1;
        this.pageSize = 5;
        this.datat = [];
        this.columnst = [
            { name: 'booking.bookingDate', label: 'Reservation date' },
            { name: 'booking.creationDate', label: 'Creation date' },
            { name: 'booking.name', label: 'Owner' },
            { name: 'booking.email', label: 'Email' },
            { name: 'booking.tableId', label: 'Table' },
        ];
        this.filteredData = this.datao;
        this.data = dialogData.row;
    }
    ReservationDialogComponent.prototype.ngOnInit = function () {
        this.datat.push(this.data);
        this.datao = this.data.invitedGuests;
        if (this.data.booking.assistants) {
            this.columnst.push({ name: 'booking.assistants', label: 'Assistants' });
        }
        this.filter();
    };
    ReservationDialogComponent.prototype.page = function (pagingEvent) {
        this.fromRow = pagingEvent.fromRow;
        this.currentPage = pagingEvent.page;
        this.pageSize = pagingEvent.pageSize;
        this.filter();
    };
    ReservationDialogComponent.prototype.filter = function () {
        var newData = this.datao;
        newData = this._dataTableService.pageData(newData, this.fromRow, this.currentPage * this.pageSize);
        this.filteredData = newData;
    };
    ReservationDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'cockpit-reservation-dialog',
            template: __webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/cockpit-area/reservation-cockpit/reservation-dialog/reservation-dialog.component.scss")],
        }),
        __param(2, __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Inject */])(__WEBPACK_IMPORTED_MODULE_3__angular_material__["c" /* MD_DIALOG_DATA */])),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__covalent_core__["a" /* TdDataTableService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__covalent_core__["a" /* TdDataTableService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__shared_waiter_cockpit_service__["a" /* WaiterCockpitService */]) === "function" && _b || Object, Object])
    ], ReservationDialogComponent);
    return ReservationDialogComponent;
    var _a, _b;
}());

//# sourceMappingURL=reservation-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/cockpit-area/shared/waiter-cockpit.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__sidenav_shared_price_calculator_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/price-calculator.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_order_order_data_service__ = __webpack_require__("../../../../../src/app/backend/order/order-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__backend_booking_booking_data_service__ = __webpack_require__("../../../../../src/app/backend/booking/booking-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return WaiterCockpitService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var WaiterCockpitService = (function () {
    function WaiterCockpitService(orderDataService, bookingDataService, priceCalculator) {
        this.orderDataService = orderDataService;
        this.bookingDataService = bookingDataService;
        this.priceCalculator = priceCalculator;
    }
    WaiterCockpitService.prototype.getOrders = function (pagination, sorting, filters) {
        filters.pagination = pagination;
        filters.sort = sorting;
        return this.orderDataService.getBookingOrders(filters);
    };
    WaiterCockpitService.prototype.getReservations = function (pagination, sorting, filters) {
        filters.pagination = pagination;
        filters.sort = sorting;
        return this.bookingDataService.getReservations(filters);
    };
    WaiterCockpitService.prototype.orderComposer = function (orderList) {
        var _this = this;
        var orders = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["cloneDeep"])(orderList);
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["map"])(orders, function (o) {
            o.dish.price = _this.priceCalculator.getPrice(o);
            o.extras = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4_lodash__["map"])(o.extras, 'name').join(', ');
        });
        return orders;
    };
    WaiterCockpitService.prototype.getTotalPrice = function (orderLines) {
        return this.priceCalculator.getTotalPrice(orderLines);
    };
    WaiterCockpitService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__backend_order_order_data_service__["a" /* OrderDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__backend_order_order_data_service__["a" /* OrderDataService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_3__backend_booking_booking_data_service__["a" /* BookingDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__backend_booking_booking_data_service__["a" /* BookingDataService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__sidenav_shared_price_calculator_service__["a" /* PriceCalculatorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__sidenav_shared_price_calculator_service__["a" /* PriceCalculatorService */]) === "function" && _c || Object])
    ], WaiterCockpitService);
    return WaiterCockpitService;
    var _a, _b, _c;
}());

//# sourceMappingURL=waiter-cockpit.service.js.map

/***/ }),

/***/ "../../../../../src/app/config.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return config; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return BackendType; });
var config = {
    pageSizes: [8, 16, 24],
    pageSizesDialog: [4, 8, 12],
    roles: [
        { name: 'CUSTOMER', permission: 0 },
        { name: 'WAITER', permission: 1 },
    ],
};
var BackendType;
(function (BackendType) {
    BackendType[BackendType["IN_MEMORY"] = 0] = "IN_MEMORY";
    BackendType[BackendType["REST"] = 1] = "REST";
    BackendType[BackendType["GRAPHQL"] = 2] = "GRAPHQL";
})(BackendType || (BackendType = {}));
//# sourceMappingURL=config.js.map

/***/ }),

/***/ "../../../../../src/app/core/authentication/auth-guard.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthGuardService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var AuthGuardService = (function () {
    function AuthGuardService(snackBar, authService, router) {
        this.snackBar = snackBar;
        this.authService = authService;
        this.router = router;
    }
    AuthGuardService.prototype.canActivate = function (route, state) {
        if (this.authService.isLogged() && this.authService.isPermited('WAITER')) {
            return true;
        }
        if (!this.authService.isLogged()) {
            this.snackBar.openSnack('Access denied, please log in first', 4000, 'red');
        }
        if (this.router.url === '/') {
            this.router.navigate(['/restaurant']);
        }
        return false;
    };
    AuthGuardService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__auth_service__["a" /* AuthService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__auth_service__["a" /* AuthService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_router__["b" /* Router */]) === "function" && _c || Object])
    ], AuthGuardService);
    return AuthGuardService;
    var _a, _b, _c;
}());

//# sourceMappingURL=auth-guard.service.js.map

/***/ }),

/***/ "../../../../../src/app/core/authentication/auth.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_lodash__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AuthService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var AuthService = (function () {
    function AuthService() {
        this.logged = false;
        this.user = '';
        this.currentRole = 'CUSTOMER';
    }
    AuthService.prototype.isLogged = function () {
        return this.logged;
    };
    AuthService.prototype.setLogged = function (login) {
        this.logged = login;
    };
    AuthService.prototype.getUser = function () {
        return this.user;
    };
    AuthService.prototype.setUser = function (username) {
        this.user = username;
    };
    AuthService.prototype.getToken = function () {
        return this.token;
    };
    AuthService.prototype.setToken = function (token) {
        this.token = token;
    };
    AuthService.prototype.setRole = function (role) {
        this.currentRole = role;
    };
    AuthService.prototype.getPermission = function (roleName) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1_lodash__["find"])(__WEBPACK_IMPORTED_MODULE_2__config__["a" /* config */].roles, { name: roleName }).permission;
    };
    AuthService.prototype.isPermited = function (userRole) {
        return this.getPermission(this.currentRole) === this.getPermission(userRole);
    };
    AuthService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])()
    ], AuthService);
    return AuthService;
}());

//# sourceMappingURL=auth.service.js.map

/***/ }),

/***/ "../../../../../src/app/core/core.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_cdk__ = __webpack_require__("../../../cdk/@angular/cdk.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__covalent_core__ = __webpack_require__("../../../../@covalent/core/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__httpClient_httpClient_service__ = __webpack_require__("../../../../../src/app/core/httpClient/httpClient.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__authentication_auth_guard_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth-guard.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__not_found_not_found_component__ = __webpack_require__("../../../../../src/app/core/not-found/not-found.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CoreModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};












var CoreModule = (function () {
    function CoreModule() {
    }
    CoreModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["t" /* MdCardModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["h" /* MdButtonModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["d" /* MdIconModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["b" /* CovalentMediaModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["c" /* CovalentLayoutModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_cdk__["_0" /* CdkTableModule */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_2__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["d" /* CovalentChipsModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["c" /* CovalentLayoutModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["e" /* CovalentExpansionPanelModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["f" /* CovalentDataTableModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["g" /* CovalentPagingModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["h" /* CovalentDialogsModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["i" /* CovalentLoadingModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["b" /* CovalentMediaModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["j" /* CovalentNotificationsModule */],
                __WEBPACK_IMPORTED_MODULE_5__covalent_core__["k" /* CovalentCommonModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_cdk__["_0" /* CdkTableModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["C" /* MdAutocompleteModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["h" /* MdButtonModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["G" /* MdButtonToggleModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["t" /* MdCardModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["z" /* MdCheckboxModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["B" /* MdChipsModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["H" /* MdCoreModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["I" /* MdDatepickerModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["w" /* MdDialogModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["J" /* MdExpansionModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["K" /* MdGridListModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["d" /* MdIconModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["g" /* MdInputModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["l" /* MdListModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["k" /* MdMenuModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["L" /* MdNativeDateModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["M" /* MdPaginatorModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["m" /* MdProgressBarModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["n" /* MdProgressSpinnerModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["N" /* MdRadioModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["e" /* MdRippleModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["j" /* MdSelectModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["r" /* MdSidenavModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["O" /* MdSliderModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["P" /* MdSlideToggleModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["Q" /* MdSnackBarModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["R" /* MdSortModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["S" /* MdTableModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["T" /* MdTabsModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["s" /* MdToolbarModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_material__["v" /* MdTooltipModule */],
                __WEBPACK_IMPORTED_MODULE_11__not_found_not_found_component__["a" /* NotFoundComponent */],
            ],
            declarations: [__WEBPACK_IMPORTED_MODULE_11__not_found_not_found_component__["a" /* NotFoundComponent */]],
            providers: [
                __WEBPACK_IMPORTED_MODULE_10__authentication_auth_service__["a" /* AuthService */],
                __WEBPACK_IMPORTED_MODULE_9__authentication_auth_guard_service__["a" /* AuthGuardService */],
                __WEBPACK_IMPORTED_MODULE_8__httpClient_httpClient_service__["a" /* HttpClientService */],
                __WEBPACK_IMPORTED_MODULE_7__snackService_snackService_service__["a" /* SnackBarService */],
                __WEBPACK_IMPORTED_MODULE_6__windowService_windowService_service__["a" /* WindowService */],
            ],
        })
    ], CoreModule);
    return CoreModule;
}());

//# sourceMappingURL=core.module.js.map

/***/ }),

/***/ "../../../../../src/app/core/httpClient/httpClient.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_http__ = __webpack_require__("../../../http/@angular/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_Observable__ = __webpack_require__("../../../../rxjs/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_rxjs_Observable___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_4_rxjs_Observable__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HttpClientService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};







var HttpClientService = (function () {
    function HttpClientService(auth, snackService, router, http, window) {
        this.auth = auth;
        this.snackService = snackService;
        this.router = router;
        this.http = http;
        this.window = window;
        this.headers = new __WEBPACK_IMPORTED_MODULE_3__angular_http__["b" /* Headers */]();
        this.headers.append('Content-Type', 'application/json');
    }
    HttpClientService.prototype.setHeaderToken = function (value) {
        this.headers.delete('Authorization');
        if (value) {
            this.headers.append('Authorization', value);
        }
    };
    HttpClientService.prototype.get = function (url) {
        var _this = this;
        return new __WEBPACK_IMPORTED_MODULE_4_rxjs_Observable__["Observable"](function (observer) {
            _this.setHeaderToken(_this.auth.getToken());
            _this.http.get(url, { withCredentials: true, headers: _this.headers })
                .subscribe(function (data) {
                return observer.next(data);
            }, function (error) {
                if (error.status === 400 || error.status === 500) {
                    _this.auth.setLogged(false);
                    _this.auth.setRole('CUSTOMER');
                    _this.auth.setUser('');
                    _this.auth.setToken('');
                    _this.snackService.openSnack(error.json().message, 4000, 'red');
                    _this.router.navigate(['restaurant']);
                }
                return observer.error(error);
            });
        });
    };
    HttpClientService.prototype.post = function (url, data) {
        var _this = this;
        return new __WEBPACK_IMPORTED_MODULE_4_rxjs_Observable__["Observable"](function (observer) {
            _this.setHeaderToken(_this.auth.getToken());
            _this.http.post(url, data, { withCredentials: true, headers: _this.headers })
                .subscribe(function (result) {
                return observer.next(result);
            }, function (error) {
                if (error.status === 400 || error.status === 500) {
                    _this.auth.setLogged(false);
                    _this.auth.setRole('CUSTOMER');
                    _this.auth.setUser('');
                    _this.auth.setToken('');
                    _this.snackService.openSnack(error.json().message, 4000, 'red');
                    _this.router.navigate(['restaurant']);
                }
                return observer.error(error);
            });
        });
    };
    HttpClientService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_5__authentication_auth_service__["a" /* AuthService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__authentication_auth_service__["a" /* AuthService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_3__angular_http__["a" /* Http */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__angular_http__["a" /* Http */]) === "function" && _d || Object, typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_6__windowService_windowService_service__["a" /* WindowService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_6__windowService_windowService_service__["a" /* WindowService */]) === "function" && _e || Object])
    ], HttpClientService);
    return HttpClientService;
    var _a, _b, _c, _d, _e;
}());

//# sourceMappingURL=httpClient.service.js.map

/***/ }),

/***/ "../../../../../src/app/core/not-found/not-found.component.html":
/***/ (function(module, exports) {

module.exports = "<div layout-gt-sm=\"row\" layout-align=\"center\" layout-padding tdMediaToggle=\"gt-xs\" [mediaClasses]=\"['push-sm']\">\r\n  <div flex-gt-md=\"60\" flex-md-sm=\"75\">\r\n    <md-card>\r\n      <md-card-title>\r\n        <md-icon color=\"accent\">pan_tools</md-icon>\r\n        Ups, something went wrong\r\n      </md-card-title>\r\n      <md-card-subtitle>Page not found</md-card-subtitle>\r\n      <md-divider></md-divider>\r\n      <md-card-content>\r\n        <p>The page that you are trying to access <strong>does not exist</strong> or <strong>cannot be found</strong>. Please,\r\n          go to the home page and navigate to other sections.</p>\r\n      </md-card-content>\r\n      <md-divider></md-divider>\r\n      <md-card-actions>\r\n        <a md-raised-button color=\"accent\" class=\"text-upper\" [routerLink]=\"['/restaurant']\">\r\n          Home\r\n        </a>\r\n      </md-card-actions>\r\n    </md-card>\r\n  </div>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../src/app/core/not-found/not-found.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/core/not-found/not-found.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return NotFoundComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var NotFoundComponent = (function () {
    function NotFoundComponent() {
    }
    NotFoundComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-not-found',
            template: __webpack_require__("../../../../../src/app/core/not-found/not-found.component.html"),
            styles: [__webpack_require__("../../../../../src/app/core/not-found/not-found.component.scss")],
        })
    ], NotFoundComponent);
    return NotFoundComponent;
}());

//# sourceMappingURL=not-found.component.js.map

/***/ }),

/***/ "../../../../../src/app/core/snackService/snackService.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SnackBarService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var SnackBarService = (function () {
    function SnackBarService(snackBar) {
        this.snackBar = snackBar;
    }
    SnackBarService.prototype.openSnack = function (message, duration, color) {
        this.snackBar.open(message, 'OK', {
            duration: duration,
            extraClasses: ['bgc-' + color + '-600'],
        });
    };
    SnackBarService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["b" /* MdSnackBar */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["b" /* MdSnackBar */]) === "function" && _a || Object])
    ], SnackBarService);
    return SnackBarService;
    var _a;
}());

//# sourceMappingURL=snackService.service.js.map

/***/ }),

/***/ "../../../../../src/app/core/windowService/windowService.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return WindowService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

function getWindow() {
    return window;
}
var WindowService = (function () {
    function WindowService() {
    }
    Object.defineProperty(WindowService.prototype, "nativeWindow", {
        get: function () {
            return getWindow();
        },
        enumerable: true,
        configurable: true
    });
    WindowService.prototype.responsiveWidth = function () {
        return (getWindow().innerWidth > 800) ? '40%' : '80%';
    };
    WindowService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])()
    ], WindowService);
    return WindowService;
}());

//# sourceMappingURL=windowService.service.js.map

/***/ }),

/***/ "../../../../../src/app/email-confirmations/email-confirmations-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__email_confirmations_component__ = __webpack_require__("../../../../../src/app/email-confirmations/email-confirmations.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EmailConfirmationsRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var emailConfirmationsRoutes = [
    { path: 'booking/acceptInvite/:token', component: __WEBPACK_IMPORTED_MODULE_2__email_confirmations_component__["a" /* EmailConfirmationsComponent */] },
    { path: 'booking/rejectInvite/:token', component: __WEBPACK_IMPORTED_MODULE_2__email_confirmations_component__["a" /* EmailConfirmationsComponent */] },
    { path: 'booking/cancel/:token', component: __WEBPACK_IMPORTED_MODULE_2__email_confirmations_component__["a" /* EmailConfirmationsComponent */] },
    { path: 'booking/cancelOrder/:token', component: __WEBPACK_IMPORTED_MODULE_2__email_confirmations_component__["a" /* EmailConfirmationsComponent */] },
];
var EmailConfirmationsRoutingModule = (function () {
    function EmailConfirmationsRoutingModule() {
    }
    EmailConfirmationsRoutingModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forChild(emailConfirmationsRoutes),
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */],
            ],
        })
    ], EmailConfirmationsRoutingModule);
    return EmailConfirmationsRoutingModule;
}());

//# sourceMappingURL=email-confirmations-routing.module.js.map

/***/ }),

/***/ "../../../../../src/app/email-confirmations/email-confirmations.component.html":
/***/ (function(module, exports) {

module.exports = "<div flex layout=\"column\" style=\"align-items: center; align-content: center\">\r\n    <md-spinner color=\"accent\"></md-spinner>\r\n    <span>Loading request</span>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../src/app/email-confirmations/email-confirmations.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/email-confirmations/email-confirmations.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__shared_email_confirmations_service__ = __webpack_require__("../../../../../src/app/email-confirmations/shared/email-confirmations.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Rx__ = __webpack_require__("../../../../rxjs/Rx.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Rx___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_rxjs_Rx__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EmailConfirmationsComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var EmailConfirmationsComponent = (function () {
    function EmailConfirmationsComponent(snackBarService, emailService, router, route) {
        this.snackBarService = snackBarService;
        this.emailService = emailService;
        this.router = router;
        this.route = route;
    }
    EmailConfirmationsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.switchMap(function (params) { return __WEBPACK_IMPORTED_MODULE_1_rxjs_Rx__["Observable"].of(params.token); })
            .subscribe(function (token) {
            _this.route.url
                .subscribe(function (data) {
                switch (data[1].path) {
                    case 'acceptInvite':
                        _this.emailService.sendAcceptInvitation(token).subscribe(function (res) {
                            _this.snackBarService.openSnack('Invitation succesfully accepted', 10000, 'green');
                        }, function (error) {
                            _this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
                        });
                        break;
                    case 'rejectInvite':
                        _this.emailService.sendRejectInvitation(token).subscribe(function (res) {
                            _this.snackBarService.openSnack('Invitation succesfully rejected', 10000, 'red');
                        }, function (error) {
                            _this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
                        });
                        break;
                    case 'cancel':
                        _this.emailService.sendCancelBooking(token).subscribe(function (res) {
                            _this.snackBarService.openSnack('Booking succesfully canceled', 10000, 'green');
                        }, function (error) {
                            _this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
                        });
                        break;
                    case 'cancelOrder':
                        _this.emailService.sendCancelOrder(token).subscribe(function (res) {
                            _this.snackBarService.openSnack('Order succesfully canceled', 10000, 'green');
                        }, function (error) {
                            _this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
                        });
                        break;
                    default:
                        _this.snackBarService.openSnack('Url not found, please try again', 10000, 'black');
                        break;
                }
            });
        });
        this.router.navigate(['restaurant']);
    };
    EmailConfirmationsComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4__angular_core__["_11" /* Component */])({
            selector: 'public-email-confirmations',
            template: __webpack_require__("../../../../../src/app/email-confirmations/email-confirmations.component.html"),
            styles: [__webpack_require__("../../../../../src/app/email-confirmations/email-confirmations.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__core_snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__core_snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__shared_email_confirmations_service__["a" /* EmailConfirmationsService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__shared_email_confirmations_service__["a" /* EmailConfirmationsService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_3__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__angular_router__["b" /* Router */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_3__angular_router__["d" /* ActivatedRoute */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__angular_router__["d" /* ActivatedRoute */]) === "function" && _d || Object])
    ], EmailConfirmationsComponent);
    return EmailConfirmationsComponent;
    var _a, _b, _c, _d;
}());

//# sourceMappingURL=email-confirmations.component.js.map

/***/ }),

/***/ "../../../../../src/app/email-confirmations/email-confirmations.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__email_confirmations_component__ = __webpack_require__("../../../../../src/app/email-confirmations/email-confirmations.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared_email_confirmations_service__ = __webpack_require__("../../../../../src/app/email-confirmations/shared/email-confirmations.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__email_confirmations_routing_module__ = __webpack_require__("../../../../../src/app/email-confirmations/email-confirmations-routing.module.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EmailConfirmationModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var EmailConfirmationModule = (function () {
    function EmailConfirmationModule() {
    }
    EmailConfirmationModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_2__core_core_module__["a" /* CoreModule */],
                __WEBPACK_IMPORTED_MODULE_5__email_confirmations_routing_module__["a" /* EmailConfirmationsRoutingModule */],
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_4__shared_email_confirmations_service__["a" /* EmailConfirmationsService */],
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_3__email_confirmations_component__["a" /* EmailConfirmationsComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_3__email_confirmations_component__["a" /* EmailConfirmationsComponent */],
            ],
        })
    ], EmailConfirmationModule);
    return EmailConfirmationModule;
}());

//# sourceMappingURL=email-confirmations.module.js.map

/***/ }),

/***/ "../../../../../src/app/email-confirmations/shared/email-confirmations.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__backend_order_order_data_service__ = __webpack_require__("../../../../../src/app/backend/order/order-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_booking_booking_data_service__ = __webpack_require__("../../../../../src/app/backend/booking/booking-data-service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EmailConfirmationsService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var EmailConfirmationsService = (function () {
    function EmailConfirmationsService(orderDataService, bookingDataService) {
        this.orderDataService = orderDataService;
        this.bookingDataService = bookingDataService;
    }
    EmailConfirmationsService.prototype.sendAcceptInvitation = function (token) {
        return this.bookingDataService.acceptInvite(token);
    };
    EmailConfirmationsService.prototype.sendRejectInvitation = function (token) {
        return this.bookingDataService.cancelInvite(token);
    };
    EmailConfirmationsService.prototype.sendCancelBooking = function (token) {
        return this.bookingDataService.cancelReserve(token);
    };
    EmailConfirmationsService.prototype.sendCancelOrder = function (token) {
        return this.orderDataService.cancelOrder(token);
    };
    EmailConfirmationsService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__backend_order_order_data_service__["a" /* OrderDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__backend_order_order_data_service__["a" /* OrderDataService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__backend_booking_booking_data_service__["a" /* BookingDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__backend_booking_booking_data_service__["a" /* BookingDataService */]) === "function" && _b || Object])
    ], EmailConfirmationsService);
    return EmailConfirmationsService;
    var _a, _b;
}());

//# sourceMappingURL=email-confirmations.service.js.map

/***/ }),

/***/ "../../../../../src/app/header/header.component.html":
/***/ (function(module, exports) {

module.exports = "<div layout=\"row\" layout-align=\"center center\" style=\"align-items: center\" flex>\r\n  <a md-icon-button class=\"forMobile\" (click)=\"openCloseNavigationSideNav()\"><md-icon>dehaze</md-icon></a>\r\n  <a md-icon-button><md-icon>star_border</md-icon></a>\r\n  <span>My Thai Star</span>\r\n  \r\n  <span flex></span>\r\n  <div *ngIf=\"auth.isPermited('CUSTOMER')\" class=\"forDesktop\">\r\n    <nav md-tab-nav-bar>\r\n      <a md-tab-link\r\n        routerLink=\"/restaurant\"\r\n        routerLinkActive #restaurant=\"routerLinkActive\"\r\n        [class.navBottomBorder]=\"restaurant.isActive\">\r\n        HOME\r\n      </a>\r\n      <a md-tab-link\r\n        routerLink=\"/menu\"\r\n        routerLinkActive #menu=\"routerLinkActive\"\r\n        [class.navBottomBorder]=\"menu.isActive\">\r\n        MENU\r\n      </a>\r\n      <a md-tab-link\r\n        routerLink=\"/bookTable\"\r\n        routerLinkActive #bookTable=\"routerLinkActive\"\r\n        [class.navBottomBorder]=\"bookTable.isActive\">\r\n        BOOK TABLE\r\n      </a>\r\n    </nav>\r\n  </div>\r\n  <div *ngIf=\"auth.isPermited('WAITER')\" class=\"forDesktop\">\r\n    <nav md-tab-nav-bar>\r\n      <a md-tab-link\r\n        routerLink=\"/orders\"\r\n        routerLinkActive #orders=\"routerLinkActive\"\r\n        [class.navBottomBorder]=\"orders.isActive\">\r\n        ORDERS\r\n      </a>\r\n      <a md-tab-link\r\n        routerLink=\"/reservations\"\r\n        routerLinkActive #reservations=\"routerLinkActive\"\r\n        [class.navBottomBorder]=\"reservations.isActive\">\r\n        RESERVATIONS\r\n      </a>\r\n    </nav>\r\n  </div>\r\n  <button *ngIf=\"!auth.isLogged()\" md-icon-button (click)=\"openLoginDialog()\" mdTooltip=\"User\"><md-icon>person_outline</md-icon></button>\r\n  <div *ngIf=\"auth.isLogged()\" layout=\"row\" style=\"align-items:center\">\r\n    <button md-icon-button [mdMenuTriggerFor]=\"toolbarMenu1\"><md-icon>account_circle</md-icon></button>\r\n    <span class=\"forDesktop\">{{auth.user}}</span>\r\n    <button class=\"forDesktop\" *ngIf=\"!auth.isLogged()\" md-icon-button (click)=\"logout()\" mdTooltip=\"log out\"><md-icon>exit_to_app</md-icon></button>\r\n    <md-menu #toolbarMenu1=\"mdMenu\">\r\n      <button md-menu-item type=\"button\" (click)=\"openResetDialog()\" *ngIf=\"auth.isPermited('CUSTOMER')\">\r\n        <md-icon>settings</md-icon>\r\n        <span>Change password</span>\r\n      </button>\r\n      <button md-menu-item type=\"button\" (click)=\"openTwitterDialog()\" *ngIf=\"auth.isPermited('CUSTOMER')\">\r\n        <md-icon>send</md-icon>\r\n        <span>Twitter account</span>\r\n      </button>\r\n      <md-divider></md-divider>\r\n      <button md-menu-item type=\"button\" (click)=\"logout()\">\r\n        <md-icon>exit_to_app</md-icon>\r\n        <span>Sign out</span>\r\n      </button>\r\n    </md-menu>\r\n  </div>\r\n  <td-notification-count *ngIf=\"auth.isPermited('CUSTOMER')\" color=\"accent\" [notifications]=\"sidenav.getNumberOrders()\">\r\n    <button md-icon-button (click)=\"openCloseSideNav(sidenav.opened)\" mdTooltip=\"Orders\"><md-icon>shopping_basket</md-icon></button>\r\n  </td-notification-count>\r\n</div>"

/***/ }),

/***/ "../../../../../src/app/header/header.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "nav {\n  border-bottom: none;\n  margin-right: 20px; }\n\nnav a {\n  min-width: 100px !important;\n  color: white; }\n\n.navBottomBorder {\n  border-bottom: 3px solid white; }\n\nmd-sidenav {\n  width: 400px; }\n\n@media (max-width: 600px) {\n  .forDesktop {\n    display: none; }\n  .sidenavMobile {\n    width: 100%; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/header/header.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__sidenav_shared_sidenav_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/sidenav.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__user_area_shared_user_area_service__ = __webpack_require__("../../../../../src/app/user-area/shared/user-area.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__core_windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__user_area_login_dialog_login_dialog_component__ = __webpack_require__("../../../../../src/app/user-area/login-dialog/login-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__user_area_password_dialog_password_dialog_component__ = __webpack_require__("../../../../../src/app/user-area/password-dialog/password-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__user_area_twitter_dialog_twitter_dialog_component__ = __webpack_require__("../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HeaderComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};










var HeaderComponent = (function () {
    function HeaderComponent(window, router, sidenav, dialog, auth, userService) {
        this.window = window;
        this.router = router;
        this.sidenav = sidenav;
        this.dialog = dialog;
        this.auth = auth;
        this.userService = userService;
        this.sidenavNavigationEmitter = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["r" /* EventEmitter */]();
    }
    HeaderComponent.prototype.openCloseSideNav = function (sidenavOpened) {
        sidenavOpened ? this.sidenav.closeSideNav() : this.sidenav.openSideNav();
    };
    HeaderComponent.prototype.openCloseNavigationSideNav = function () {
        this.sidenavNavigationEmitter.emit();
    };
    HeaderComponent.prototype.navigateTo = function (route) {
        this.router.navigate([route]);
        this.sidenavNavigationEmitter.emit();
    };
    HeaderComponent.prototype.openLoginDialog = function () {
        var _this = this;
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_7__user_area_login_dialog_login_dialog_component__["a" /* LoginDialogComponent */], {
            width: this.window.responsiveWidth(),
        });
        dialogRef.afterClosed().subscribe(function (result) {
            if (result) {
                if (result.email) {
                    _this.userService.register(result.email, result.password);
                }
                else {
                    _this.userService.login(result.username, result.password);
                }
            }
        });
    };
    HeaderComponent.prototype.openResetDialog = function () {
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_8__user_area_password_dialog_password_dialog_component__["a" /* PasswordDialogComponent */], {
            width: this.window.responsiveWidth(),
        });
    };
    HeaderComponent.prototype.openTwitterDialog = function () {
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_9__user_area_twitter_dialog_twitter_dialog_component__["a" /* TwitterDialogComponent */], {
            width: this.window.responsiveWidth(),
        });
    };
    HeaderComponent.prototype.logout = function () {
        this.userService.logout();
        this.router.navigate(['restaurant']);
    };
    __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["u" /* Output */])('openCloseSidenavMobile'),
        __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["r" /* EventEmitter */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["r" /* EventEmitter */]) === "function" && _a || Object)
    ], HeaderComponent.prototype, "sidenavNavigationEmitter", void 0);
    HeaderComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-header',
            template: __webpack_require__("../../../../../src/app/header/header.component.html"),
            styles: [__webpack_require__("../../../../../src/app/header/header.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_6__core_windowService_windowService_service__["a" /* WindowService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_6__core_windowService_windowService_service__["a" /* WindowService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_2__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_router__["b" /* Router */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_4__sidenav_shared_sidenav_service__["a" /* SidenavService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__sidenav_shared_sidenav_service__["a" /* SidenavService */]) === "function" && _d || Object, typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["a" /* MdDialog */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["a" /* MdDialog */]) === "function" && _e || Object, typeof (_f = typeof __WEBPACK_IMPORTED_MODULE_3__core_authentication_auth_service__["a" /* AuthService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_authentication_auth_service__["a" /* AuthService */]) === "function" && _f || Object, typeof (_g = typeof __WEBPACK_IMPORTED_MODULE_5__user_area_shared_user_area_service__["a" /* UserAreaService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__user_area_shared_user_area_service__["a" /* UserAreaService */]) === "function" && _g || Object])
    ], HeaderComponent);
    return HeaderComponent;
    var _a, _b, _c, _d, _e, _f, _g;
}());

//# sourceMappingURL=header.component.js.map

/***/ }),

/***/ "../../../../../src/app/header/header.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_routing_module__ = __webpack_require__("../../../../../src/app/app-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__header_component__ = __webpack_require__("../../../../../src/app/header/header.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HeaderModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var HeaderModule = (function () {
    function HeaderModule() {
    }
    HeaderModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_2__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_0__core_core_module__["a" /* CoreModule */],
                __WEBPACK_IMPORTED_MODULE_3__app_routing_module__["a" /* AppRoutingModule */],
            ],
            providers: [],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_4__header_component__["a" /* HeaderComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_4__header_component__["a" /* HeaderComponent */],
            ],
        })
    ], HeaderModule);
    return HeaderModule;
}());

//# sourceMappingURL=header.module.js.map

/***/ }),

/***/ "../../../../../src/app/home/home.component.html":
/***/ (function(module, exports) {

module.exports = "<td-layout>\r\n  <div class=\"home-title\" flex>\r\n    <div layout=\"column\" class=\"property-text-center\">\r\n      <h1 class= \"title-color title\">MY THAI STAR</h1>\r\n      <span class= \"title-color pull-top-xl subtitle\">More than just delicious food</span>\r\n    </div>\r\n  </div>\r\n  <md-card id=\"homeCard\" class=\"card-over push-left-xl push-right-xl\">\r\n    <div layout-gt-xs=\"row\" class=\"pad\" flex>\r\n      <md-card>\r\n        <img md-card-image src=\"../../assets/images/thai-restaurant.jpg\">\r\n        <md-card-content>\r\n          <div class=\"property-text-center pad\">\r\n            <h3>OUR RESTAURANT</h3>\r\n            <p>\r\n                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent elementum tellus eget volutpat porta. Duis pellentesque velit venenatis, tincidunt ligula ac, rhoncus felis. Donec aliquam nulla id turpis dignissim laoreet. Vestibulum sit amet ante eu lacus convallis lobortis sit amet eu orci. Nam vitae felis ac nisi accumsan faucibus.\r\n            </p>\r\n          </div>\r\n        </md-card-content>\r\n        <md-card-actions class=\"property-text-center\">\r\n          <button md-raised-button color=\"accent\" (click)=\"navigateTo('bookTable')\">BOOK A TABLE</button>\r\n        </md-card-actions>\r\n      </md-card>\r\n      <md-card>\r\n        <img md-card-image src=\"../../assets/images/thai-restaurant-dish.jpg\">\r\n        <md-card-content>\r\n          <div class=\"property-text-center pad\">\r\n            <h3>OUR MENU</h3>\r\n            <p>\r\n                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent elementum tellus eget volutpat porta. Duis pellentesque velit venenatis, tincidunt ligula ac, rhoncus felis. Donec aliquam nulla id turpis dignissim laoreet. Vestibulum sit amet ante eu lacus convallis lobortis sit amet eu orci. Nam vitae felis ac nisi accumsan faucibus.\r\n            </p>\r\n          </div>\r\n        </md-card-content>\r\n        <md-card-actions class=\"property-text-center\">\r\n          <button md-raised-button color=\"accent\" (click)=\"navigateTo('menu')\">VIEW MENU</button>\r\n        </md-card-actions>\r\n      </md-card>\r\n    </div>\r\n  </md-card>\r\n</td-layout>"

/***/ }),

/***/ "../../../../../src/app/home/home.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".title-color {\n  color: white; }\n\n@media (min-width: 1300px) {\n  .star-image {\n    position: absolute;\n    left: 50%;\n    top: 1%; }\n  .title {\n    font-size: 3em; }\n  .subtitle {\n    font-size: 1em; } }\n\n@media (max-width: 1300px) {\n  .star-image {\n    position: absolute;\n    left: 53%;\n    top: -2%; }\n  .title {\n    font-size: 1.5em; }\n  .subtitle {\n    padding-top: 10px;\n    font-size: 0.7em; }\n  .sizeImg {\n    max-width: 75%; } }\n\n.card-over {\n  margin-top: -75px; }\n\n.home-title {\n  padding-bottom: 25%;\n  padding-top: 5%;\n  background-image: url(" + __webpack_require__("../../../../../src/assets/images/background-dish.png") + "), url(" + __webpack_require__("../../../../../src/assets/images/star.png") + "), url(" + __webpack_require__("../../../../../src/assets/images/Wood2.jpg") + ");\n  background-position: bottom, top, center;\n  background-size: cover, 15%, cover;\n  background-repeat: no-repeat, no-repeat, repeat; }\n\n@media (max-width: 600px) {\n  .forDesktop {\n    display: none; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/home/home.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HomeComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HomeComponent = (function () {
    function HomeComponent(router) {
        this.router = router;
    }
    HomeComponent.prototype.navigateTo = function (route) {
        this.router.navigate([route]);
    };
    HomeComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_11" /* Component */])({
            selector: 'public-home',
            template: __webpack_require__("../../../../../src/app/home/home.component.html"),
            styles: [__webpack_require__("../../../../../src/app/home/home.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */]) === "function" && _a || Object])
    ], HomeComponent);
    return HomeComponent;
    var _a;
}());

//# sourceMappingURL=home.component.js.map

/***/ }),

/***/ "../../../../../src/app/home/home.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__home_component__ = __webpack_require__("../../../../../src/app/home/home.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return HomeModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var HomeModule = (function () {
    function HomeModule() {
    }
    HomeModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_2__core_core_module__["a" /* CoreModule */],
            ],
            providers: [],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_3__home_component__["a" /* HomeComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_3__home_component__["a" /* HomeComponent */],
            ],
        })
    ], HomeModule);
    return HomeModule;
}());

//# sourceMappingURL=home.module.js.map

/***/ }),

/***/ "../../../../../src/app/index.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__app_module__ = __webpack_require__("../../../../../src/app/app.module.ts");
/* harmony reexport (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return __WEBPACK_IMPORTED_MODULE_0__app_module__["a"]; });

//# sourceMappingURL=index.js.map

/***/ }),

/***/ "../../../../../src/app/menu/menu-card/menu-card.component.html":
/***/ (function(module, exports) {

module.exports = "<md-card class=\"card-height\" flex>\r\n  <div layout-gt-xs=\"row\" flex>\r\n    <div flex-gt-xs=\"50\" layout=\"column\" class= \"menu-image card-image-height card-height\" [style.background-image]=\"'url(' + menuInfo.image.content + ')'\">\r\n      <button md-fab *ngIf=\"auth.isLogged()\" (click)=\"changeFavouriteState()\" class=\"push-sm favourite-button\">\r\n        <md-icon [class.fab-on]=\"!menuInfo.isfav\" [class.fab-off]=\"menuInfo.isfav\">star</md-icon>\r\n      </button>\r\n      <span flex></span>\r\n      <div style=\"background-color: rgba(0,0,0,0.3)\" flex>\r\n        <md-list>\r\n          <md-list-item>\r\n            <img md-list-avatar src=\"https://help.github.com/assets/images/help/profile/identicon.png\">\r\n            <p md-line class=\"twitter-title\">John Smith <span class=\"twitter-text\">@jsmith</span></p>\r\n            <p md-line class=\"twitter-text\">{{\"ipsum dolor sits it sit sem ipsum dolor sits its it  sem ipsum dolor sits itsit sem ipsum sem ipsum dolor sits itsit sem ipsumsem ipsum dolor sits itsit sem ipsum dolor s its \" | truncate: 100}}</p>\r\n          </md-list-item>\r\n          <md-list-item>\r\n            <img md-list-avatar src=\"https://help.github.com/assets/images/help/profile/identicon.png\">\r\n            <p md-line class=\"twitter-title\">Ann Smith <span class=\"twitter-text\">@annS</span></p>\r\n            <p md-line class=\"twitter-text\">{{\"ipsum dolor sits it sit sem ipsum dolor sits its it  sem ipsum dolor sits itsit sem ipsum sem ipsum dolor sits itsit sem ipsumsem ipsum dolor sits itsit sem ipsum dolor s its \" | truncate: 100}}</p>\r\n          </md-list-item>\r\n        </md-list>\r\n      </div>\r\n    </div>\r\n\r\n    <div flex-gt-xs=\"50\" layout=\"column\" class= \"pad\" flex>\r\n      <div layout=\"row\" flex>\r\n        <span flex></span>\r\n        <span class=\"price\">{{menuInfo.dish.price | number : '1.2-2'}} €</span>\r\n      </div>\r\n      <h3 class=\"text-upper\">{{menuInfo.dish.name}}</h3>\r\n      <span class=\"push-bottom-sm\" flex>\r\n        {{menuInfo.dish.description}}\r\n      </span>\r\n      <div layout-gt-xs=\"column\" layout-xs=\"row\" class=\"justify-space-between\" flex>\r\n        <md-checkbox class=\"push-bottom-sm\" [checked]=\"extra.selected\" *ngFor=\"let extra of menuInfo.extras\" (change)=\"selectedOption(extra)\">{{extra.name}} +{{extra.price}}€</md-checkbox>\r\n      </div>\r\n      <span flex></span>\r\n      <div class=\"align-right\" flex>\r\n        <button md-button (click)=\"addOrderMenu()\" color=\"accent\" class=\"text-upper property-text-bold\">Add to order</button>\r\n      </div>\r\n    </div>\r\n  </div>\r\n\r\n</md-card>"

/***/ }),

/***/ "../../../../../src/app/menu/menu-card/menu-card.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".favourite-button {\n  -ms-flex-item-align: end;\n      align-self: flex-end;\n  background-color: white; }\n\n.price {\n  font-size: 2em;\n  color: #514f4f; }\n\n.fab-on {\n  color: lightgray; }\n\n.fab-off {\n  color: sandybrown; }\n\n.twitter-title {\n  font-size: 1.1em !important;\n  color: white !important;\n  white-space: normal !important; }\n\n.twitter-text {\n  font-size: 0.8em !important;\n  color: white !important;\n  white-space: normal !important; }\n\n.menu-image {\n  background-size: cover;\n  background-repeat: no-repeat; }\n\n@media (min-width: 900px) {\n  .card-height {\n    height: 50vh; } }\n\n@media (max-width: 900px) {\n  .card-image-height {\n    height: 50vh; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/menu/menu-card/menu-card.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__shared_viewModels_interfaces__ = __webpack_require__("../../../../../src/app/shared/viewModels/interfaces.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__shared_viewModels_interfaces___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1__shared_viewModels_interfaces__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__sidenav_shared_sidenav_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/sidenav.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_menu_service__ = __webpack_require__("../../../../../src/app/menu/shared/menu.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuCardComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var MenuCardComponent = (function () {
    function MenuCardComponent(menuService, auth, sidenav) {
        this.menuService = menuService;
        this.auth = auth;
        this.sidenav = sidenav;
    }
    MenuCardComponent.prototype.addOrderMenu = function () {
        this.sidenav.addOrder(this.menuService.menuToOrder(this.menuInfo));
        this.sidenav.openSideNav();
        this.menuService.clearSelectedExtras(this.menuInfo);
    };
    MenuCardComponent.prototype.changeFavouriteState = function () {
        this.menuInfo.isfav = !this.menuInfo.isfav;
    };
    MenuCardComponent.prototype.selectedOption = function (extra) {
        extra.selected = !extra.selected;
    };
    __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Input */])('menu'),
        __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__shared_viewModels_interfaces__["DishView"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__shared_viewModels_interfaces__["DishView"]) === "function" && _a || Object)
    ], MenuCardComponent.prototype, "menuInfo", void 0);
    MenuCardComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-menu-card',
            template: __webpack_require__("../../../../../src/app/menu/menu-card/menu-card.component.html"),
            styles: [__webpack_require__("../../../../../src/app/menu/menu-card/menu-card.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_3__shared_menu_service__["a" /* MenuService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__shared_menu_service__["a" /* MenuService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_4__core_authentication_auth_service__["a" /* AuthService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_authentication_auth_service__["a" /* AuthService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_2__sidenav_shared_sidenav_service__["a" /* SidenavService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__sidenav_shared_sidenav_service__["a" /* SidenavService */]) === "function" && _d || Object])
    ], MenuCardComponent);
    return MenuCardComponent;
    var _a, _b, _c, _d;
}());

//# sourceMappingURL=menu-card.component.js.map

/***/ }),

/***/ "../../../../../src/app/menu/menu.component.html":
/***/ (function(module, exports) {

module.exports = "<td-layout>\r\n  <form (ngSubmit)=\"applyFilters(filterForm.form.value)\" #filterForm=\"ngForm\">\r\n    <div style= \"background-color: white\">\r\n      \r\n      <div layout-gt-xs=\"row\" class=\"justify-space-between pad-left-md pad-right-md\" style=\"align-items:center\" flex>\r\n        <div layout=\"row\" style=\"align-items:center\" class=\"pad-top\" flex>\r\n          <button md-icon-button><md-icon>search</md-icon></button>\r\n          <md-input-container color=\"accent\" class=\"searchBy\">\r\n            <input mdInput placeholder=\"Search our dishes\" ngModel name=\"searchBy\">\r\n          </md-input-container>\r\n        </div>\r\n        <div layout=\"row\" class=\"justify-space-between pad-bottom-sm pad-top-sm forMobile\" flex>\r\n          <md-checkbox ngModel name=\"1\">Main dishes</md-checkbox>\r\n          <md-checkbox ngModel name=\"2\">Starter</md-checkbox>\r\n          <md-checkbox ngModel name=\"3\">Dessert</md-checkbox>\r\n        </div>\r\n        <div layout=\"row\" class=\"pull-bottom pad-sm sortingBy\">\r\n          <md-select flex ngModel name=\"sortName\" #sorting placeholder=\"Sort By\">\r\n            <md-option value=\"price\">\r\n              Price\r\n            </md-option>\r\n            <md-option value=\"likes\">\r\n              Likes\r\n            </md-option>\r\n            <md-option value=\"name\">\r\n              Name\r\n            </md-option>\r\n          </md-select>\r\n          <button md-icon-button type=\"button\" (click)=\"changeSortDir()\" [mdTooltip]=\"sortDir\"><md-icon>{{sortDirIcon}}</md-icon></button>\r\n        </div>\r\n      </div>\r\n      <md-divider></md-divider>\r\n\r\n      <td-expansion-panel label=\"FILTERS\">\r\n        <div layout=\"column\" class =\"pad-left-md pad-top pad-right-md\" flex>\r\n          \r\n          <div layout-gt-xs=\"column\" class=\"justify-space-between flex-wrap forMobile\" style=\"margin-bottom: 5px\" flex>\r\n            <div layout=\"row\" class=\"justify-space-between pad-bottom-sm pad-top-sm\" flex>\r\n              <md-checkbox ngModel name=\"3\">Noodle</md-checkbox>\r\n              <md-checkbox ngModel name=\"4\">Rice</md-checkbox>\r\n              <md-checkbox ngModel name=\"5\">Curry</md-checkbox>\r\n            </div>    \r\n            <md-divider></md-divider>\r\n            <div layout=\"row\" class=\"justify-space-between pad-bottom-sm pad-top-sm\" flex>\r\n              <md-checkbox ngModel name=\"6\">Vegan</md-checkbox>\r\n              <md-checkbox ngModel name=\"7\">Vegetarian</md-checkbox>\r\n            </div>   \r\n            <md-divider></md-divider>            \r\n            <div layout=\"row\" class=\"justify-space-between pad-bottom-sm pad-top-sm\" flex>\r\n              <md-checkbox ngModel name=\"8\">Favourites</md-checkbox>\r\n            </div>\r\n            <md-divider></md-divider>\r\n          </div>\r\n\r\n          <div layout-gt-xs=\"row\" class=\"justify-space-between flex-wrap forDesktop\" style=\"margin-bottom: 5px\" flex>\r\n            <md-checkbox ngModel name=\"0\">Main dishes</md-checkbox>\r\n            <md-checkbox ngModel name=\"1\">Starter</md-checkbox>\r\n            <md-checkbox ngModel name=\"2\">Dessert</md-checkbox>\r\n            <span style=\"border-right: 1px solid lightgray\"></span>\r\n            <md-checkbox ngModel name=\"3\">Noodle</md-checkbox>\r\n            <md-checkbox ngModel name=\"4\">Rice</md-checkbox>\r\n            <md-checkbox ngModel name=\"5\">Curry</md-checkbox>\r\n            <span style=\"border-right: 1px solid lightgray\"></span>        \r\n            <md-checkbox ngModel name=\"6\">Vegan</md-checkbox>\r\n            <md-checkbox ngModel name=\"7\">Vegetarian</md-checkbox>\r\n            <span style=\"border-right: 1px solid lightgray\"></span>        \r\n            <md-checkbox ngModel name=\"8\">Favourites</md-checkbox>\r\n          </div>\r\n\r\n          <div layout-gt-xs=\"row\" class=\"push-top-sm\" flex>\r\n            <div flex-gt-xs=\"50\" layout=\"column\" flex>\r\n              <span class =\"pull-bottom-md\">Price</span>\r\n              <md-slider\r\n                #price\r\n                ngModel\r\n                name=\"maxPrice\"\r\n                max=\"50\"\r\n                min=\"1\"\r\n                step=\"1\"\r\n                thumb-label=\"true\"\r\n                tick-interval=\"10\">\r\n              </md-slider>\r\n            </div>\r\n            <div flex-gt-xs=\"50\" layout=\"column\" flex>\r\n              <span class =\"pull-bottom-md\">Likes</span>\r\n              <md-slider\r\n                #likes\r\n                ngModel\r\n                name=\"minLikes\"\r\n                max=500\r\n                min=0\r\n                step=5\r\n                thumb-label=\"true\"\r\n                tick-interval=\"25\">\r\n              </md-slider>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </td-expansion-panel>\r\n      <div class=\"align-right\">\r\n        <button md-button (click)=\"clearFilters(filterForm.form, price, likes)\" class=\"text-upper property-text-bold\">Clear filters</button>\r\n        <button md-button type=\"submit\" color=\"accent\" class=\"text-upper property-text-bold\">Apply filters</button>\r\n      </div>\r\n    </div>\r\n  </form>\r\n\r\n  <div flex layout=\"row\" *ngIf=\"menus | async as allMenus; else loading\" class=\"justify-space-around flex-wrap\">\r\n    <public-menu-card *ngFor=\"let menu of allMenus\" [menu]=\"menu\"></public-menu-card>\r\n  </div>\r\n  \r\n  <ng-template #loading>\r\n    <md-progress-bar\r\n      #loading\r\n      color=\"accent\"\r\n      mode=\"indeterminate\">\r\n    </md-progress-bar>\r\n  </ng-template>\r\n\r\n</td-layout>\r\n"

/***/ }),

/***/ "../../../../../src/app/menu/menu.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".filter-group {\n  display: -webkit-inline-box;\n  display: -ms-inline-flexbox;\n  display: inline-flex;\n  -webkit-box-orient: vertical;\n  -webkit-box-direction: normal;\n      -ms-flex-direction: column;\n          flex-direction: column;\n  border-bottom: 1px solid grey;\n  margin-bottom: 5%;\n  padding-bottom: 5%;\n  width: 100%; }\n\n.filter-title {\n  font-size: 1.1 em;\n  font-weight: bold;\n  margin-bottom: 10px;\n  margin-top: 10px; }\n\n.margin-checkbox {\n  margin-left: 15px; }\n\n.bottom-border {\n  border-bottom: 1px solid grey; }\n\n@media (min-width: 1300px) {\n  public-menu-card {\n    width: 50%; } }\n\n@media (max-width: 1300px) {\n  public-menu-card {\n    width: 75%; } }\n\n@media (max-width: 600px) {\n  .forDesktop {\n    display: none; }\n  .searchBy {\n    width: 100%; }\n  public-menu-card {\n    width: 100%; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; }\n  .sortingBy {\n    width: 30%; }\n  .searchBy {\n    width: 30%; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/menu/menu.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__shared_menu_service__ = __webpack_require__("../../../../../src/app/menu/shared/menu.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var MenuComponent = (function () {
    function MenuComponent(menuService) {
        this.menuService = menuService;
        this.sortDir = 'DESC';
        this.sortDirIcon = 'vertical_align_bottom';
    }
    MenuComponent.prototype.ngOnInit = function () {
        this.applyFilters(undefined);
    };
    MenuComponent.prototype.changeSortDir = function () {
        this.sortDir = (this.sortDir === 'ASC') ? 'DESC' : 'ASC';
        this.sortDirIcon = (this.sortDirIcon === 'vertical_align_bottom') ? 'vertical_align_top' : 'vertical_align_bottom';
    };
    MenuComponent.prototype.applyFilters = function (filters) {
        this.menus = this.menuService.getDishes(this.menuService.composeFilters(filters, this.sortDir));
    };
    MenuComponent.prototype.clearFilters = function (form, price, likes) {
        likes.value = 0;
        price.value = 0;
        form.reset();
        this.applyFilters(undefined);
    };
    MenuComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-menu',
            template: __webpack_require__("../../../../../src/app/menu/menu.component.html"),
            styles: [__webpack_require__("../../../../../src/app/menu/menu.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__shared_menu_service__["a" /* MenuService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__shared_menu_service__["a" /* MenuService */]) === "function" && _a || Object])
    ], MenuComponent);
    return MenuComponent;
    var _a;
}());

//# sourceMappingURL=menu.component.js.map

/***/ }),

/***/ "../../../../../src/app/menu/menu.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__shared_menu_service__ = __webpack_require__("../../../../../src/app/menu/shared/menu.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__menu_card_menu_card_component__ = __webpack_require__("../../../../../src/app/menu/menu-card/menu-card.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__menu_component__ = __webpack_require__("../../../../../src/app/menu/menu.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








var MenuModule = (function () {
    function MenuModule() {
    }
    MenuModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_2__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_4__angular_material__["U" /* MaterialModule */],
                __WEBPACK_IMPORTED_MODULE_0__core_core_module__["a" /* CoreModule */],
                __WEBPACK_IMPORTED_MODULE_3__angular_forms__["b" /* FormsModule */],
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_5__shared_menu_service__["a" /* MenuService */],
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_7__menu_component__["a" /* MenuComponent */],
                __WEBPACK_IMPORTED_MODULE_6__menu_card_menu_card_component__["a" /* MenuCardComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_7__menu_component__["a" /* MenuComponent */],
            ],
            entryComponents: [],
        })
    ], MenuModule);
    return MenuModule;
}());

//# sourceMappingURL=menu.module.js.map

/***/ }),

/***/ "../../../../../src/app/menu/shared/menu.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__backend_dishes_dishes_data_service__ = __webpack_require__("../../../../../src/app/backend/dishes/dishes-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MenuService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var MenuService = (function () {
    function MenuService(dishesDataService) {
        this.dishesDataService = dishesDataService;
    }
    MenuService.prototype.menuToOrder = function (menu) {
        var order;
        order = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["assign"])(order, menu);
        order.orderLine = {
            amount: 1,
            comment: '',
        };
        return order;
    };
    MenuService.prototype.composeFilters = function (filters, sortDir) {
        var filtersComposed;
        var categories = [];
        if (filters) {
            __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["map"])(filters, function (value, field) {
                if (value === true) {
                    categories.push({ id: field });
                }
            });
            filtersComposed = {
                categories: categories,
                searchBy: filters.searchBy,
                sort: [{
                        name: filters.sortName,
                        direction: sortDir,
                    }],
                maxPrice: filters.maxPrice,
                minLikes: filters.minLikes,
                isFav: filters.isFav,
            };
        }
        else {
            filtersComposed = {
                searchBy: undefined,
                sort: [],
                maxPrice: undefined,
                minLikes: undefined,
                isFav: undefined,
                categories: categories,
            };
        }
        return filtersComposed;
    };
    MenuService.prototype.clearSelectedExtras = function (menuInfo) {
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["map"])(menuInfo.extras, function (extra) { extra.selected = false; });
    };
    MenuService.prototype.getDishes = function (filters) {
        return this.dishesDataService.filter(filters);
    };
    MenuService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__backend_dishes_dishes_data_service__["a" /* DishesDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__backend_dishes_dishes_data_service__["a" /* DishesDataService */]) === "function" && _a || Object])
    ], MenuService);
    return MenuService;
    var _a;
}());

//# sourceMappingURL=menu.service.js.map

/***/ }),

/***/ "../../../../../src/app/shared/directives/assistant-validator.directive.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AssistantsValidatorDirective; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var AssistantsValidatorDirective = (function () {
    var AssistantsValidatorDirective = AssistantsValidatorDirective_1 = function AssistantsValidatorDirective() {
    };
    AssistantsValidatorDirective.prototype.validate = function (c) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["isInteger"])(c.value) && c.value > 0 && c.value < 9 ? undefined : {
            validateAssistants: {
                valid: false,
            },
        };
    };
    AssistantsValidatorDirective = AssistantsValidatorDirective_1 = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["i" /* Directive */])({
            selector: '[validateAssistants][formControlName], [validateAssistants][formControl],[validateAssistants][ngModel]',
            providers: [
                { provide: __WEBPACK_IMPORTED_MODULE_1__angular_forms__["f" /* NG_VALIDATORS */], useExisting: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_12" /* forwardRef */])(function () { return AssistantsValidatorDirective_1; }), multi: true },
            ],
        })
    ], AssistantsValidatorDirective);
    return AssistantsValidatorDirective;
    var AssistantsValidatorDirective_1;
}());

//# sourceMappingURL=assistant-validator.directive.js.map

/***/ }),

/***/ "../../../../../src/app/shared/directives/email-validator.directive.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony export (immutable) */ __webpack_exports__["a"] = emailValidator;
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return EmailValidatorDirective; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


// Function exported to be used in this directive and unit tests
function emailValidator(c) {
    var regExp = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    return regExp.test(c);
}
var EmailValidatorDirective = (function () {
    var EmailValidatorDirective = EmailValidatorDirective_1 = function EmailValidatorDirective() {
    };
    EmailValidatorDirective.prototype.validate = function (c) {
        return emailValidator(c.value) ? undefined : {
            validateEmail: {
                valid: false,
            },
        };
    };
    EmailValidatorDirective = EmailValidatorDirective_1 = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["i" /* Directive */])({
            selector: '[validateEmail][formControlName], [validateEmail][formControl],[validateEmail][ngModel]',
            providers: [
                { provide: __WEBPACK_IMPORTED_MODULE_1__angular_forms__["f" /* NG_VALIDATORS */], useExisting: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_12" /* forwardRef */])(function () { return EmailValidatorDirective_1; }), multi: true },
            ],
        })
    ], EmailValidatorDirective);
    return EmailValidatorDirective;
    var EmailValidatorDirective_1;
}());

//# sourceMappingURL=email-validator.directive.js.map

/***/ }),

/***/ "../../../../../src/app/shared/directives/equal-validator.directive.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_forms__ = __webpack_require__("../../../forms/@angular/forms.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return EqualValidatorDirective; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var EqualValidatorDirective = (function () {
    var EqualValidatorDirective = EqualValidatorDirective_1 = function EqualValidatorDirective() {
    };
    EqualValidatorDirective.prototype.validate = function (control) {
        // control value
        var controlValue = control.root.get(this.validateEqual);
        if (!controlValue) {
            return undefined;
        }
        if (this.isReverse()) {
            if (this.isValueEqual(control, controlValue)) {
                delete controlValue.errors.validateEqual;
                if (!Object.keys(controlValue.errors).length) {
                    controlValue.setErrors(undefined);
                }
            }
            else {
                controlValue.setErrors({
                    validateEqual: false,
                });
            }
        }
        else {
            if (!this.isValueEqual(control, controlValue)) {
                return {
                    validateEqual: false,
                };
            }
        }
        return undefined;
    };
    EqualValidatorDirective.prototype.isValueEqual = function (c1, c2) {
        return c1.value === c2.value;
    };
    EqualValidatorDirective.prototype.isReverse = function () {
        return this.reverse && this.reverse === 'true';
    };
    __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Input */])('validateEqual'),
        __metadata("design:type", String)
    ], EqualValidatorDirective.prototype, "validateEqual", void 0);
    __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Input */])('reverse'),
        __metadata("design:type", String)
    ], EqualValidatorDirective.prototype, "reverse", void 0);
    EqualValidatorDirective = EqualValidatorDirective_1 = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["i" /* Directive */])({
            selector: '[validateEqual][formControlName],[validateEqual][formControl],[validateEqual][ngModel]',
            providers: [
                { provide: __WEBPACK_IMPORTED_MODULE_1__angular_forms__["f" /* NG_VALIDATORS */], useExisting: __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_12" /* forwardRef */])(function () { return EqualValidatorDirective_1; }), multi: true },
            ],
        })
    ], EqualValidatorDirective);
    return EqualValidatorDirective;
    var EqualValidatorDirective_1;
}());

//# sourceMappingURL=equal-validator.directive.js.map

/***/ }),

/***/ "../../../../../src/app/shared/shared.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__directives_assistant_validator_directive__ = __webpack_require__("../../../../../src/app/shared/directives/assistant-validator.directive.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__directives_email_validator_directive__ = __webpack_require__("../../../../../src/app/shared/directives/email-validator.directive.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__directives_equal_validator_directive__ = __webpack_require__("../../../../../src/app/shared/directives/equal-validator.directive.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SharedModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var SharedModule = (function () {
    function SharedModule() {
    }
    SharedModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [__WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */]],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_3__directives_email_validator_directive__["b" /* EmailValidatorDirective */],
                __WEBPACK_IMPORTED_MODULE_2__directives_assistant_validator_directive__["a" /* AssistantsValidatorDirective */],
                __WEBPACK_IMPORTED_MODULE_4__directives_equal_validator_directive__["a" /* EqualValidatorDirective */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_3__directives_email_validator_directive__["b" /* EmailValidatorDirective */],
                __WEBPACK_IMPORTED_MODULE_2__directives_assistant_validator_directive__["a" /* AssistantsValidatorDirective */],
                __WEBPACK_IMPORTED_MODULE_4__directives_equal_validator_directive__["a" /* EqualValidatorDirective */],
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
            ],
        })
    ], SharedModule);
    return SharedModule;
}());

//# sourceMappingURL=shared.module.js.map

/***/ }),

/***/ "../../../../../src/app/shared/viewModels/interfaces.ts":
/***/ (function(module, exports) {

//# sourceMappingURL=interfaces.js.map

/***/ }),

/***/ "../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<span md-dialog-title class=\"text-upper\">Add a comment</span>\r\n<md-dialog-content>\r\n  <span style=\"font-size: 0.9em\">Here you can add a short comment to our cookers about how to prepare your dish</span>\r\n  <md-input-container color=\"accent\" style=\"width:100%\">\r\n    <textarea mdInput placeholder=\"Comment\" #comment style=\"resize: none\"></textarea>\r\n  </md-input-container>\r\n</md-dialog-content>\r\n<md-dialog-actions>\r\n  <div class=\"align-right\" flex>\r\n    <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n    <button md-button [disabled]=\"!comment.value.length\" (click)=\"sendComment(comment.value)\" color=\"accent\" class=\"text-upper\">Send</button>\r\n  </div>\r\n</md-dialog-actions>"

/***/ }),

/***/ "../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CommentDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var CommentDialogComponent = (function () {
    function CommentDialogComponent(dialog) {
        this.dialog = dialog;
    }
    CommentDialogComponent.prototype.sendComment = function (comment) {
        this.dialog.close(comment);
    };
    CommentDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_11" /* Component */])({
            selector: 'public-comment-dialog',
            template: __webpack_require__("../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_material__["y" /* MdDialogRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_material__["y" /* MdDialogRef */]) === "function" && _a || Object])
    ], CommentDialogComponent);
    return CommentDialogComponent;
    var _a;
}());

//# sourceMappingURL=comment-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/sidenav/shared/price-calculator.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PriceCalculatorService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var PriceCalculatorService = (function () {
    function PriceCalculatorService() {
    }
    PriceCalculatorService.prototype.getPrice = function (order) {
        var extrasPrice = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1_lodash__["chain"])(order.extras)
            .reduce(function (total, extra) { return total + extra.price; }, 0)
            .value();
        return (order.dish.price + extrasPrice) * order.orderLine.amount;
    };
    PriceCalculatorService.prototype.getTotalPrice = function (orders) {
        var _this = this;
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1_lodash__["reduce"])(orders, function (sum, order) { return sum + _this.getPrice(order); }, 0);
    };
    PriceCalculatorService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])()
    ], PriceCalculatorService);
    return PriceCalculatorService;
}());

//# sourceMappingURL=price-calculator.service.js.map

/***/ }),

/***/ "../../../../../src/app/sidenav/shared/sidenav.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__backend_order_order_data_service__ = __webpack_require__("../../../../../src/app/backend/order/order-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SidenavService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var isOrderEqual = function (orderToFind) { return function (o) { return o.dish.name === orderToFind.dish.name && __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["isEqual"])(o.extras, orderToFind.extras); }; };
var SidenavService = (function () {
    function SidenavService(orderDataService) {
        this.orderDataService = orderDataService;
        this.orders = [];
        this.opened = false;
    }
    SidenavService.prototype.openSideNav = function () {
        this.opened = true;
    };
    SidenavService.prototype.closeSideNav = function () {
        this.opened = false;
    };
    SidenavService.prototype.getOrderData = function () {
        return this.orders;
    };
    SidenavService.prototype.getNumberOrders = function () {
        return this.orders.length;
    };
    SidenavService.prototype.findOrder = function (order) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["find"])(this.orders, isOrderEqual(order));
    };
    SidenavService.prototype.addOrder = function (order) {
        var addOrder = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["cloneDeep"])(order);
        addOrder.extras = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["filter"])(addOrder.extras, function (extra) { return extra.selected; });
        if (this.findOrder(addOrder)) {
            this.increaseOrder(addOrder);
        }
        else {
            this.orders.push(addOrder);
        }
    };
    SidenavService.prototype.increaseOrder = function (order) {
        return this.findOrder(order).orderLine.amount += 1;
    };
    SidenavService.prototype.decreaseOrder = function (order) {
        return this.findOrder(order).orderLine.amount -= 1;
    };
    SidenavService.prototype.removeOrder = function (order) {
        return __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_2_lodash__["remove"])(this.orders, isOrderEqual(order));
    };
    SidenavService.prototype.removeAllOrders = function () {
        this.orders = [];
        return this.orders;
    };
    SidenavService.prototype.sendOrders = function (token) {
        var orderList = {
            booking: { bookingToken: token },
            orderLines: this.composeOrders(this.orders),
        };
        this.closeSideNav();
        return this.orderDataService.saveOrders(orderList);
    };
    SidenavService.prototype.composeOrders = function (orders) {
        var composedOrders = [];
        orders.forEach(function (order) {
            var extras = [];
            order.extras.filter(function (extra) { return extra.selected; })
                .forEach(function (extra) { return extras.push({ id: extra.id }); });
            composedOrders.push({
                orderLine: {
                    dishId: order.dish.id,
                    amount: order.orderLine.amount,
                    comment: order.orderLine.comment,
                },
                extras: extras,
            });
        });
        return composedOrders;
    };
    SidenavService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__backend_order_order_data_service__["a" /* OrderDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__backend_order_order_data_service__["a" /* OrderDataService */]) === "function" && _a || Object])
    ], SidenavService);
    return SidenavService;
    var _a;
}());

//# sourceMappingURL=sidenav.service.js.map

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav-order/sidenav-order.component.html":
/***/ (function(module, exports) {

module.exports = "<div layout=\"row\" class=\"pad-top-sm pad-right-sm\" style=\"border-bottom: 1px solid lightgrey\" flex>\r\n  \r\n  <button md-icon-button (click)=\"removeOrder()\" class=\"push-bottom-xs\"><md-icon style=\"color: lightgray\">clear</md-icon></button>\r\n  \r\n  <div layout=\"column\" class=\"pull-top-xs\" flex>\r\n    <span class=\"orderName\">{{order.dish.name}}</span>\r\n    <div layout=\"row\">\r\n      <span class=\"orderIngredients\" *ngIf=\"extras.length === 0\"> - </span>\r\n      <span class=\"orderIngredients\">{{extras}}</span>\r\n    </div>\r\n    <label (click)=\"removeComment()\" class=\"removeOrderComment\" *ngIf=\"order.orderLine.comment\">Remove comment</label>\r\n    <label (click)=\"addComment()\" class=\"addOrderComment\" *ngIf=\"!order.orderLine.comment\">Add comment</label>\r\n  </div>\r\n\r\n  <button md-icon-button mdTooltip=\"Comment\" (click)=\"openCommentDialog()\" [class.hideButton]=\"!order.orderLine.comment\" class=\"push-bottom-xs\" mdTooltipPosition=\"above\">\r\n    <md-icon color=\"accent\">speaker_notes</md-icon>\r\n  </button>\r\n\r\n  <div layout=\"row\" class=\"push-bottom-xs\" flex>\r\n    <button md-icon-button [disabled]=\"order.orderLine.amount == 1\" color=\"accent\" (click)=\"decreaseOrder()\"><md-icon>remove</md-icon></button>  \r\n    <span class=\"push-top-sm\">{{order.orderLine.amount}}</span>               \r\n    <button md-icon-button (click)=\"increaseOrder()\" color=\"accent\"><md-icon>add</md-icon></button>                  \r\n  </div>\r\n\r\n  <span class=\"push-top-sm\">{{calculateOrderPrice() | number : '2.2-2'}} €</span>\r\n</div>"

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav-order/sidenav-order.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".orderName {\n  font-weight: bold; }\n\n.removeOrderComment {\n  font-size: 0.75em;\n  cursor: pointer;\n  color: orangered;\n  width: 90%; }\n\n.addOrderComment {\n  font-size: 0.75em;\n  cursor: pointer;\n  color: seagreen;\n  width: 70%; }\n\n.orderIngredients {\n  font-size: 0.75em;\n  color: gray; }\n\n.hideButton {\n  visibility: hidden; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav-order/sidenav-order.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__shared_sidenav_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/sidenav.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__shared_price_calculator_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/price-calculator.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__comment_dialog_comment_dialog_component__ = __webpack_require__("../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__covalent_core__ = __webpack_require__("../../../../@covalent/core/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__shared_viewModels_interfaces__ = __webpack_require__("../../../../../src/app/shared/viewModels/interfaces.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__shared_viewModels_interfaces___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_6__shared_viewModels_interfaces__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_lodash__ = __webpack_require__("../../../../lodash/lodash.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7_lodash___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_7_lodash__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SidenavOrderComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};








var SidenavOrderComponent = (function () {
    function SidenavOrderComponent(sidenav, dialog, _dialogService, calculator) {
        this.sidenav = sidenav;
        this.dialog = dialog;
        this._dialogService = _dialogService;
        this.calculator = calculator;
    }
    SidenavOrderComponent.prototype.ngOnInit = function () {
        this.extras = __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_7_lodash__["map"])(this.order.extras, 'name').join(', ');
    };
    SidenavOrderComponent.prototype.removeComment = function () {
        this.order.orderLine.comment = undefined;
    };
    SidenavOrderComponent.prototype.addComment = function () {
        var _this = this;
        var dialogRef = this.dialog.open(__WEBPACK_IMPORTED_MODULE_3__comment_dialog_comment_dialog_component__["a" /* CommentDialogComponent */]);
        dialogRef.afterClosed().subscribe(function (result) {
            _this.order.orderLine.comment = result;
        });
    };
    SidenavOrderComponent.prototype.increaseOrder = function () {
        this.sidenav.increaseOrder(this.order);
    };
    SidenavOrderComponent.prototype.decreaseOrder = function () {
        this.sidenav.decreaseOrder(this.order);
    };
    SidenavOrderComponent.prototype.removeOrder = function () {
        this.sidenav.removeOrder(this.order);
    };
    SidenavOrderComponent.prototype.calculateOrderPrice = function () {
        return this.calculator.getPrice(this.order);
    };
    SidenavOrderComponent.prototype.openCommentDialog = function () {
        this._dialogService.openAlert({
            message: this.order.orderLine.comment,
            title: 'Comment',
            closeButton: 'Close',
        });
    };
    __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4__angular_core__["m" /* Input */])('order'),
        __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_6__shared_viewModels_interfaces__["OrderView"] !== "undefined" && __WEBPACK_IMPORTED_MODULE_6__shared_viewModels_interfaces__["OrderView"]) === "function" && _a || Object)
    ], SidenavOrderComponent.prototype, "order", void 0);
    SidenavOrderComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_4__angular_core__["_11" /* Component */])({
            selector: 'public-sidenav-order',
            template: __webpack_require__("../../../../../src/app/sidenav/sidenav-order/sidenav-order.component.html"),
            styles: [__webpack_require__("../../../../../src/app/sidenav/sidenav-order/sidenav-order.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__shared_sidenav_service__["a" /* SidenavService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__shared_sidenav_service__["a" /* SidenavService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_2__angular_material__["a" /* MdDialog */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_material__["a" /* MdDialog */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_5__covalent_core__["l" /* TdDialogService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_5__covalent_core__["l" /* TdDialogService */]) === "function" && _d || Object, typeof (_e = typeof __WEBPACK_IMPORTED_MODULE_1__shared_price_calculator_service__["a" /* PriceCalculatorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__shared_price_calculator_service__["a" /* PriceCalculatorService */]) === "function" && _e || Object])
    ], SidenavOrderComponent);
    return SidenavOrderComponent;
    var _a, _b, _c, _d, _e;
}());

//# sourceMappingURL=sidenav-order.component.js.map

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav.component.html":
/***/ (function(module, exports) {

module.exports = "<div layout=\"column\" class=\"justify-space-between\" style =\"height: 100%\">\r\n  <div layout=\"column\" flex>\r\n    <md-toolbar>\r\n      <div layout=\"row\" style=\"align-items:center\" flex>\r\n        <button class=\"forMobile\" md-icon-button (click)=\"closeSidenav()\"><md-icon>clear</md-icon></button>\r\n        <md-icon class=\"forDesktop push-right-sm\">shopping_basket</md-icon>\r\n        <span style=\"font-weight: bold; font-size: 0.7em\">RESUME BOOKING</span>\r\n      </div>\r\n    </md-toolbar>\r\n    <div layout=\"column\" class=\"pull-top pad-sm\">\r\n      <h4 class=\"pad-bottom-sm\" style=\"border-bottom: 1px solid lightgrey\">ORDER MENU</h4>\r\n      <div layout=\"row\" *ngIf=\"orders.length < 1\" style=\"align-items:center\" class=\"justify-space-between push-bottom-lg\">\r\n        <span>You have not selected any order</span>\r\n        <button md-button color=\"accent\" (click)=\"navigateTo('menu')\">View Menu</button>\r\n      </div>\r\n      <div class=\"pull-top\" [class.scrollOrders]=\"orders.length > 3\">\r\n        <public-sidenav-order *ngFor=\"let order of orders\" [order]=\"order\"></public-sidenav-order>\r\n      </div>\r\n    </div>\r\n    <div *ngIf=\"orders.length > 0\" layout=\"row\" class=\"justify-space-between pad-sm\">\r\n      <h3>Total</h3>\r\n      <h3>{{calculateTotal() | number : '1.2-2'}} €</h3>\r\n    </div>\r\n    <div layout=\"column\" class=\"pad-sm\">\r\n      <md-card *ngIf=\"!bookingId.value\" class=\"pad-sm\" style=\"background-color:bisque\">\r\n        <div layout=\"row\" class=\"justify-space-around\" style=\"align-items:center\" flex>\r\n          <md-icon class=\"md-18 pad-right-sm\">report_problem</md-icon>\r\n          <span flex=\"65\" class=\"invitationAlert\">To order a menu it is necessary to obtain a booking id. Neither you enter your already known booking id or you book a table </span>\r\n          <button md-button color=\"accent\" (click)=\"navigateTo('bookTable')\">Book a table</button>\r\n        </div>\r\n      </md-card>\r\n      <md-input-container class=\"push-left\" color=\"accent\" flex>\r\n        <input mdInput #bookingId placeholder=\"Booking ID\">\r\n      </md-input-container>\r\n      <md-checkbox #terms class=\"push-left\">Accept terms</md-checkbox>\r\n    </div>\r\n  </div>\r\n  <div style=\"text-align: right\">\r\n    <button md-button (click)=\"closeSidenav()\" class=\"text-upper property-text-bold\">Cancel</button>\r\n    <button md-button color=\"accent\" (click)=\"sendOrders(bookingId.value)\" [disabled]=\"!(bookingId.value.length > 0 && terms.checked)\" class=\"text-upper property-text-bold\">Send</button>\r\n  </div>\r\n</div>"

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".sidenav-info-text {\n  color: lightslategrey; }\n\n.sidenav-info {\n  font-size: 0.8em;\n  -webkit-box-pack: justify;\n      -ms-flex-pack: justify;\n          justify-content: space-between; }\n\n.scrollOrders {\n  overflow-y: scroll; }\n\n.invitationAlert {\n  font-size: 0.80em; }\n\n@media (max-width: 600px) {\n  .forDesktop {\n    display: none; } }\n\n@media (min-width: 600px) {\n  .forMobile {\n    display: none; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_sidenav_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/sidenav.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_price_calculator_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/price-calculator.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SidenavComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var SidenavComponent = (function () {
    function SidenavComponent(router, sidenav, snackBar, calculator) {
        this.router = router;
        this.sidenav = sidenav;
        this.snackBar = snackBar;
        this.calculator = calculator;
    }
    SidenavComponent.prototype.ngOnInit = function () {
        this.orders = this.sidenav.getOrderData();
    };
    SidenavComponent.prototype.closeSidenav = function () {
        this.sidenav.closeSideNav();
    };
    SidenavComponent.prototype.navigateTo = function (route) {
        this.router.navigate([route]);
        this.closeSidenav();
    };
    SidenavComponent.prototype.calculateTotal = function () {
        return this.calculator.getTotalPrice(this.orders);
    };
    SidenavComponent.prototype.sendOrders = function (bookingId) {
        var _this = this;
        this.sidenav.sendOrders(bookingId)
            .subscribe(function () {
            _this.orders = _this.sidenav.removeAllOrders();
            _this.snackBar.openSnack('Order correctly noted', 4000, 'green');
        }, function (error) {
            _this.snackBar.openSnack('Error sending order, please, try again later', 4000, 'red');
        });
    };
    SidenavComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_11" /* Component */])({
            selector: 'public-sidenav',
            template: __webpack_require__("../../../../../src/app/sidenav/sidenav.component.html"),
            styles: [__webpack_require__("../../../../../src/app/sidenav/sidenav.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__shared_sidenav_service__["a" /* SidenavService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__shared_sidenav_service__["a" /* SidenavService */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_4__core_snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_3__shared_price_calculator_service__["a" /* PriceCalculatorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__shared_price_calculator_service__["a" /* PriceCalculatorService */]) === "function" && _d || Object])
    ], SidenavComponent);
    return SidenavComponent;
    var _a, _b, _c, _d;
}());

//# sourceMappingURL=sidenav.component.js.map

/***/ }),

/***/ "../../../../../src/app/sidenav/sidenav.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_sidenav_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/sidenav.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared_price_calculator_service__ = __webpack_require__("../../../../../src/app/sidenav/shared/price-calculator.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__core_windowService_windowService_service__ = __webpack_require__("../../../../../src/app/core/windowService/windowService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__sidenav_component__ = __webpack_require__("../../../../../src/app/sidenav/sidenav.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__sidenav_order_sidenav_order_component__ = __webpack_require__("../../../../../src/app/sidenav/sidenav-order/sidenav-order.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__comment_dialog_comment_dialog_component__ = __webpack_require__("../../../../../src/app/sidenav/comment-dialog/comment-dialog.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SidenavModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var SidenavModule = (function () {
    function SidenavModule() {
    }
    SidenavModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_2__core_core_module__["a" /* CoreModule */],
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_3__shared_sidenav_service__["a" /* SidenavService */],
                __WEBPACK_IMPORTED_MODULE_4__shared_price_calculator_service__["a" /* PriceCalculatorService */],
                __WEBPACK_IMPORTED_MODULE_5__core_windowService_windowService_service__["a" /* WindowService */],
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_6__sidenav_component__["a" /* SidenavComponent */],
                __WEBPACK_IMPORTED_MODULE_7__sidenav_order_sidenav_order_component__["a" /* SidenavOrderComponent */],
                __WEBPACK_IMPORTED_MODULE_8__comment_dialog_comment_dialog_component__["a" /* CommentDialogComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_6__sidenav_component__["a" /* SidenavComponent */],
            ],
            entryComponents: [
                __WEBPACK_IMPORTED_MODULE_8__comment_dialog_comment_dialog_component__["a" /* CommentDialogComponent */],
            ],
        })
    ], SidenavModule);
    return SidenavModule;
}());

//# sourceMappingURL=sidenav.module.js.map

/***/ }),

/***/ "../../../../../src/app/user-area/login-dialog/login-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<md-tab-group md-stretch-tabs class=\"pull\">\r\n\r\n  <md-tab label=\"LOGIN\">\r\n    <form (ngSubmit)=\"logInSubmit(loginForm.form.value)\" class=\"pad\" #loginForm=\"ngForm\">\r\n      <div layout= \"column\" flex>\r\n        <md-input-container color=\"accent\" flex>\r\n          <input mdInput placeholder=\"Username\" ngModel name=\"username\" required>\r\n        </md-input-container>\r\n\r\n        <md-input-container color=\"accent\" flex>\r\n          <input mdInput placeholder=\"Password\" ngModel name=\"password\" type=\"password\" required>\r\n        </md-input-container>\r\n      </div>\r\n      <div style=\"text-align: right\">\r\n        <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n        <button md-button type=\"submit\" [disabled]=\"!loginForm.form.valid\" color=\"accent\" class=\"text-upper\">Login</button>\r\n      </div>\r\n    </form>\r\n  </md-tab>\r\n\r\n  <md-tab label=\"SIGN UP\">\r\n    <form (ngSubmit)=\"signInSubmit(signForm.form.value)\" class=\"pad\" #signForm=\"ngForm\">\r\n      <div layout= \"column\" class=\"push-bottom-sm\" flex>\r\n\r\n        <md-input-container color=\"accent\" flex>\r\n          <input mdInput placeholder=\"Email\" ngModel name=\"email\" required validateEmail>\r\n          <md-error>Please, enter a valid email address</md-error>\r\n        </md-input-container>\r\n\r\n        <md-input-container color=\"accent\" flex>\r\n          <input mdInput type=\"password\" name=\"password\" placeholder=\"Password\" ngModel required validateEqual=\"confirmPassword\" reverse=\"true\" #password=\"ngModel\">\r\n        </md-input-container>\r\n\r\n        <md-input-container color=\"accent\" flex>\r\n          <input mdInput type=\"password\" name=\"confirmPassword\" placeholder=\"Confirm Password\" ngModel required validateEqual=\"password\" reverse=\"false\" #confirmPassword=\"ngModel\">\r\n          <md-error>Password mismatch</md-error>\r\n        </md-input-container>\r\n      </div>\r\n\r\n      <md-checkbox #termsd>Accept terms</md-checkbox>\r\n\r\n      <div class=\"align-right\" flex>\r\n        <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n        <button md-button type=\"submit\" [disabled]=\"!(signForm.form.valid && termsd.checked)\" color=\"accent\" class=\"text-upper\">Register</button>\r\n      </div>\r\n    </form>\r\n  </md-tab>\r\n</md-tab-group>\r\n"

/***/ }),

/***/ "../../../../../src/app/user-area/login-dialog/login-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "@media (max-width: 600px) {\n  .forgotPassword {\n    cursor: pointer;\n    color: seagreen;\n    width: 60%; } }\n\n@media (min-width: 600px) {\n  .forgotPassword {\n    cursor: pointer;\n    color: seagreen;\n    width: 25%; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/user-area/login-dialog/login-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var LoginDialogComponent = (function () {
    function LoginDialogComponent(dialog) {
        this.dialog = dialog;
    }
    LoginDialogComponent.prototype.logInSubmit = function (formValue) {
        this.dialog.close(formValue);
    };
    LoginDialogComponent.prototype.signInSubmit = function (formValue) {
        this.dialog.close(formValue);
    };
    LoginDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-login-dialog',
            template: __webpack_require__("../../../../../src/app/user-area/login-dialog/login-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/user-area/login-dialog/login-dialog.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */]) === "function" && _a || Object])
    ], LoginDialogComponent);
    return LoginDialogComponent;
    var _a;
}());

//# sourceMappingURL=login-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/user-area/password-dialog/password-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<h3 md-dialog-title>SET NEW PASSWORD</h3>\r\n<form (ngSubmit)=\"passwordSubmit(passwordForm.form.value)\" #passwordForm=\"ngForm\">\r\n  <div layout= \"column\" class=\"push-bottom-sm\" flex>\r\n\r\n    <md-input-container color=\"accent\" flex>\r\n      <input mdInput type=\"password\" name=\"oldPassword\" placeholder=\"Current password\" ngModel required validateEqual=\"confirmPassword\" reverse=\"true\" #password=\"ngModel\">\r\n    </md-input-container>\r\n\r\n    <md-input-container color=\"accent\" flex>\r\n      <input mdInput type=\"password\" name=\"newPassword\" placeholder=\"New password\" ngModel required validateEqual=\"verifyPassword\" reverse=\"true\" #newPassword=\"ngModel\">\r\n    </md-input-container>\r\n\r\n    <md-input-container color=\"accent\" flex>\r\n      <input mdInput type=\"password\" name=\"verifyPassword\" placeholder=\"Verify Password\" ngModel required validateEqual=\"newPassword\" reverse=\"false\" #verifyPassword=\"ngModel\">\r\n      <md-error>Password mismatch</md-error>\r\n    </md-input-container>\r\n  </div>\r\n\r\n  <div class=\"align-right\" flex>\r\n    <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n    <button md-button type=\"submit\" [disabled]=\"!passwordForm.form.valid\" color=\"accent\" class=\"text-upper\">Save</button>\r\n  </div>\r\n</form>"

/***/ }),

/***/ "../../../../../src/app/user-area/password-dialog/password-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/user-area/password-dialog/password-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__shared_user_area_service__ = __webpack_require__("../../../../../src/app/user-area/shared/user-area.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PasswordDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var PasswordDialogComponent = (function () {
    function PasswordDialogComponent(dialog, userService) {
        this.dialog = dialog;
        this.userService = userService;
    }
    PasswordDialogComponent.prototype.passwordSubmit = function (form) {
        this.dialog.close();
        this.userService.changePassword(form);
    };
    PasswordDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["_11" /* Component */])({
            selector: 'public-password-dialog',
            template: __webpack_require__("../../../../../src/app/user-area/password-dialog/password-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/user-area/password-dialog/password-dialog.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__angular_material__["y" /* MdDialogRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_material__["y" /* MdDialogRef */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__shared_user_area_service__["a" /* UserAreaService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__shared_user_area_service__["a" /* UserAreaService */]) === "function" && _b || Object])
    ], PasswordDialogComponent);
    return PasswordDialogComponent;
    var _a, _b;
}());

//# sourceMappingURL=password-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/user-area/shared/user-area.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_router__ = __webpack_require__("../../../router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__backend_login_login_data_service__ = __webpack_require__("../../../../../src/app/backend/login/login-data-service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__ = __webpack_require__("../../../../../src/app/core/snackService/snackService.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserAreaService; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var UserAreaService = (function () {
    function UserAreaService(snackBar, router, authService, loginDataService) {
        this.snackBar = snackBar;
        this.router = router;
        this.authService = authService;
        this.loginDataService = loginDataService;
    }
    UserAreaService.prototype.login = function (username, password) {
        var _this = this;
        this.loginDataService.login(username, password)
            .subscribe(function (res) {
            _this.authService.setToken(res.headers.get('Authorization'));
            _this.loginDataService.getCurrentUser()
                .subscribe(function (loginInfo) {
                _this.authService.setLogged(true);
                _this.authService.setUser(loginInfo.name);
                _this.authService.setRole(loginInfo.role);
                _this.router.navigate(['orders']);
                _this.snackBar.openSnack('Login successful', 4000, 'green');
            });
        }, function (err) {
            _this.authService.setLogged(false);
            _this.snackBar.openSnack(err.json().message, 4000, 'red');
        });
    };
    UserAreaService.prototype.register = function (email, password) {
        var _this = this;
        this.loginDataService.register(email, password)
            .subscribe(function () {
            _this.snackBar.openSnack('Register successful', 4000, 'green');
        }, function (error) {
            _this.snackBar.openSnack('Register failed, username already in use', 4000, 'red');
        });
    };
    UserAreaService.prototype.logout = function () {
        this.authService.setLogged(false);
        this.authService.setUser('');
        this.authService.setRole('CUSTOMER');
        this.authService.setToken('');
        this.router.navigate(['restarant']);
        this.snackBar.openSnack('Log out successful, come back soon!', 4000, 'black');
    };
    UserAreaService.prototype.changePassword = function (data) {
        var _this = this;
        data.username = this.authService.getUser();
        this.loginDataService.changePassword(data.username, data.oldPassword, data.newPassword)
            .subscribe(function (res) {
            _this.snackBar.openSnack(res.message, 4000, 'green');
        }, function (error) {
            _this.snackBar.openSnack(error.message, 4000, 'red');
        });
    };
    UserAreaService = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["v" /* Injectable */])(),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__["a" /* SnackBarService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__core_snackService_snackService_service__["a" /* SnackBarService */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_router__["b" /* Router */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_4__core_authentication_auth_service__["a" /* AuthService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_4__core_authentication_auth_service__["a" /* AuthService */]) === "function" && _c || Object, typeof (_d = typeof __WEBPACK_IMPORTED_MODULE_2__backend_login_login_data_service__["a" /* LoginDataService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__backend_login_login_data_service__["a" /* LoginDataService */]) === "function" && _d || Object])
    ], UserAreaService);
    return UserAreaService;
    var _a, _b, _c, _d;
}());

//# sourceMappingURL=user-area.service.js.map

/***/ }),

/***/ "../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.html":
/***/ (function(module, exports) {

module.exports = "<h4 md-dialog-title>ASSOCIATE TWITTER ACCOUNT</h4>\r\n<form  (ngSubmit)=\"twitterSubmit(twitterForm.form.value)\" #twitterForm=\"ngForm\">\r\n  <div layout= \"column\" class=\"push-bottom-sm\" flex>\r\n    <md-input-container color=\"accent\" flex>\r\n      <input mdInput placeholder=\"Twitter username\" ngModel name=\"username\" required>\r\n    </md-input-container>\r\n\r\n    <md-input-container color=\"accent\" flex>\r\n      <input mdInput placeholder=\"Twitter password\" ngModel name=\"password\" type=\"password\" required>\r\n    </md-input-container>\r\n  </div>\r\n\r\n  <div class=\"align-right\" flex>  \r\n    <button md-button md-dialog-close class=\"text-upper\">Cancel</button>\r\n    <button md-raised-button type=\"submit\" [disabled]=\"!twitterForm.form.valid\" color=\"accent\" class=\"text-upper\">\r\n      <md-icon svgIcon=\"twitter\"></md-icon>\r\n      <span>Log in twitter</span>\r\n    </button>\r\n  </div>\r\n</form>"

/***/ }),

/***/ "../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_platform_browser__ = __webpack_require__("../../../platform-browser/@angular/platform-browser.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TwitterDialogComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var TwitterDialogComponent = (function () {
    function TwitterDialogComponent(dialog, iconReg, sanitizer) {
        this.dialog = dialog;
        this.iconReg = iconReg;
        this.sanitizer = sanitizer;
        iconReg.addSvgIcon('twitter', sanitizer.bypassSecurityTrustResourceUrl('assets/images/Twitter_Logo.svg'));
    }
    TwitterDialogComponent.prototype.twitterSubmit = function (form) {
        // TODO
    };
    TwitterDialogComponent = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_11" /* Component */])({
            selector: 'public-twitter-dialog',
            template: __webpack_require__("../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.html"),
            styles: [__webpack_require__("../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.scss")],
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["y" /* MdDialogRef */]) === "function" && _a || Object, typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["V" /* MdIconRegistry */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["V" /* MdIconRegistry */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_2__angular_platform_browser__["f" /* DomSanitizer */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__angular_platform_browser__["f" /* DomSanitizer */]) === "function" && _c || Object])
    ], TwitterDialogComponent);
    return TwitterDialogComponent;
    var _a, _b, _c;
}());

//# sourceMappingURL=twitter-dialog.component.js.map

/***/ }),

/***/ "../../../../../src/app/user-area/user-area.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_material__ = __webpack_require__("../../../material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_shared_module__ = __webpack_require__("../../../../../src/app/shared/shared.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__core_core_module__ = __webpack_require__("../../../../../src/app/core/core.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__core_authentication_auth_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__core_authentication_auth_guard_service__ = __webpack_require__("../../../../../src/app/core/authentication/auth-guard.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__shared_user_area_service__ = __webpack_require__("../../../../../src/app/user-area/shared/user-area.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__login_dialog_login_dialog_component__ = __webpack_require__("../../../../../src/app/user-area/login-dialog/login-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__password_dialog_password_dialog_component__ = __webpack_require__("../../../../../src/app/user-area/password-dialog/password-dialog.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__twitter_dialog_twitter_dialog_component__ = __webpack_require__("../../../../../src/app/user-area/twitter-dialog/twitter-dialog.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return UserAreaModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











var UserAreaModule = (function () {
    function UserAreaModule() {
    }
    UserAreaModule = __decorate([
        __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
            imports: [
                __WEBPACK_IMPORTED_MODULE_1__angular_common__["k" /* CommonModule */],
                __WEBPACK_IMPORTED_MODULE_2__angular_material__["U" /* MaterialModule */],
                __WEBPACK_IMPORTED_MODULE_3__shared_shared_module__["a" /* SharedModule */],
                __WEBPACK_IMPORTED_MODULE_4__core_core_module__["a" /* CoreModule */],
            ],
            providers: [
                __WEBPACK_IMPORTED_MODULE_7__shared_user_area_service__["a" /* UserAreaService */],
                __WEBPACK_IMPORTED_MODULE_6__core_authentication_auth_guard_service__["a" /* AuthGuardService */],
                __WEBPACK_IMPORTED_MODULE_5__core_authentication_auth_service__["a" /* AuthService */],
            ],
            declarations: [
                __WEBPACK_IMPORTED_MODULE_8__login_dialog_login_dialog_component__["a" /* LoginDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_9__password_dialog_password_dialog_component__["a" /* PasswordDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_10__twitter_dialog_twitter_dialog_component__["a" /* TwitterDialogComponent */],
            ],
            exports: [
                __WEBPACK_IMPORTED_MODULE_8__login_dialog_login_dialog_component__["a" /* LoginDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_9__password_dialog_password_dialog_component__["a" /* PasswordDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_10__twitter_dialog_twitter_dialog_component__["a" /* TwitterDialogComponent */],
            ],
            entryComponents: [
                __WEBPACK_IMPORTED_MODULE_8__login_dialog_login_dialog_component__["a" /* LoginDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_9__password_dialog_password_dialog_component__["a" /* PasswordDialogComponent */],
                __WEBPACK_IMPORTED_MODULE_10__twitter_dialog_twitter_dialog_component__["a" /* TwitterDialogComponent */],
            ],
        })
    ], UserAreaModule);
    return UserAreaModule;
}());

//# sourceMappingURL=user-area.module.js.map

/***/ }),

/***/ "../../../../../src/assets/images/Wood2.jpg":
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "Wood2.7b3782e7fd543cc9cdc4.jpg";

/***/ }),

/***/ "../../../../../src/assets/images/background-dish.png":
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "background-dish.fe9678cd8802fa639336.png";

/***/ }),

/***/ "../../../../../src/assets/images/slider-1.jpg":
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "slider-1.a66518ca4fb732b07a90.jpg";

/***/ }),

/***/ "../../../../../src/assets/images/slider-2.jpg":
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "slider-2.824fc6c25bbd8ce09d01.jpg";

/***/ }),

/***/ "../../../../../src/assets/images/star.png":
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__.p + "star.5cbbb1d4fad2e640a5bb.png";

/***/ }),

/***/ "../../../../../src/environments/environment.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__app_config__ = __webpack_require__("../../../../../src/app/config.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

var environment = {
    production: false,
    backendType: __WEBPACK_IMPORTED_MODULE_0__app_config__["b" /* BackendType */].REST,
    restPathRoot: 'http://localhost:8081/mythaistar/',
    restServiceRoot: 'http://localhost:8081/mythaistar/services/rest/',
};
//# sourceMappingURL=environment.js.map

/***/ }),

/***/ "../../../../../src/main.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser_dynamic__ = __webpack_require__("../../../platform-browser-dynamic/@angular/platform-browser-dynamic.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app___ = __webpack_require__("../../../../../src/app/index.ts");




if (__WEBPACK_IMPORTED_MODULE_2__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["a" /* enableProdMode */])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_3__app___["a" /* AppModule */]);
//# sourceMappingURL=main.js.map

/***/ }),

/***/ "../../../../moment/locale recursive ^\\.\\/.*$":
/***/ (function(module, exports, __webpack_require__) {

var map = {
	"./af": "../../../../moment/locale/af.js",
	"./af.js": "../../../../moment/locale/af.js",
	"./ar": "../../../../moment/locale/ar.js",
	"./ar-dz": "../../../../moment/locale/ar-dz.js",
	"./ar-dz.js": "../../../../moment/locale/ar-dz.js",
	"./ar-kw": "../../../../moment/locale/ar-kw.js",
	"./ar-kw.js": "../../../../moment/locale/ar-kw.js",
	"./ar-ly": "../../../../moment/locale/ar-ly.js",
	"./ar-ly.js": "../../../../moment/locale/ar-ly.js",
	"./ar-ma": "../../../../moment/locale/ar-ma.js",
	"./ar-ma.js": "../../../../moment/locale/ar-ma.js",
	"./ar-sa": "../../../../moment/locale/ar-sa.js",
	"./ar-sa.js": "../../../../moment/locale/ar-sa.js",
	"./ar-tn": "../../../../moment/locale/ar-tn.js",
	"./ar-tn.js": "../../../../moment/locale/ar-tn.js",
	"./ar.js": "../../../../moment/locale/ar.js",
	"./az": "../../../../moment/locale/az.js",
	"./az.js": "../../../../moment/locale/az.js",
	"./be": "../../../../moment/locale/be.js",
	"./be.js": "../../../../moment/locale/be.js",
	"./bg": "../../../../moment/locale/bg.js",
	"./bg.js": "../../../../moment/locale/bg.js",
	"./bn": "../../../../moment/locale/bn.js",
	"./bn.js": "../../../../moment/locale/bn.js",
	"./bo": "../../../../moment/locale/bo.js",
	"./bo.js": "../../../../moment/locale/bo.js",
	"./br": "../../../../moment/locale/br.js",
	"./br.js": "../../../../moment/locale/br.js",
	"./bs": "../../../../moment/locale/bs.js",
	"./bs.js": "../../../../moment/locale/bs.js",
	"./ca": "../../../../moment/locale/ca.js",
	"./ca.js": "../../../../moment/locale/ca.js",
	"./cs": "../../../../moment/locale/cs.js",
	"./cs.js": "../../../../moment/locale/cs.js",
	"./cv": "../../../../moment/locale/cv.js",
	"./cv.js": "../../../../moment/locale/cv.js",
	"./cy": "../../../../moment/locale/cy.js",
	"./cy.js": "../../../../moment/locale/cy.js",
	"./da": "../../../../moment/locale/da.js",
	"./da.js": "../../../../moment/locale/da.js",
	"./de": "../../../../moment/locale/de.js",
	"./de-at": "../../../../moment/locale/de-at.js",
	"./de-at.js": "../../../../moment/locale/de-at.js",
	"./de-ch": "../../../../moment/locale/de-ch.js",
	"./de-ch.js": "../../../../moment/locale/de-ch.js",
	"./de.js": "../../../../moment/locale/de.js",
	"./dv": "../../../../moment/locale/dv.js",
	"./dv.js": "../../../../moment/locale/dv.js",
	"./el": "../../../../moment/locale/el.js",
	"./el.js": "../../../../moment/locale/el.js",
	"./en-au": "../../../../moment/locale/en-au.js",
	"./en-au.js": "../../../../moment/locale/en-au.js",
	"./en-ca": "../../../../moment/locale/en-ca.js",
	"./en-ca.js": "../../../../moment/locale/en-ca.js",
	"./en-gb": "../../../../moment/locale/en-gb.js",
	"./en-gb.js": "../../../../moment/locale/en-gb.js",
	"./en-ie": "../../../../moment/locale/en-ie.js",
	"./en-ie.js": "../../../../moment/locale/en-ie.js",
	"./en-nz": "../../../../moment/locale/en-nz.js",
	"./en-nz.js": "../../../../moment/locale/en-nz.js",
	"./eo": "../../../../moment/locale/eo.js",
	"./eo.js": "../../../../moment/locale/eo.js",
	"./es": "../../../../moment/locale/es.js",
	"./es-do": "../../../../moment/locale/es-do.js",
	"./es-do.js": "../../../../moment/locale/es-do.js",
	"./es.js": "../../../../moment/locale/es.js",
	"./et": "../../../../moment/locale/et.js",
	"./et.js": "../../../../moment/locale/et.js",
	"./eu": "../../../../moment/locale/eu.js",
	"./eu.js": "../../../../moment/locale/eu.js",
	"./fa": "../../../../moment/locale/fa.js",
	"./fa.js": "../../../../moment/locale/fa.js",
	"./fi": "../../../../moment/locale/fi.js",
	"./fi.js": "../../../../moment/locale/fi.js",
	"./fo": "../../../../moment/locale/fo.js",
	"./fo.js": "../../../../moment/locale/fo.js",
	"./fr": "../../../../moment/locale/fr.js",
	"./fr-ca": "../../../../moment/locale/fr-ca.js",
	"./fr-ca.js": "../../../../moment/locale/fr-ca.js",
	"./fr-ch": "../../../../moment/locale/fr-ch.js",
	"./fr-ch.js": "../../../../moment/locale/fr-ch.js",
	"./fr.js": "../../../../moment/locale/fr.js",
	"./fy": "../../../../moment/locale/fy.js",
	"./fy.js": "../../../../moment/locale/fy.js",
	"./gd": "../../../../moment/locale/gd.js",
	"./gd.js": "../../../../moment/locale/gd.js",
	"./gl": "../../../../moment/locale/gl.js",
	"./gl.js": "../../../../moment/locale/gl.js",
	"./gom-latn": "../../../../moment/locale/gom-latn.js",
	"./gom-latn.js": "../../../../moment/locale/gom-latn.js",
	"./he": "../../../../moment/locale/he.js",
	"./he.js": "../../../../moment/locale/he.js",
	"./hi": "../../../../moment/locale/hi.js",
	"./hi.js": "../../../../moment/locale/hi.js",
	"./hr": "../../../../moment/locale/hr.js",
	"./hr.js": "../../../../moment/locale/hr.js",
	"./hu": "../../../../moment/locale/hu.js",
	"./hu.js": "../../../../moment/locale/hu.js",
	"./hy-am": "../../../../moment/locale/hy-am.js",
	"./hy-am.js": "../../../../moment/locale/hy-am.js",
	"./id": "../../../../moment/locale/id.js",
	"./id.js": "../../../../moment/locale/id.js",
	"./is": "../../../../moment/locale/is.js",
	"./is.js": "../../../../moment/locale/is.js",
	"./it": "../../../../moment/locale/it.js",
	"./it.js": "../../../../moment/locale/it.js",
	"./ja": "../../../../moment/locale/ja.js",
	"./ja.js": "../../../../moment/locale/ja.js",
	"./jv": "../../../../moment/locale/jv.js",
	"./jv.js": "../../../../moment/locale/jv.js",
	"./ka": "../../../../moment/locale/ka.js",
	"./ka.js": "../../../../moment/locale/ka.js",
	"./kk": "../../../../moment/locale/kk.js",
	"./kk.js": "../../../../moment/locale/kk.js",
	"./km": "../../../../moment/locale/km.js",
	"./km.js": "../../../../moment/locale/km.js",
	"./kn": "../../../../moment/locale/kn.js",
	"./kn.js": "../../../../moment/locale/kn.js",
	"./ko": "../../../../moment/locale/ko.js",
	"./ko.js": "../../../../moment/locale/ko.js",
	"./ky": "../../../../moment/locale/ky.js",
	"./ky.js": "../../../../moment/locale/ky.js",
	"./lb": "../../../../moment/locale/lb.js",
	"./lb.js": "../../../../moment/locale/lb.js",
	"./lo": "../../../../moment/locale/lo.js",
	"./lo.js": "../../../../moment/locale/lo.js",
	"./lt": "../../../../moment/locale/lt.js",
	"./lt.js": "../../../../moment/locale/lt.js",
	"./lv": "../../../../moment/locale/lv.js",
	"./lv.js": "../../../../moment/locale/lv.js",
	"./me": "../../../../moment/locale/me.js",
	"./me.js": "../../../../moment/locale/me.js",
	"./mi": "../../../../moment/locale/mi.js",
	"./mi.js": "../../../../moment/locale/mi.js",
	"./mk": "../../../../moment/locale/mk.js",
	"./mk.js": "../../../../moment/locale/mk.js",
	"./ml": "../../../../moment/locale/ml.js",
	"./ml.js": "../../../../moment/locale/ml.js",
	"./mr": "../../../../moment/locale/mr.js",
	"./mr.js": "../../../../moment/locale/mr.js",
	"./ms": "../../../../moment/locale/ms.js",
	"./ms-my": "../../../../moment/locale/ms-my.js",
	"./ms-my.js": "../../../../moment/locale/ms-my.js",
	"./ms.js": "../../../../moment/locale/ms.js",
	"./my": "../../../../moment/locale/my.js",
	"./my.js": "../../../../moment/locale/my.js",
	"./nb": "../../../../moment/locale/nb.js",
	"./nb.js": "../../../../moment/locale/nb.js",
	"./ne": "../../../../moment/locale/ne.js",
	"./ne.js": "../../../../moment/locale/ne.js",
	"./nl": "../../../../moment/locale/nl.js",
	"./nl-be": "../../../../moment/locale/nl-be.js",
	"./nl-be.js": "../../../../moment/locale/nl-be.js",
	"./nl.js": "../../../../moment/locale/nl.js",
	"./nn": "../../../../moment/locale/nn.js",
	"./nn.js": "../../../../moment/locale/nn.js",
	"./pa-in": "../../../../moment/locale/pa-in.js",
	"./pa-in.js": "../../../../moment/locale/pa-in.js",
	"./pl": "../../../../moment/locale/pl.js",
	"./pl.js": "../../../../moment/locale/pl.js",
	"./pt": "../../../../moment/locale/pt.js",
	"./pt-br": "../../../../moment/locale/pt-br.js",
	"./pt-br.js": "../../../../moment/locale/pt-br.js",
	"./pt.js": "../../../../moment/locale/pt.js",
	"./ro": "../../../../moment/locale/ro.js",
	"./ro.js": "../../../../moment/locale/ro.js",
	"./ru": "../../../../moment/locale/ru.js",
	"./ru.js": "../../../../moment/locale/ru.js",
	"./sd": "../../../../moment/locale/sd.js",
	"./sd.js": "../../../../moment/locale/sd.js",
	"./se": "../../../../moment/locale/se.js",
	"./se.js": "../../../../moment/locale/se.js",
	"./si": "../../../../moment/locale/si.js",
	"./si.js": "../../../../moment/locale/si.js",
	"./sk": "../../../../moment/locale/sk.js",
	"./sk.js": "../../../../moment/locale/sk.js",
	"./sl": "../../../../moment/locale/sl.js",
	"./sl.js": "../../../../moment/locale/sl.js",
	"./sq": "../../../../moment/locale/sq.js",
	"./sq.js": "../../../../moment/locale/sq.js",
	"./sr": "../../../../moment/locale/sr.js",
	"./sr-cyrl": "../../../../moment/locale/sr-cyrl.js",
	"./sr-cyrl.js": "../../../../moment/locale/sr-cyrl.js",
	"./sr.js": "../../../../moment/locale/sr.js",
	"./ss": "../../../../moment/locale/ss.js",
	"./ss.js": "../../../../moment/locale/ss.js",
	"./sv": "../../../../moment/locale/sv.js",
	"./sv.js": "../../../../moment/locale/sv.js",
	"./sw": "../../../../moment/locale/sw.js",
	"./sw.js": "../../../../moment/locale/sw.js",
	"./ta": "../../../../moment/locale/ta.js",
	"./ta.js": "../../../../moment/locale/ta.js",
	"./te": "../../../../moment/locale/te.js",
	"./te.js": "../../../../moment/locale/te.js",
	"./tet": "../../../../moment/locale/tet.js",
	"./tet.js": "../../../../moment/locale/tet.js",
	"./th": "../../../../moment/locale/th.js",
	"./th.js": "../../../../moment/locale/th.js",
	"./tl-ph": "../../../../moment/locale/tl-ph.js",
	"./tl-ph.js": "../../../../moment/locale/tl-ph.js",
	"./tlh": "../../../../moment/locale/tlh.js",
	"./tlh.js": "../../../../moment/locale/tlh.js",
	"./tr": "../../../../moment/locale/tr.js",
	"./tr.js": "../../../../moment/locale/tr.js",
	"./tzl": "../../../../moment/locale/tzl.js",
	"./tzl.js": "../../../../moment/locale/tzl.js",
	"./tzm": "../../../../moment/locale/tzm.js",
	"./tzm-latn": "../../../../moment/locale/tzm-latn.js",
	"./tzm-latn.js": "../../../../moment/locale/tzm-latn.js",
	"./tzm.js": "../../../../moment/locale/tzm.js",
	"./uk": "../../../../moment/locale/uk.js",
	"./uk.js": "../../../../moment/locale/uk.js",
	"./ur": "../../../../moment/locale/ur.js",
	"./ur.js": "../../../../moment/locale/ur.js",
	"./uz": "../../../../moment/locale/uz.js",
	"./uz-latn": "../../../../moment/locale/uz-latn.js",
	"./uz-latn.js": "../../../../moment/locale/uz-latn.js",
	"./uz.js": "../../../../moment/locale/uz.js",
	"./vi": "../../../../moment/locale/vi.js",
	"./vi.js": "../../../../moment/locale/vi.js",
	"./x-pseudo": "../../../../moment/locale/x-pseudo.js",
	"./x-pseudo.js": "../../../../moment/locale/x-pseudo.js",
	"./yo": "../../../../moment/locale/yo.js",
	"./yo.js": "../../../../moment/locale/yo.js",
	"./zh-cn": "../../../../moment/locale/zh-cn.js",
	"./zh-cn.js": "../../../../moment/locale/zh-cn.js",
	"./zh-hk": "../../../../moment/locale/zh-hk.js",
	"./zh-hk.js": "../../../../moment/locale/zh-hk.js",
	"./zh-tw": "../../../../moment/locale/zh-tw.js",
	"./zh-tw.js": "../../../../moment/locale/zh-tw.js"
};
function webpackContext(req) {
	return __webpack_require__(webpackContextResolve(req));
};
function webpackContextResolve(req) {
	var id = map[req];
	if(!(id + 1)) // check for number or string
		throw new Error("Cannot find module '" + req + "'.");
	return id;
};
webpackContext.keys = function webpackContextKeys() {
	return Object.keys(map);
};
webpackContext.resolve = webpackContextResolve;
module.exports = webpackContext;
webpackContext.id = "../../../../moment/locale recursive ^\\.\\/.*$";

/***/ }),

/***/ 1:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../src/main.ts");


/***/ })

},[1]);
//# sourceMappingURL=main.bundle.js.map