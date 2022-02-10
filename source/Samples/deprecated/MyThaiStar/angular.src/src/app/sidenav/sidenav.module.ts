import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';

import { SidenavService } from './shared/sidenav.service';
import { PriceCalculatorService } from './shared/price-calculator.service';
import { WindowService } from '../core/windowService/windowService.service';

import { SidenavComponent } from './sidenav.component';
import { SidenavOrderComponent } from './sidenav-order/sidenav-order.component';
import { CommentDialogComponent } from './comment-dialog/comment-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
  ],
  providers: [
    SidenavService,
    PriceCalculatorService,
    WindowService,
  ],
  declarations: [
    SidenavComponent,
    SidenavOrderComponent,
    CommentDialogComponent,
  ],
  exports: [
    SidenavComponent,
  ],
  entryComponents: [
    CommentDialogComponent,
  ],
})
export class SidenavModule { }
