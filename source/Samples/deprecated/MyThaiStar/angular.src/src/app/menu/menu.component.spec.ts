import { HttpClient, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidenavService } from '../sidenav/shared/sidenav.service';
import { MenuService } from './shared/menu.service';
import { SnackBarService } from '../core/snackService/snackService.service';
import { AuthService } from '../core/authentication/auth.service';
import { MenuComponent } from './menu.component';
import { MenuCardComponent } from './menu-card/menu-card.component';
import { BackendModule } from './../backend/backend.module';
import { CoreModule } from '../core/core.module';

describe('MenuComponent', () => {
  let component: MenuComponent;
  let fixture: ComponentFixture<MenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MenuComponent, MenuCardComponent],
      providers: [SidenavService, MenuService, SnackBarService, AuthService, HttpClient],
      imports: [
        BrowserAnimationsModule,
        BackendModule.forRoot({ environmentType: 0, restServiceRoot: 'v1' }),
        CoreModule,
        HttpClientModule,
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
