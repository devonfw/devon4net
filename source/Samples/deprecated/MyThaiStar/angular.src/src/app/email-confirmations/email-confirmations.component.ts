import { InvitationResponse } from '../shared/viewModels/interfaces';
import { EmailConfirmationsService } from './shared/email-confirmations.service';
import { Observable } from 'rxjs/Rx';
import { SnackBarService } from '../core/snackService/snackService.service';
import { UrlSegment } from '@angular/router/router';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'public-email-confirmations',
  templateUrl: './email-confirmations.component.html',
  styleUrls: ['./email-confirmations.component.scss'],
})
export class EmailConfirmationsComponent implements OnInit {
  private action: string;
  private token: string;

  constructor(private snackBarService: SnackBarService,
    private emailService: EmailConfirmationsService,
    private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap
      .map((params: ParamMap) => params)
      .subscribe((params: ParamMap) => {
        this.token = params.get('token');
        this.action = params.get('action');
        switch (this.action) {
          case 'acceptInvite':
            this.emailService.sendAcceptInvitation(this.token).subscribe((res: InvitationResponse) => {
                this.snackBarService.openSnack('Invitation succesfully accepted', 10000, 'green');
              },
              (error: any) => {
                this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
              });
            break;
          case 'rejectInvite':
            this.emailService.sendRejectInvitation(this.token).subscribe((res: InvitationResponse) => {
                this.snackBarService.openSnack('Invitation succesfully rejected', 10000, 'red');
              },
              (error: any) => {
                this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
              });
            break;
          case 'cancel':
            this.emailService.sendCancelBooking(this.token).subscribe((res: InvitationResponse) => {
                this.snackBarService.openSnack('Booking succesfully canceled', 10000, 'green');
              },
              (error: any) => {
                this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
              });
            break;
          case 'cancelOrder':
            this.emailService.sendCancelOrder(this.token).subscribe((res: boolean) => {
                this.snackBarService.openSnack('Order succesfully canceled', 10000, 'green');
              },
              (error: any) => {
                this.snackBarService.openSnack('An error has ocurred, please try again later', 10000, 'red');
              });
            break;
          default:
            this.snackBarService.openSnack('Url not found, please try again', 10000, 'black');
            break;
        }
      });
    // Navigate to home
    this.router.navigate(['restaurant']);
  }
}
