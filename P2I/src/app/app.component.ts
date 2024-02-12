import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'P2I';
  topbarsize = '';
  loggedUserId = parseInt(localStorage.getItem('loggedUserId') ?? '-1');
  constructor(private router: Router, private authService: AuthService) {}

  onStart() {
    return this.router.url.split('/')[1] == '';
  }
  onGame() {
    return this.router.url.split('/')[1] == 'killer';
  }
  ngOnInit() {
    this.loggedUserId = parseInt(localStorage.getItem('loggedUserId') ?? '-1');
  }
}
