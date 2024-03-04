import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  constructor(public router: Router, private authService: AuthService) {}

  login: string = '';
  password: string = '';

  loading: boolean = false;
  error = '';
  logged: boolean = false;

  ngOnInit() {
    this.Load();
  }

  async Load() {
    this.logged = this.authService.isLoggedIn();
  }

  async Login() {
    if (this.login == '' || this.password == '') {
      this.error = 'Veuillez remplir tous les champs';
      return;
    }
    console.log(this.login);
    this.loading = true;
    var to = setTimeout(() => {
      this.error = 'Temps de réponse trop long, veuillez réessayer';
      this.loading = false;
      this.logged = false;
      return;
    }, 5000);
    if (await this.authService.login(this.login, this.password)) {
      this.logged = true;
      this.error = '';
      this.loading = false;
      clearTimeout(to);
    } else {
      this.error = 'Utilisateur ou mot de passe incorrect';
      this.logged = false;
      this.loading = false;
      clearTimeout(to);
    }
    // this.router.navigate(['killer']);
  }
  async Logout() {
    await this.authService.logout();
    this.Load();
  }
}
