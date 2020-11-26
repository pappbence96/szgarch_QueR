import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagerMenuPageComponent } from './pages/manager-menu-page/manager-menu-page.component';
import { ManagerRoutingModule } from './manager-routing.module';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import { SharedModule } from 'src/app/shared/shared.module';
import { QueuesPageComponent } from './pages/queues-page/queues-page.component';
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
    ManagerRoutingModule,
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
    ManagerMenuPageComponent,
    QueuesPageComponent
  ]
})
export class ManagerModule { }
