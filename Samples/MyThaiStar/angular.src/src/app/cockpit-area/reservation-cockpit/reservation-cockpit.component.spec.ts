import { HttpClient, HttpClientModule } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { BackendModule } from '../../backend/backend.module';
import { CoreModule } from '../../core/core.module';

import { PriceCalculatorService } from '../../sidenav/shared/price-calculator.service';
import { WaiterCockpitService } from '../shared/waiter-cockpit.service';

import { ReservationCockpitComponent } from './reservation-cockpit.component';

describe('ReservationCockpitComponent', () => {
  let component: ReservationCockpitComponent;
  let fixture: ComponentFixture<ReservationCockpitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReservationCockpitComponent],
      providers: [WaiterCockpitService, PriceCalculatorService, HttpClient],
      imports: [
        CoreModule,
        BackendModule.forRoot({ environmentType: 0, restServiceRoot: 'v1' }),
        BrowserAnimationsModule,
        HttpClientModule,
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationCockpitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
