import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ManagerMenuPageComponent } from './pages/manager-menu-page/manager-menu-page.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ManagerMenuPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagerRoutingModule { }
