import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeWorkComponent } from './pages/employee-work/employee-work.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: EmployeeWorkComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EmployeeRoutingModule { }
