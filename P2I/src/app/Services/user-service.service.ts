import { Injectable } from '@angular/core';
import { User } from '../Models/models';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  constructor() {}

  route = 'http://localhost:5149/api/';

  async getUsers() {
    var response = await fetch('http://localhost:5149/api/users', {
      mode: 'cors',
    });
    var data = await response.json();
    var user: User[] = data;
    console.log(user);
    return user;
  }
}
