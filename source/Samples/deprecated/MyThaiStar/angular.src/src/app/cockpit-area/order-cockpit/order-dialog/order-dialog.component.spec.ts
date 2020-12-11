import { HttpClient, HttpClientModule } from '@angular/common/http';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material';

import { CoreModule } from '../../../core/core.module';
import { BackendModule } from '../../../backend/backend.module';
import { WaiterCockpitModule } from '../../cockpit.module';

import { WaiterCockpitService } from '../../shared/waiter-cockpit.service';
import { PriceCalculatorService } from '../../../sidenav/shared/price-calculator.service';

import { OrderDialogComponent } from './order-dialog.component';

describe('OrderDialogComponent', () => {
  let component: OrderDialogComponent;
  let dialog: MatDialog;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [WaiterCockpitService, PriceCalculatorService, HttpClient],
      imports: [
        BrowserAnimationsModule,
        WaiterCockpitModule,
        CoreModule,
        HttpClientModule,
        BackendModule.forRoot({ environmentType: 0, restServiceRoot: 'v1' }),
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    dialog = TestBed.get(MatDialog);
    component = dialog.open(OrderDialogComponent, { data: { dialogData: { row: undefined } } }).componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
