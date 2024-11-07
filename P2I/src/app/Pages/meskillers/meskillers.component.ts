import { Component, OnInit } from '@angular/core';
import { Game } from 'src/app/Models/models';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-meskillers',
  templateUrl: './meskillers.component.html',
  styleUrls: ['./meskillers.component.scss'],
})
export class MeskillersComponent implements OnInit {
  gamesOfUser: Game[] = [];
  gamesOfModerator: Game[] = [];

  connected: boolean = true;

  constructor(
    private gameService: GameService,
    private authService: AuthService,
    private router: Router // Inject the Router service
  ) { }

  async ngOnInit() {
    if (await this.authService.isLoggedIn()) {
      this.connected = true;
      this.gamesOfUser = await this.gameService.getGamesOfPlayer(
        this.authService.getLoggedUserId()
      );
      this.gamesOfModerator = await this.gameService.getGamesOfModerator(
        this.authService.getLoggedUserId()
      );
    } else {
      // Redirect to the login page if the user is not logged in
      this.connected = false;
    }
  }
}
