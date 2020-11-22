import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagerMenuPageComponent } from './pages/manager-menu-page/manager-menu-page.component';
import { ManagerRoutingModule } from './manager-routing.module';

@NgModule({
  imports: [
    CommonModule,
    ManagerRoutingModule
  ],
  declarations: [
    ManagerMenuPageComponent
  ]
})
export class ManagerModule { }
