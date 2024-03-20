import { Component } from '@angular/core';
import { Game } from 'src/app/Models/models';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-meskillers',
  templateUrl: './meskillers.component.html',
  styleUrls: ['./meskillers.component.scss'],
})
export class MeskillersComponent {
  constructor(
    private gameService: GameService,
    private authService: AuthService
  ) {}

  gamesOfUser: Game[] = [];
  gamesOfModerator: Game[] = [];

  async ngOnInit() {
    this.gamesOfUser = await this.gameService.getGamesOfPlayer(
      this.authService.getLoggedUserId()
    );
    this.gamesOfModerator = await this.gameService.getGamesOfModerator(
      this.authService.getLoggedUserId()
    );
  }
}
