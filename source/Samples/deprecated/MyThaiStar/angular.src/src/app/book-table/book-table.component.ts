import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material';
import { BookTableDialogComponent } from './book-table-dialog/book-table-dialog.component';
import { InvitationDialogComponent } from './invitation-dialog/invitation-dialog.component';
import { WindowService } from '../core/windowService/windowService.service';
import { SnackBarService } from '../core/snackService/snackService.service';
import { emailValidator } from '../shared/directives/email-validator.directive';
import { last } from 'lodash';

@Component({
  selector: 'public-book-table',
  templateUrl: './book-table.component.html',
  styleUrls: ['./book-table.component.scss'],
})

export class BookTableComponent {

  invitationModel: string[] = [];
  minDate: Date = new Date();

  constructor(public window: WindowService,
              public snackBarservice: SnackBarService,
              public dialog: MatDialog) {
  }

  showBookTableDialog(form: FormGroup): void {
    let dialogRef: MatDialogRef<BookTableDialogComponent> = this.dialog.open(BookTableDialogComponent, {
      width: this.window.responsiveWidth(),
      data: form.value,
    });
    dialogRef.afterClosed().subscribe((res: boolean) => {
      if (res) {
        form.reset();
      }
    });
  }

  showInviteDialog(form: FormGroup): void {
    let dialogRef: MatDialogRef<InvitationDialogComponent> = this.dialog.open(InvitationDialogComponent, {
      width: this.window.responsiveWidth(),
      data: form.value,
    });
    dialogRef.afterClosed().subscribe((res: boolean) => {
      if (res) {
        form.reset();
        this.invitationModel = [];
      }
    });
  }

  validateEmail(): void {
    if (!emailValidator(last(this.invitationModel))) {
      this.invitationModel.pop();
      this.snackBarservice.openSnack('Email format not valid', 1000, 'red');
    }
  }

}
