import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorMenuPageComponent } from './pages/administrator-menu-page/administrator-menu-page.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AdministratorMenuPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministratorRoutingModule { }
