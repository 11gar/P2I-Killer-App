import { Injectable } from '@angular/core';
import { User } from '../Models/models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import route from './route.json';
import skip from './skipNgrok.json';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  route = route.route;
  private headers = new HttpHeaders(skip);
  constructor(private http: HttpClient) {}

  async getUsers() {
    const url = `${this.route}users`;
    const headers = this.headers;
    const resp = await lastValueFrom(this.http.get<User[]>(url, { headers }));
    return resp;
  }
}
