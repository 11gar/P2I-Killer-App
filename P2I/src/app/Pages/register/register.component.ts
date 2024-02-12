import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  constructor(public router: Router, private authService: AuthService) {}

  login: string = '';
  password: string = '';
  prenom: string = '';
  nom: string = '';

  loading: boolean = false;

  registered: boolean = false;
  error = '';

  ngOnInit() {
    // if (this.authService.isLoggedIn()) this.router.navigate(['killer']);
  }

  async Register() {
    if (
      this.login == '' ||
      this.password == '' ||
      this.prenom == '' ||
      this.nom == ''
    ) {
      this.error = 'Veuillez remplir tous les champs';
      return;
    }
    const used = await this.authService.getByLogin(this.login);
    console.log(used);
    if (used > -1) {
      this.error = 'Ce login est déjà utilisé';
      return;
    }
    console.log(this.login);
    this.loading = true;
    if (
      await this.authService.register(
        this.login,
        this.password,
        this.prenom,
        this.nom
      )
    ) {
      this.registered = true;
      this.error = '';
      this.loading = false;
    } else {
      this.error = "Une erreur s'est produite, merci de réessayer";
      this.registered = true;
      this.loading = false;
    }
  }
}
