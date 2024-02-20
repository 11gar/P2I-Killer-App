import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Game, UserIG } from 'src/app/Models/models';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-killer',
  templateUrl: './killer.component.html',
  styleUrls: ['./killer.component.scss'],
})
export class KillerComponent {
  killOnHold: any = null;
  killingOnHold: any = null;

  loading = false;
  error = '';
  gameId = this.route.snapshot.paramMap.get('id') ?? '0';
  game: Game | undefined;
  openLists: string[] = [];
  player: UserIG | undefined;
  cible: UserIG | undefined;
  hidden = true;
  confirmingKill = false;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gameService: GameService,
    private authService: AuthService
  ) {}

  async gettingKilled() {
    this.loading = true;
    this.killOnHold = await this.gameService.isUserBeingKilled(
      this.player?.id ?? 0
    );
  }

  async getKilling() {
    this.loading = true;
    this.killingOnHold = await this.gameService.isUserKilling(
      this.player?.id ?? 0
    );
  }

  async getGame() {
    this.loading = true;
    var game = await this.gameService.getGameById(parseInt(this.gameId ?? '0'));
    game.players = await this.gameService.getPlayersFromGame(
      parseInt(this.gameId ?? '0')
    );
    return game;
  }

  async getPlayer(game: Game, id: number) {
    return game?.players.find((p) => p.id == id);
  }

  async getTargetOfP(game: Game, id: number) {
    return this.game?.players.find(
      (p) => p.id == game?.players.find((p) => p.id == id)?.cible?.id
    );
  }

  async initGame() {
    this.loading = true;
    const game = await this.getGame();
    this.game = game;
    this.player = await this.getPlayer(
      game,
      this.authService.getLoggedUserId()
    );
    this.cible = await this.getTargetOfP(
      game,
      this.authService.getLoggedUserId()
    );
    await this.gettingKilled();
    await this.getKilling();
  }
  // setPlayer() {
  //   this.player = this.game?.players.find(
  //     (p) => p.id == this.authService.getLoggedUserId()
  //   );
  // }
  // setTarget() {
  //   console.log('cible:', this.player?.Cible);
  //   this.cible = this.game?.players.find((p) => p.id == this.player?.Cible?.id);
  // }

  async ngOnInit() {
    this.loading = true;
    try {
      await this.initGame();
    } catch (err: any) {
      this.error = 'Erreur lors du chargement : ' + err.message;
    }
    console.log(
      parseInt(localStorage.getItem('loggedUserId') ?? '0'),
      this.player,
      this.cible
    );
    this.loading = false;
  }

  async kill() {
    this.loading = true;
    if (this.player?.cible?.id) {
      try {
        await this.gameService.kill(
          this.cible?.id ?? 0,
          this.authService.getLoggedUserId(),
          this.game?.id ?? 0
        );
      } catch (error: any) {
        console.log(error.message);
        this.error = error.message;
        this.loading = false;
        return;
      }
    }
    this.loading = false;
    this.router.navigate(['/killer/game', { id: this.game?.id }]);
  }

  async confirmKill() {
    this.loading = true;
    try {
      await this.gameService.validateKill(this.killOnHold.id);
    } catch (error: any) {
      console.log(error.message);
      this.error = error.message;
      this.loading = false;
      return;
    }
    this.loading = false;
    this.router.navigate(['/killer/game', { id: this.game?.id }]);
  }
  async cancelKill() {
    this.loading = true;
    try {
      await this.gameService.cancelKill(this.killOnHold.id);
    } catch (error: any) {
      console.log(error.message);
      this.error = error.message;
      this.loading = false;
      return;
    }
    this.loading = false;
    this.router.navigate(['/killer/game', { id: this.game?.id }]);
  }
}
