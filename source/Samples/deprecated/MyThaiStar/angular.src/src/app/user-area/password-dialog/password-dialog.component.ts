import { UserAreaService } from '../shared/user-area.service';
import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'public-password-dialog',
  templateUrl: './password-dialog.component.html',
  styleUrls: ['./password-dialog.component.scss'],
})
export class PasswordDialogComponent {

  constructor(private dialog: MatDialogRef<PasswordDialogComponent>,
              private userService: UserAreaService) { }

  passwordSubmit(form: FormGroup): void {
    this.dialog.close();
    this.userService.changePassword(form);
  }

}
