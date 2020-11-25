import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorMenuPageComponent } from './pages/administrator-menu-page/administrator-menu-page.component';
import { EmployeesPageComponent } from './pages/employees-page/employees-page.component';
import { QueueTypesPageComponent } from './pages/queue-types-page/queue-types-page.component';
import { SitesPageComponent } from './pages/sites-page/sites-page.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AdministratorMenuPageComponent
  },
  {
    path: 'sites',
    component: SitesPageComponent
  },
  {
    path: 'employees',
    component: EmployeesPageComponent
  },
  {
    path: 'queuetypes',
    component: QueueTypesPageComponent
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministratorRoutingModule { }
