import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { AuthGuard } from './shared/utilities/AuthGuard';


const routes: Routes = [
  {
    path: 'operator',
    loadChildren: () => import('./features/operator/operator.module').then(m => m.OperatorModule),
    canActivate: [AuthGuard], data: {role: 'operator'}
  },
  {
    path: 'administrator',
    loadChildren: () => import('./features/administrator/administrator.module').then(m => m.AdministratorModule),
    canActivate: [AuthGuard], data: {role: 'administrator'}
  },
  {
    path: 'manager',
    loadChildren: () => import('./features/manager/manager.module').then(m => m.ManagerModule),
    canActivate: [AuthGuard], data: {role: 'manager'}
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
