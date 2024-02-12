import { Injectable } from '@angular/core';
import { User } from '../Models/models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import route from './route.json';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  constructor(private http: HttpClient) {}

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
