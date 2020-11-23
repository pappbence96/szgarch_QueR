import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagerMenuPageComponent } from './pages/manager-menu-page/manager-menu-page.component';
import { ManagerRoutingModule } from './manager-routing.module';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    ManagerRoutingModule,
    MatGridListModule,
    MatCardModule
  ],
  declarations: [
    ManagerMenuPageComponent
  ]
})
export class ManagerModule { }
