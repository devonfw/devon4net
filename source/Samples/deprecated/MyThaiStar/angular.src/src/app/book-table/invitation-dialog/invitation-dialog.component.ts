import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { BookTableService } from '../shared/book-table.service';
import { SnackBarService } from '../../core/snackService/snackService.service';
import * as moment from 'moment';

@Component({
  selector: 'public-invitation-dialog',
  templateUrl: './invitation-dialog.component.html',
  styleUrls: ['./invitation-dialog.component.scss'],
})
export class InvitationDialogComponent implements OnInit {

  data: any;
  date: string;

  constructor(private snackBar: SnackBarService,
              private invitationService: BookTableService,
              private dialog: MatDialogRef<InvitationDialogComponent>,
              @Inject(MAT_DIALOG_DATA) dialogData: any) {
                 this.data = dialogData;
  }

  ngOnInit(): void {
    this.date = moment(this.data.bookingDate).format('LLL');
  }

  sendInvitation(): void {
    this.invitationService.postBooking(this.invitationService.composeBooking(this.data, 1)).subscribe( () => {
      this.snackBar.openSnack('Table succesfully booked', 4000, 'green');
    }, (error: any) => {
      this.snackBar.openSnack('Error booking, please try again later', 4000, 'red');
    });
    this.dialog.close(true);
  }

}
