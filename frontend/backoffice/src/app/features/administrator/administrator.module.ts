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
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  imports: [
    CommonModule,
    AdministratorRoutingModule,
    MatGridListModule,
    MatCardModule,
    SharedModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatTableModule,
    MatSortModule,
    FormsModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
  ],
  declarations: [
    AdministratorMenuPageComponent,
    EmployeesPageComponent,
    QueueTypesPageComponent,
    SitesPageComponent
  ]
})
export class AdministratorModule { }
