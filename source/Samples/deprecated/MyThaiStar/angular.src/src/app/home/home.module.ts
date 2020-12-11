import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';

import { HomeComponent } from './home.component';

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
  ],
  providers: [
  ],
  declarations: [
    HomeComponent,
  ],
  exports: [
    HomeComponent,
  ],
})
export class HomeModule { }
