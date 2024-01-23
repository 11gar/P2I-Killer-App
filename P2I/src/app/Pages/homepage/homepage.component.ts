import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/models';
import { UserServiceService } from 'src/app/Services/user-service.service';
import {} from 'rxjs';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss'],
})
export class HomepageComponent {
  constructor(public router: Router, private userService: UserServiceService) {}
  users: User[] | null = null;

  async ngOnInit() {
    console.log('coucou');
    this.userService.getUsers();
    // await this.userService.getUsers().then((t) => console.log(t));
    // this.users = await this.userService.getUsers();
  }
}
