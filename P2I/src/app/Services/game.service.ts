import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { catchError, throwError, lastValueFrom } from 'rxjs';
import { Game, UserIG } from '../Models/models';
import route from './route.json';
import skip from './skipNgrok.json';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  route = route.route;
  private headers = new HttpHeaders(skip);
  constructor(private http: HttpClient) {}

  async createGame(nom: string, mdp: string) {
    const url = `${this.route}games/create?name=${nom}&mdp=${mdp}`;

    // You may need to include headers or other options as required by your API
    const headers = this.headers;
    const resp = await lastValueFrom(this.http.post<Game>(url, { headers }));
    return resp;
  }

  async getAllGames() {
    const url = `${this.route}games`;
    const headers = this.headers;
    const resp = await lastValueFrom(this.http.get<Game[]>(url, { headers }));
    return resp;
  }

  async getGameById(id: number) {
    const url = `${this.route}games/${id}`;
    const headers = this.headers;
    const resp = await lastValueFrom(
      this.http.get<Game>(url, {headers}).pipe(catchError(this.handleError))
    );
    return resp;
  }

  async joinGame(id: number, idUser: number) {

    const url = `${this.route}usersInGame/join?idGame=${id}&idUser=${idUser}`;
    const headers = this.headers;
    const resp = await lastValueFrom(
      this.http.post<Game>(url, { headers }).pipe(catchError(this.handleError))
    );

    return resp;
  }

  async getPlayersFromGame(id: number) {
    const headers = this.headers;
    const url = `${this.route}usersInGame/game/${id}`;
    const resp = await lastValueFrom(this.http.get<UserIG[]>(url, { headers }));
    return resp;
  }

  handleError(error: HttpErrorResponse) {

    const errorMessage = 'Error at Game Service : ' + error.message + '\n';
    var userMessage = '';
    switch (error.status) {
      case 404:
        userMessage += "La partie n'existe pas";
        break;
      case 405:
        userMessage += "L'utilisateur n'existe pas";
        break;
      case 301:
        userMessage += 'Vous êtes déjà dans la partie';
        break;
      case 500:
        userMessage += 'Internal Server Error';
        break;
      default:
        userMessage += 'Unknown Error';
    }
    console.log(errorMessage);
    console.log(userMessage);
    return throwError(() => new Error(userMessage));
  }
}
