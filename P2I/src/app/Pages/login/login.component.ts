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
    // if (this.authService.isLoggedIn()) this.router.navigate(['killer']);
  }

  async Login() {
    if (this.login == '' || this.password == '') {
      this.error = 'Veuillez remplir tous les champs';
      return;
    }
    console.log(this.login);
    this.loading = true;
    setTimeout(() => {
      this.error = 'Temps de réponse trop long, veuillez réessayer';
      this.loading = false;
      this.logged = false;
    }, 5000);
    if (await this.authService.login(this.login, this.password)) {
      this.logged = true;
      this.error = '';
      this.loading = false;
    } else {
      this.error = 'Utilisateur ou mot de passe incorrect';
      this.logged = false;
      this.loading = false;
    }
    // this.router.navigate(['killer']);
  }
}
