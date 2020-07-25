import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ExpensesComponent } from './components/expenses/expenses.component';

import { MaterialModule } from './../material/material.module';
import { ExpensesRoutingModule } from './expenses-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [
        ExpensesComponent,
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
