import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperatorMenuPageComponent } from './pages/operator-menu-page/operator-menu-page.component';
import { OperatorRoutingModule } from './operator-routing.module';

@NgModule({
  imports: [
    CommonModule,
    OperatorRoutingModule
  ],
  declarations: [
    OperatorMenuPageComponent
  ]
})
export class OperatorModule { }
