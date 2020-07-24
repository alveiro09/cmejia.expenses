import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ExpensesComponent } from './components/expenses/expenses.component';

import { MaterialModule } from './../material/material.module';
import { ExpensesRoutingModule } from './expenses-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ExpenseComponent } from './components/expense/expense.component';
import { ExpenseListComponent } from './components/expense-list/expense-list.component';

@NgModule({
    declarations: [
        ExpensesComponent,
        ExpenseComponent,
        ExpenseListComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        ExpensesRoutingModule,
        ReactiveFormsModule,
        MaterialModule
    ]
})
export class ExpensesModule {

}
