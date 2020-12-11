import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'public-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss'],
})
export class LoginDialogComponent {

  constructor(private dialog: MatDialogRef<LoginDialogComponent>) { }

  logInSubmit(formValue: FormGroup): void {
    this.dialog.close(formValue);
  }

  signInSubmit(formValue: FormGroup): void {
    this.dialog.close(formValue);
  }
}
