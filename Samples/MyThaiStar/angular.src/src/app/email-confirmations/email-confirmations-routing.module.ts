import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmailConfirmationsComponent } from './email-confirmations.component';

const emailConfirmationsRoutes: Routes = [
  { path: 'booking/acceptInvite/:token', component: EmailConfirmationsComponent},
  { path: 'booking/rejectInvite/:token', component: EmailConfirmationsComponent},
  { path: 'booking/cancel/:token', component: EmailConfirmationsComponent},
  { path: 'booking/cancelOrder/:token', component: EmailConfirmationsComponent},
];

@NgModule({
  imports: [
    RouterModule.forChild(emailConfirmationsRoutes),
  ],
  exports: [
    RouterModule,
  ],
})
export class EmailConfirmationsRoutingModule { }
