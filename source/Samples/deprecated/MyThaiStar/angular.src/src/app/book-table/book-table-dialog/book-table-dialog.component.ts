import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { BookTableService } from '../shared/book-table.service';
import { SnackBarService } from '../../core/snackService/snackService.service';
import * as moment from 'moment';

@Component({
  selector: 'public-book-table-dialog',
  templateUrl: './book-table-dialog.component.html',
  styleUrls: ['./book-table-dialog.component.scss'],
})
export class BookTableDialogComponent implements OnInit {

  data: any;
  date: string;

  constructor (public snackBar: SnackBarService,
               public bookingService: BookTableService,
               private dialog: MatDialogRef<BookTableDialogComponent>,
               @Inject(MAT_DIALOG_DATA) dialogData: any) {
                 this.data = dialogData;
  }

  ngOnInit(): void {
    this.date = moment(this.data.bookingDate).format('LLL');
  }

  sendBooking (): void {
    this.bookingService.postBooking(this.bookingService.composeBooking(this.data, 0)).subscribe( () => {
      this.snackBar.openSnack('Table succesfully booked', 4000, 'green');
    }, (error: any) => {
      this.snackBar.openSnack('Error booking, please try again later', 4000, 'red');
    });
    this.dialog.close(true);
  }

}
