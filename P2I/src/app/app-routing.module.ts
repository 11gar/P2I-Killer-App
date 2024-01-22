import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './Pages/homepage/homepage.component';
import { KillerComponent } from './Pages/killer/killer.component';
import { LoginComponent } from './Pages/login/login.component';
import { CreateComponent } from './Pages/create/create.component';
import { JoinComponent } from './Pages/join/join.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomepageComponent,
  },
  {
    path: '1',
    pathMatch: 'full',
    component: HomepageComponent,
  },
  {
    path: '2',
    pathMatch: 'full',
    component: KillerComponent,
  },
  {
    path: '3',
    pathMatch: 'full',
    component: LoginComponent,
  },
  {
    path: 'create',
    pathMatch: 'full',
    component: CreateComponent,
  },

  {
    path: 'join',
    pathMatch: 'full',
    component: JoinComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
