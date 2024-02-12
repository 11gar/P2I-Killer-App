import { Component } from '@angular/core';
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

  constructor(private gameService: GameService) {}
  async ngOnInit() {
    console.log(await this.gameService.getAllGames());
  }

  async createGame() {
    this.loading = true;
    var resp = await this.gameService.createGame(this.nom, this.mdp);
    if (resp != null) {
      this.idcreated = this.formatId(resp.id).toString();
      this.error = '';
      this.loading = false;
    } else {
      this.error = "Une erreur s'est produite, veuillez réessayer";
      this.loading = false;
    }
  }

  formatId(id: number) {
    return id.toString().padStart(5, '0');
  }
}
