import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { SidenavService } from './shared/sidenav.service';
import { PriceCalculatorService } from './shared/price-calculator.service';
import { SnackBarService } from '../core/snackService/snackService.service';
import { SidenavOrderComponent } from './sidenav-order/sidenav-order.component';
import { ExtraView, OrderView } from '../shared/viewModels/interfaces';
import { toNumber } from 'lodash';

@Component({
  selector: 'public-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss'],
})
export class SidenavComponent implements OnInit {

  orders: OrderView[];

  constructor(private router: Router,
    private sidenav: SidenavService,
    private snackBar: SnackBarService,
    private calculator: PriceCalculatorService,
  ) {}

  ngOnInit(): void {
    this.orders = this.sidenav.getOrderData();
  }

  closeSidenav(): void {
    this.sidenav.closeSideNav();
  }

  navigateTo(route: string): void {
    this.router.navigate([route]);
    this.closeSidenav();
  }

  calculateTotal(): number {
    return this.calculator.getTotalPrice(this.orders);
  }

  sendOrders(bookingId: string): void {
    this.sidenav.sendOrders(bookingId)
        .subscribe(() => {
            this.orders = this.sidenav.removeAllOrders();
            this.snackBar.openSnack('Order correctly noted', 4000, 'green');
        },
        (error: any) => {
            this.snackBar.openSnack('Error sending order, please, try again later', 4000, 'red');
        });
  }
}
