import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Game } from 'src/app/Models/models';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';

@Component({
  selector: 'app-killercard',
  templateUrl: './joinkillercard.component.html',
  styleUrls: ['./joinkillercard.component.scss'],
})
export class JoinkillercardComponent {
  constructor(private router: Router, private gameService: GameService) {}

  @Input() game!: Game;
  @Input() isModerator = false;

  navigateToGame() {
    this.router.navigate(['killer/game', { id: this.game.id }]);
  }
  moderateGame() {
    this.router.navigate(['killer/moderate', { id: this.game.id }]);
  }

  goToGame() {
    if (this.isModerator) {
      this.moderateGame();
    } else {
      this.navigateToGame();
    }
  }
}
