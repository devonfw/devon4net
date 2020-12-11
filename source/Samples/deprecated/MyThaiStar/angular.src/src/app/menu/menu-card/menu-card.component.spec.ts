
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CoreModule } from '../../core/core.module';

import { SidenavService } from '../../sidenav/shared/sidenav.service';
import { BookingInMemoryService } from '../../backend/booking/booking-in-memory.service';
import { BookingDataService } from '../../backend/booking/booking-data-service';
import { DishesDataService } from '../../backend/dishes/dishes-data-service';
import { DishesInMemoryService } from '../../backend/dishes/dishes-in-memory.service';
import { OrderInMemoryService } from '../../backend/order/order-in-memory.service';
import { OrderDataService } from '../../backend/order/order-data-service';
import { MenuService } from '../shared/menu.service';
import { SnackBarService } from '../../core/snackService/snackService.service';
import { AuthService } from '../../core/authentication/auth.service';

import { MenuCardComponent } from './menu-card.component';

describe('MenuCardComponent', () => {
  let component: MenuCardComponent;
  let fixture: ComponentFixture<MenuCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MenuCardComponent ],
      providers: [
        MenuService,
        SidenavService,
        AuthService,
        SnackBarService,
        { provide: BookingDataService, useClass: BookingInMemoryService},
        { provide: OrderDataService, useClass: OrderInMemoryService},
        { provide: DishesDataService, useClass: DishesInMemoryService}],
      imports: [
        CoreModule,
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuCardComponent);
    component = fixture.componentInstance;
    component.menuInfo = {
      dish: {
        id: 0,
        name: '',
        description: '',
        price: 0,
      },
      image: {content: 'string'},
      extras: [],
      likes: 0,
      isfav: true,
    };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
