import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/models';
import { AuthService } from 'src/app/Services/auth.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss'],
})
export class HomepageComponent {
  constructor(public router: Router, private authService: AuthService) {}

  async ngOnInit() {
    //this.authService.register('admin', 'admin', 'admiprnom', 'adminom');
    // await this.userService.getUsers().then((t) => console.log(t));
    // this.users = await this.userService.getUsers();
  }

  onStart() {
    return this.router.url.split('/')[1] == '';
  }
}
