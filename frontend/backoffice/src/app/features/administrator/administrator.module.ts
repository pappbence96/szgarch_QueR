import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdministratorMenuPageComponent } from './pages/administrator-menu-page/administrator-menu-page.component';

@NgModule({
  imports: [
    CommonModule,
    AdministratorRoutingModule
  ],
  declarations: [
    AdministratorMenuPageComponent
  ]
})
export class AdministratorModule { }
