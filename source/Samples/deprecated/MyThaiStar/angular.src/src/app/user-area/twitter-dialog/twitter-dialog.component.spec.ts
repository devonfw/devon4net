import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material';

import { CoreModule } from '../../core/core.module';
import { UserAreaModule } from '../user-area.module';

import { SnackBarService } from '../../core/snackService/snackService.service';

import { TwitterDialogComponent } from './twitter-dialog.component';

describe('TwitterDialogComponent', () => {
  let component: TwitterDialogComponent;
  let dialog: MatDialog;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        CoreModule,
        BrowserAnimationsModule,
        UserAreaModule,
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    dialog = TestBed.get(MatDialog);
    component = dialog.open(TwitterDialogComponent).componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
