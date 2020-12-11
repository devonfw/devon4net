import { CoreModule } from '../core/core.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MenuService } from './shared/menu.service';

import { MenuCardComponent } from './menu-card/menu-card.component';
import { MenuComponent } from './menu.component';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    FormsModule,
  ],
  providers: [
    MenuService,
  ],
  declarations: [
    MenuComponent,
    MenuCardComponent,
  ],
  exports: [
    MenuComponent,
  ],
  entryComponents: [
  ],
})
export class MenuModule { }
