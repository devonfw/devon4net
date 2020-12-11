import { SidenavService } from '../shared/sidenav.service';
import { PriceCalculatorService } from '../shared/price-calculator.service';
import { MatDialog, MatDialogRef } from '@angular/material';
import { CommentDialogComponent } from '../comment-dialog/comment-dialog.component';
import { Component, OnInit, Input } from '@angular/core';
import { TdDialogService } from '@covalent/core';
import { ExtraView, OrderView } from '../../shared/viewModels/interfaces';
import { map } from 'lodash';

@Component({
  selector: 'public-sidenav-order',
  templateUrl: './sidenav-order.component.html',
  styleUrls: ['./sidenav-order.component.scss'],
})
export class SidenavOrderComponent implements OnInit {

  extras: string;
  @Input('order') order: OrderView;

  constructor(private sidenav: SidenavService,
              public dialog: MatDialog,
              private _dialogService: TdDialogService,
              private calculator: PriceCalculatorService,
  ) {}

  ngOnInit(): void {
    this.extras = map(this.order.extras, 'name').join(', ');
 }

  removeComment(): void {
    this.order.orderLine.comment = undefined;
  }

  addComment(): void {
    let dialogRef: MatDialogRef<CommentDialogComponent> = this.dialog.open(CommentDialogComponent);
    dialogRef.afterClosed().subscribe((result: string) => {
      this.order.orderLine.comment = result;
    });
  }

  increaseOrder(): void {
    this.sidenav.increaseOrder(this.order);
  }

  decreaseOrder(): void {
    this.sidenav.decreaseOrder(this.order);
  }

  removeOrder(): void {
    this.sidenav.removeOrder(this.order);
  }

  calculateOrderPrice(): number {
    return this.calculator.getPrice(this.order);
  }

  openCommentDialog(): void {
    this._dialogService.openAlert({
      message: this.order.orderLine.comment,
      title: 'Comment',
      closeButton: 'Close',
    });
  }

}
