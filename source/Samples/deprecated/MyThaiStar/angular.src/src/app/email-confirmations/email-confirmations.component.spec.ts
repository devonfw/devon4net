import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClient } from '@angular/common/http';

import { CoreModule } from '../core/core.module';
import { SnackBarService } from '../core/snackService/snackService.service';
import { BackendModule } from '../backend/backend.module';
import { EmailConfirmationsService } from './shared/email-confirmations.service';
import { EmailConfirmationsComponent } from './email-confirmations.component';

describe('EmailConfirmationsComponent', () => {
  let component: EmailConfirmationsComponent;
  let fixture: ComponentFixture<EmailConfirmationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [EmailConfirmationsComponent],
      providers: [
        SnackBarService,
        EmailConfirmationsService,
        HttpClient,
      ],
      imports: [
        CoreModule,
        BackendModule.forRoot({ environmentType: 0, restServiceRoot: 'v1' }),
        BrowserAnimationsModule,
        RouterTestingModule,
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailConfirmationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
