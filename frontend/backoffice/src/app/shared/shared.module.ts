import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';
import { SiteTitleComponent } from './components/site-title/site-title.component';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule
  ],
  exports: [
    ConfirmDialogComponent,
    SiteTitleComponent
  ],
  declarations: [
    ConfirmDialogComponent,
    SiteTitleComponent
  ]
})
export class SharedModule { }
