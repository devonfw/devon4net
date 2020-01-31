import { NgModule } from '@angular/core';
import { RouterModule, Routes }  from '@angular/router';

import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { BookTableComponent } from './book-table/book-table.component';
import { ReservationCockpitComponent } from './cockpit-area/reservation-cockpit/reservation-cockpit.component';
import { OrderCockpitComponent } from './cockpit-area/order-cockpit/order-cockpit.component';
import { EmailConfirmationsComponent } from './email-confirmations/email-confirmations.component';
import { NotFoundComponent } from './core/not-found/not-found.component';

import { AuthGuardService } from './core/authentication/auth-guard.service';

const appRoutes: Routes = [
  { path: 'restaurant', component: HomeComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'bookTable', component: BookTableComponent },
  { path: 'booking/:action/:token', component: EmailConfirmationsComponent },
  { path: 'orders', component: OrderCockpitComponent, canActivate: [AuthGuardService] },
  { path: 'reservations', component: ReservationCockpitComponent, canActivate: [AuthGuardService] },
  { path: '', redirectTo: '/restaurant', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true }, // <-- debugging purposes only
    ),
  ],
  exports: [
    RouterModule,
  ],
})
export class AppRoutingModule {}
