import { MatDialogRef } from '@angular/material';
import { Component } from '@angular/core';

@Component({
  selector: 'public-comment-dialog',
  templateUrl: './comment-dialog.component.html',
  styleUrls: ['./comment-dialog.component.scss'],
})
export class CommentDialogComponent {

  constructor(private dialog: MatDialogRef<CommentDialogComponent>) {}

  sendComment(comment: string): void {
    this.dialog.close(comment);
  }

}
