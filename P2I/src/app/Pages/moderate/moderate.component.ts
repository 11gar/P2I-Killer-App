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
  teamerror = '';
  gameId = this.route.snapshot.paramMap.get('id') ?? '0';
  game: Game | undefined;
  currentObjet: Objet | undefined;
  objetDebutValidite: string | undefined;
  objets: Objet[] = [];

  csvOrder = '';
  newteamname = '';
  newteamcolor = 'FF00FF';

  nomobjet = '';
  description = '';
  offset = new Date().getTimezoneOffset();
  now = new Date(new Date().getTime() - this.offset * 60 * 1000);

  debutValidite = this.now.toISOString().split('.')[0];
  finValidite = new Date(
    new Date().setTime(this.now.getTime() + 24 * 60 * 60 * 1000)
  )
    .toISOString()
    .split('.')[0];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gameService: GameService,
    private authService: AuthService
  ) {}

  unrolledPlayers = false;
  unrolledObjets = false;
  unrolledTeams = false;

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

  async addNewTeam() {
    this.loading = true;
    if (this.newteamname === '' || this.newteamcolor === '') {
      this.teamerror = 'Tous les champs doivent être remplis';
      this.loading = false;
      return;
    }
    await this.gameService.newTeamInGame(
      this.game!.id,
      this.newteamname,
      this.newteamcolor
    );
    this.game = await this.getGame();
    this.loading = false;
  }

  async deleteObjet(id: number) {
    this.loading = true;
    try {
      await this.gameService.deleteObjet(id);
    } catch {
      this.error = "Erreur lors de la suppression de l'objet";
    }
    this.objets = this.objets.filter((o) => o.id != id);
    this.loading = false;
  }

  async Shuffle() {
    this.loading = true;
    await this.gameService.shuffleGameWithId(this.game!.id, this.csvOrder);
    this.game = await this.getGame();
    this.loading = false;
  }

  async startGame() {
    this.loading = true;
    await this.gameService.startGameWithId(this.game!.id);
    this.game = await this.getGame();
    this.loading = false;
  }

  async deleteTeam(id: number) {
    this.loading = true;
    await this.gameService.deleteTeam(id);
    await this.initGame();
    this.loading = false;
  }

  async createObjetDuJour() {
    this.loading = true;
    if (
      this.nomobjet === '' ||
      this.description === '' ||
      this.debutValidite === '' ||
      this.finValidite === ''
    ) {
      this.error = 'Tous les champs doivent être remplis';
      this.loading = false;
      return;
    }
    console.log('createObjetDuJour');
    console.log('nomobjet', this.nomobjet);
    console.log('date', this.debutValidite, this.finValidite);

    try {
      var obj = await this.gameService.postObject(
        this.nomobjet,
        this.description,
        this.game!.id,
        this.debutValidite,
        this.finValidite
      );
      this.objets.push(obj);
    } catch {
      this.error = "Erreur lors de la création de l'objet";
    }

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
    this.objets = await this.gameService.getObjects(game.id);
    this.objets = this.objets.sort((a, b) => {
      return a.debutValidite < b.debutValidite ? 1 : -1;
    });
    this.game = game;
    this.loading = false;
    console.log('game', this.objets);
  }
  async ngOnInit() {
    this.loading = true;
    // this.postObjet();
    try {
      await this.initGame();
    } catch (err: any) {
      this.error = 'Erreur lors du chargement : ' + err.message;
    }
  }
}
