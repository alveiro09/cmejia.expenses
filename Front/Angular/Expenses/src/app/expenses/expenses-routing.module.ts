import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { ExpensesComponent } from './components/expenses/expenses.component';

const routes: Routes = [
    {
        path: '',
        component: ExpensesComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    exports: [
        RouterModule
    ]
})
export class ExpensesRoutingModule { }
