import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdministratorMenuPageComponent } from './pages/administrator-menu-page/administrator-menu-page.component';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import { EmployeesPageComponent } from './pages/employees-page/employees-page.component';
import { QueueTypesPageComponent } from './pages/queue-types-page/queue-types-page.component';
import { SitesPageComponent } from './pages/sites-page/sites-page.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    AdministratorRoutingModule,
    MatGridListModule,
    MatCardModule,
    SharedModule
  ],
  declarations: [
    AdministratorMenuPageComponent,
    EmployeesPageComponent,
    QueueTypesPageComponent,
    SitesPageComponent
  ]
})
export class AdministratorModule { }
