import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdministratorMenuPageComponent } from './pages/administrator-menu-page/administrator-menu-page.component';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    AdministratorRoutingModule,
    MatGridListModule,
    MatCardModule
  ],
  declarations: [
    AdministratorMenuPageComponent
  ]
})
export class AdministratorModule { }
