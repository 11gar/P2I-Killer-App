import { CSP_NONCE, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { User } from '../Models/models';
import route from './route.json';
import skip from './skipNgrok.json';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticated: boolean = false;
  private headers = () => new HttpHeaders({
    Authorization: `Bearer ${localStorage.getItem('token')}`,
    ...skip});
  route = route.route;

  constructor(private http: HttpClient) {}

  async login(login: string, password: string) {
    this.logout();
    const url = `${this.route}users/login?login=${login}&password=${password}`;

    // You may need to include headers or other options as required by your API
    const headers = this.headers();

    const isgood = await lastValueFrom(this.http.get<any>(url, { headers }));
    console.log(isgood);
    if (isgood && isgood["userId"]) {
      localStorage.setItem('loggedUserId', isgood["userId"]);
      this.isAuthenticated = true;
      localStorage.setItem("token", isgood["token"]);
    } else {
      this.logout();
    }
    return this.isAuthenticated;
  }

  async logout() {
    localStorage.removeItem('loggedUserId');
    localStorage.removeItem("token");
    this.isAuthenticated = false;
  }

  getLoggedUserId() {
    this.isLoggedIn();
    return parseInt(localStorage.getItem('loggedUserId') ?? '-1');
  }

  async getByLogin(login: string) {
    const url = `${this.route}users/login/${login}`;
    console.log(url);
    const headers = this.headers();

    const resp = await lastValueFrom(this.http.get<number>(url, { headers }));
    console.log(resp);
    return resp;
  }

  async canModerate(gameId: number) {
    const url = `${
      this.route
    }users/${this.getLoggedUserId()}/canModerate/${gameId}`;
    const headers = this.headers();
    return await lastValueFrom(this.http.get<boolean>(url, { headers }));
  }

  async isLoggedIn(): Promise<boolean> {
    if (
      localStorage.getItem('loggedUserId') !== '' &&
      localStorage.getItem('loggedUserId') !== null
    ) {
      this.isAuthenticated = true;
    }

    const url = `${this.route}users/islogged`;
    const headers = this.headers();

    return await this.http
      .get<any>(url, { headers, observe: 'response' })
      .toPromise()
      .then((response) => {
        this.isAuthenticated = response?.status === 200 && response?.body === 1;
        return this.isAuthenticated;
      })
      .catch((error) => {
        console.error('Error during isLoggedIn check:', error);
        this.isAuthenticated = false;
        return false;
      });
  }

  async register(login: string, password: string, prenom: string, nom: string) {
    const headers = this.headers();

    console.log('registering');
    const url = `${this.route}users/register?login=${login}&password=${password}&prenom=${prenom}&nom=${nom}`;

    // You may need to include headers or other options as required by your API

    const resp = await lastValueFrom(this.http.post<User>(url, { headers }));
    if (resp != null) {
      await this.login(login, password);
      console.log('registered', resp);
      return true;
    }
    return false;
  }
}
