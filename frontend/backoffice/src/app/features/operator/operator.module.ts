import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperatorMenuPageComponent } from './pages/operator-menu-page/operator-menu-page.component';
import { OperatorRoutingModule } from './operator-routing.module';

import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    OperatorRoutingModule,
    MatGridListModule,
    MatCardModule
  ],
  declarations: [
    OperatorMenuPageComponent
  ]
})
export class OperatorModule { }
