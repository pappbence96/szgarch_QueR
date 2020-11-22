import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OperatorMenuPageComponent } from './pages/operator-menu-page/operator-menu-page.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: OperatorMenuPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OperatorRoutingModule { }
