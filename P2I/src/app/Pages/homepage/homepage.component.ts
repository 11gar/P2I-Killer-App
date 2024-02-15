import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/models';
import { UserServiceService } from 'src/app/Services/user-service.service';
import { AuthService } from 'src/app/Services/auth.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss'],
})
export class HomepageComponent {
  constructor(
    public router: Router,
    private userService: UserServiceService,
    private authService: AuthService
  ) {}
  users: User[] | null = null;

  async ngOnInit() {
    console.log('coucou');
    console.log(this.router.url.split('/')[1]);
    this.userService.getUsers();
    //this.authService.register('admin', 'admin', 'admiprnom', 'adminom');

    // await this.userService.getUsers().then((t) => console.log(t));
    // this.users = await this.userService.getUsers();
  }
}