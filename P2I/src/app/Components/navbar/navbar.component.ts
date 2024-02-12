import { Component } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  logged = this.authService.isLoggedIn();
  selected = this.router.url;
  constructor(private router: Router, private authService: AuthService) {
    router.events.subscribe((val) => {
      this.selected = this.router.url.split('/')[1];
    });
  }

  ngOnInit() {
    console.log(this.router.url);
  }

  select(id: string) {
    this.router.navigate([id.toString()]);
  }
}
