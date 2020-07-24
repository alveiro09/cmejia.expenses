import { Component, OnInit } from '@angular/core';

import { Expense } from '../../../../app/core/models/expense.model';

import { ExpensesService } from './../../../core/services/expenses/expenses.service';
@Component({
  selector: 'app-expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.css']
})
export class ExpenseListComponent implements OnInit {

  expensesList: Expense[] = [];

  constructor(private expensesService: ExpensesService) { }

  ngOnInit() {
    this.getExpesnes();
  }

  getExpesnes() {
    this.expensesService.getExpenses()
      .subscribe(expenses => {
        this.expensesList = expenses;
      });
  }

   clickExpense(id: string) {
    console.log('product');
  } 


}
