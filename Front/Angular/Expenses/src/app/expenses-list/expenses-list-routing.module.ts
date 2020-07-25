import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { ExpenseListComponent } from './components/expense-list/expense-list.component';

const routes: Routes = [
    {
        path: '',
        component: ExpenseListComponent
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
export class ExpenseListRoutingModule { }
