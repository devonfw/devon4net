import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class SnackBarService {

    constructor(public snackBar: MatSnackBar) { }

    openSnack(message: string, duration: number, color: string): void {
            this.snackBar.open(message, 'OK', {
                duration: duration,
                extraClasses: ['bgc-' + color + '-600'],
            });
    }
}
