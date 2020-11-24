import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperatorMenuPageComponent } from './pages/operator-menu-page/operator-menu-page.component';
import { OperatorRoutingModule } from './operator-routing.module';
import { CompaniesPageComponent } from './pages/companies-page/companies-page.component';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import { SharedModule } from 'src/app/shared/shared.module';
import { AdministratorsPageComponent } from './pages/administrators-page/administrators-page.component';

@NgModule({
  imports: [
    CommonModule,
    OperatorRoutingModule,
    MatGridListModule,
    MatCardModule,
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
    SharedModule
  ],
  declarations: [
    OperatorMenuPageComponent,
    CompaniesPageComponent,
    AdministratorsPageComponent
  ]
})
export class OperatorModule { }
