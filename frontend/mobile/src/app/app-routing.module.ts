import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewTicketComponent } from './components/new-ticket/new-ticket.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { MenuPageComponent } from './pages/menu-page/menu-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { AuthGuard } from './utilities/AuthGuard';


const routes: Routes = [
  {
    path: 'login',
    component: LoginPageComponent,
  },
  {
    path: 'register',
    component: RegisterPageComponent,
  },
  {
    path: 'new',
    pathMatch: 'full',
    component: NewTicketComponent,
    canActivate: [AuthGuard]
  },
  {
    path: '',
    component: MenuPageComponent,
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
