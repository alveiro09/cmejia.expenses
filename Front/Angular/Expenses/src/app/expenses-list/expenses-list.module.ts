import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


import { MaterialModule } from './../material/material.module';
import { ExpenseListRoutingModule } from './expenses-list-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ExpenseComponent } from './components/expense/expense.component';
import { ExpenseListComponent } from './components/expense-list/expense-list.component';

@NgModule({
    declarations: [
        ExpenseComponent,
        ExpenseListComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        ExpenseListRoutingModule,
        ReactiveFormsModule,
        MaterialModule
    ]
})
export class ExpensesListModule {

}
