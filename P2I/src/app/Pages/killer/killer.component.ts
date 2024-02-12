import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Game } from 'src/app/Models/models';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-killer',
  templateUrl: './killer.component.html',
  styleUrls: ['./killer.component.scss'],
})
export class KillerComponent {
  loading = false;
  error = '';
  gameId = this.route.snapshot.paramMap.get('id') ?? '0';
  game: Game | undefined;
  openLists: string[] = [];
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gameService: GameService
  ) {}

  async getGame() {
    this.loading = true;
    return this.gameService
      .getGameById(parseInt(this.gameId ?? '0'))
      .then(async (game) => {
        game.players = await this.gameService.getPlayersFromGame(game.id);
        return game;
      });
  }
  async setGame() {
    this.game = await this.getGame();
  }

  async ngOnInit() {
    this.loading = true;
    try {
      await this.setGame();
    } catch (err: any) {
      this.error = 'Erreur lors du chargement : ' + err.message;
    }
    console.log(this.game);

    this.loading = false;
  }
}
