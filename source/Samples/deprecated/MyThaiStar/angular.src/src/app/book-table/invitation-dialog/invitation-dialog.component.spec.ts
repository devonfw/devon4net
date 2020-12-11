import { HttpClient, HttpClientModule } from '@angular/common/http';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialog } from '@angular/material';

import { BackendModule } from '../../backend/backend.module';
import { BookTableModule } from '../book-table.module';

import { BookTableService } from '../shared/book-table.service';
import { SnackBarService } from '../../core/snackService/snackService.service';

import { InvitationDialogComponent } from './invitation-dialog.component';

describe('InvitationDialogComponent', () => {
  let component: InvitationDialogComponent;
  let dialog: MatDialog;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [SnackBarService, BookTableService, HttpClient],
      imports: [
        BrowserAnimationsModule,
        BookTableModule,
        HttpClientModule,
        BackendModule.forRoot({environmentType: 0, restServiceRoot: 'v1'}),
      ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    dialog = TestBed.get(MatDialog);
    component = dialog.open(InvitationDialogComponent).componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
