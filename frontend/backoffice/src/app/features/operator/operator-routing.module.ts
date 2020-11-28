import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorMenuPageComponent } from '../administrator/pages/administrator-menu-page/administrator-menu-page.component';
import { AdministratorsPageComponent } from './pages/administrators-page/administrators-page.component';
import { CompaniesPageComponent } from './pages/companies-page/companies-page.component';
import { OperatorMenuPageComponent } from './pages/operator-menu-page/operator-menu-page.component';
import { StatisticsPageComponent } from './pages/statistics-page/statistics-page.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: OperatorMenuPageComponent
  },
  {
    path: 'companies',
    component: CompaniesPageComponent
  },
  {
    path: 'administrators',
    component: AdministratorsPageComponent
  },
  {
    path: 'statistics',
    component: StatisticsPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OperatorRoutingModule { }
