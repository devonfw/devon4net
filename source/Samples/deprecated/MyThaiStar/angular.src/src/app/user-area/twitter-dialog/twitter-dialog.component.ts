import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialogRef, MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'public-twitter-dialog',
  templateUrl: './twitter-dialog.component.html',
  styleUrls: ['./twitter-dialog.component.scss'],
})
export class TwitterDialogComponent {

  constructor(private dialog: MatDialogRef<TwitterDialogComponent>,
              public iconReg: MatIconRegistry,
              public sanitizer: DomSanitizer) {
    iconReg.addSvgIcon('twitter', sanitizer.bypassSecurityTrustResourceUrl('assets/images/Twitter_Logo.svg'));
   }

  twitterSubmit(form: FormGroup): void {
    // TODO
  }

}
