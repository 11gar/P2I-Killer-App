import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent {
  nom = '';
  mdp = '';
  loading = false;
  idcreated = '';
  error = '';

  constructor(
    private gameService: GameService,
    private authService: AuthService,
    private router: Router
  ) {}
  async ngOnInit() {
    console.log(await this.gameService.getAllGames());
  }

  async createGame() {
    this.loading = true;
    var resp = await this.gameService.createGame(
      this.nom,
      this.authService.getLoggedUserId()
    );
    this.gameService.moderateGame(resp.id, this.authService.getLoggedUserId());
    // this.router.navigate(['killer/moderate', { id: resp.id }]);
    if (resp != null) {
      this.idcreated = this.formatId(resp.id).toString();
      this.error = '';
      this.loading = false;
    } else {
      this.error = "Une erreur s'est produite, veuillez réessayer";
      this.loading = false;
    }
  }

  goToModerate() {
    this.router.navigate(['killer/moderate', { id: this.idcreated }]);
  }

  formatId(id: number) {
    return id.toString().padStart(5, '0');
  }
}
