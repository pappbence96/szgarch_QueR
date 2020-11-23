import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/login/login.component';

import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule
  ],
  declarations: [
    HeaderComponent,
    LoginComponent
  ],
  exports: [
    HeaderComponent,
    LoginComponent
  ]
})
export class CoreModule { }
