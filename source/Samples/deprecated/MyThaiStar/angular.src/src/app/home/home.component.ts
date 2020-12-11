import { Router } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'public-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {

  constructor(private router: Router) {
  }

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }

}
