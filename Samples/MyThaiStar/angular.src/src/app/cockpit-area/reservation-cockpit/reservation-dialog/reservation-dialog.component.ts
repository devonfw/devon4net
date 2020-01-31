import { Component, OnInit, Inject } from '@angular/core';
import { IPageChangeEvent, ITdDataTableColumn, TdDataTableService } from '@covalent/core';
import { FriendsInvite, ReservationView } from '../../../shared/viewModels/interfaces';
import { WaiterCockpitService } from '../../shared/waiter-cockpit.service';
import { MAT_DIALOG_DATA } from '@angular/material';
import { config } from '../../../config';

@Component({
  selector: 'cockpit-reservation-dialog',
  templateUrl: './reservation-dialog.component.html',
  styleUrls: ['./reservation-dialog.component.scss'],
})
export class ReservationDialogComponent implements OnInit {

  private datao: FriendsInvite[] = [];
  private columnso: ITdDataTableColumn[] = [
    { name: 'email', label: 'Guest email'},
    { name: 'accepted', label: 'Acceptances and declines'},
  ];

  private pageSizes: number[] = config.pageSizesDialog;

  private fromRow: number = 1;
  private currentPage: number = 1;
  private pageSize: number = 4;

  data: any;

  datat: ReservationView[] = [];
  columnst: ITdDataTableColumn[] = [
    { name: 'booking.bookingDate', label: 'Reservation date'},
    { name: 'booking.creationDate', label: 'Creation date'},
    { name: 'booking.name', label: 'Owner' },
    { name: 'booking.email', label: 'Email' },
    { name: 'booking.tableId', label: 'Table'},
  ];

  filteredData: any[] = this.datao;

  constructor(private _dataTableService: TdDataTableService,
              private waiterCockpitService: WaiterCockpitService,
              @Inject(MAT_DIALOG_DATA) dialogData: any) {
                 this.data = dialogData.row;
  }

  ngOnInit(): void {
    this.datat.push(this.data);
    this.datao = this.data.invitedGuests;
    if (this.data.booking.assistants) {
      this.columnst.push({ name: 'booking.assistants', label: 'Assistants'});
    }
    this.filter();
  }

  page(pagingEvent: IPageChangeEvent): void {
    this.fromRow = pagingEvent.fromRow;
    this.currentPage = pagingEvent.page;
    this.pageSize = pagingEvent.pageSize;
    this.filter();
  }

  filter(): void {
    let newData: any[] = this.datao;
    newData = this._dataTableService.pageData(newData, this.fromRow, this.currentPage * this.pageSize);
    setTimeout(() => this.filteredData = newData);
  }

}
