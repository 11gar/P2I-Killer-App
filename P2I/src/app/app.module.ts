import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './Pages/homepage/homepage.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { TopbarComponent } from './Components/topbar/topbar.component';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
import { KillerComponent } from './Pages/killer/killer.component';
import { CreateComponent } from './Pages/create/create.component';
import { JoinComponent } from './Pages/join/join.component';
import { ButtonComponent } from './Components/button/button.component';
import { TextInputComponent } from './Components/text-input/text-input.component';
import { TextBarComponent } from './Components/text-bar/text-bar.component';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    NavbarComponent,
    TopbarComponent,
    LoginComponent,
    RegisterComponent,
    KillerComponent,
    CreateComponent,
    JoinComponent,
    ButtonComponent,
    TextInputComponent,
    TextBarComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
