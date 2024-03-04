import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './Pages/homepage/homepage.component';
import { KillerComponent } from './Pages/killer/killer.component';
import { LoginComponent } from './Pages/login/login.component';
import { CreateComponent } from './Pages/create/create.component';
import { JoinComponent } from './Pages/join/join.component';
import { authGuard } from './auth.guard';
import { RegisterComponent } from './Pages/register/register.component';
import { OnstartComponent } from './Pages/onstart/onstart.component';
import { ModerateComponent } from './Pages/moderate/moderate.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomepageComponent,
  },
  {
    path: 'home',
    pathMatch: 'full',
    component: HomepageComponent,
  },
  {
    path: 'killer/game',
    //canActivate: [authGuard],
    component: KillerComponent,
  },
  {
    path: 'login',
    pathMatch: 'full',
    component: LoginComponent,
  },
  {
    path: 'killer/create',
    pathMatch: 'full',
    //canActivate: [authGuard],
    component: CreateComponent,
  },

  {
    path: 'killer/join',
    pathMatch: 'full',
    // canActivate: [authGuard],
    component: JoinComponent,
  },
  {
    path: 'killer',
    pathMatch: 'full',
    // canActivate: [authGuard],
    component: JoinComponent,
  },

  {
    path: 'login/register',
    pathMatch: 'full',
    component: RegisterComponent,
  },
  {
    path: 'killer/moderate',
    pathMatch: 'full',
    component: ModerateComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
