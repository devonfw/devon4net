import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreModule } from '../core/core.module';

import { WaiterCockpitService } from './shared/waiter-cockpit.service';
import { WindowService } from '../core/windowService/windowService.service';

import { ReservationCockpitComponent } from './reservation-cockpit/reservation-cockpit.component';
import { OrderCockpitComponent } from './order-cockpit/order-cockpit.component';
import { OrderDialogComponent } from './order-cockpit/order-dialog/order-dialog.component';
import { ReservationDialogComponent } from './reservation-cockpit/reservation-dialog/reservation-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
  ],
  providers: [
    WaiterCockpitService,
    WindowService,
  ],
  declarations: [
    ReservationCockpitComponent,
    OrderCockpitComponent,
    ReservationDialogComponent,
    OrderDialogComponent,
  ],
  exports: [
    ReservationCockpitComponent,
    OrderCockpitComponent,
  ],
  entryComponents: [
    ReservationDialogComponent,
    OrderDialogComponent,
  ],
})
export class WaiterCockpitModule { }
