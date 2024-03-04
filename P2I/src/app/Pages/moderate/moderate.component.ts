import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Game, Objet, UserIG } from 'src/app/Models/models';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-moderate',
  templateUrl: './moderate.component.html',
  styleUrls: ['./moderate.component.scss'],
})
export class ModerateComponent {
  loading = false;
  error = '';
  gameId = this.route.snapshot.paramMap.get('id') ?? '0';
  game: Game | undefined;
  currentObjet: Objet | undefined;
  objetDebutValidite: string | undefined;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gameService: GameService,
    private authService: AuthService
  ) {}

  unrolledPlayers = false;

  async getGame() {
    this.loading = true;
    var game = await this.gameService.getGameById(parseInt(this.gameId ?? '0'));
    game.players = await this.gameService.getPlayersInOrderFromGame(
      parseInt(this.gameId ?? '0')
    );
    return game;
  }

  async postObjet() {
    console.log('postObjet');

    var now = new Date();

    var rep = await this.gameService.postObject(
      'Porter un parapluie',
      'Il doit être tenu dans la main droite%%Il doit être ouvert%%Il doit être de couleur bleue%%',
      1,
      now.toISOString(),
      new Date(
        new Date().setTime(now.getTime() + 240 * 60 * 60 * 1000)
      ).toISOString()
    );
    console.log('postObjet', rep);
  }
  async getCurrentObject() {
    this.currentObjet = await this.gameService.getCurrentObject(
      parseInt(this.gameId)
    );
    let dd = new Date(this.currentObjet?.debutValidite!)
      .getDay()
      .toString()
      .padStart(2, '0');
    let mm = new Date(this.currentObjet?.debutValidite!)
      .getMonth()
      .toString()
      .padStart(2, '0');
    let yyyy = new Date(this.currentObjet?.debutValidite!)
      .getFullYear()
      .toString()
      .padStart(2, '0');
    let hh = new Date(this.currentObjet?.debutValidite!)
      .getHours()
      .toString()
      .padStart(2, '0');
    let min = new Date(this.currentObjet?.debutValidite!)
      .getMinutes()
      .toString()
      .padStart(2, '0');

    this.objetDebutValidite = `${dd}/${mm}/${yyyy} à ${hh}:${min}`;
  }

  async Shuffle() {
    this.loading = true;
    await this.gameService.shuffleGameWithId(this.game!.id);
    this.game = await this.getGame();
    this.loading = false;
  }

  async initGame() {
    this.loading = true;
    if (!(await this.authService.canModerate(parseInt(this.gameId)))) {
      this.error = "Vous n'avez pas les droits pour modérer cette partie";
      this.loading = false;
      return;
    }
    const game = await this.getGame();
    this.game = game;
    this.loading = false;
  }
  async ngOnInit() {
    this.loading = true;
    // this.postObjet();
    this.getCurrentObject();
    try {
      await this.initGame();
    } catch (err: any) {
      this.error = 'Erreur lors du chargement : ' + err.message;
    }
  }
}
