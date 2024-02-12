import { Component } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { GameService } from 'src/app/Services/game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-join',
  templateUrl: './join.component.html',
  styleUrls: ['./join.component.scss'],
})
export class JoinComponent {
  id = '';
  loading = false;
  error = '';

  constructor(
    private gameService: GameService,
    private authService: AuthService,
    private router: Router
  ) {}
  async ngOnInit() {
    console.log(await this.gameService.getAllGames());
  }

  async joinGame() {
    console.log(localStorage.getItem('loggedUserId'));
    this.loading = true;
    var id = parseInt(this.id);
    if (this.authService.getLoggedUserId() > 0) {
      try {
        await this.gameService.joinGame(id, this.authService.getLoggedUserId());
      } catch (error: any) {
        console.log(error.message);
        this.error = error.message;
        this.loading = false;
        return;
      }
    } else {
      console.log('not logged');
    }
    this.loading = false;
    this.router.navigate(['/game', id]);
  }

  formatId(id: number) {
    return id.toString().padStart(5, '0');
  }
}
