import { Component, OnInit } from '@angular/core';

import { Expense } from '../../../../app/core/models/expense.model';

import { LocalStorageService } from './../../../core/services/localstorage/localstorage.service';
import { ExpensesService } from './../../../core/services/expenses/expenses.service';
import { UsersService } from '../../../core/services/users/users.service';

@Component({
  selector: 'app-expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.css']
})
export class ExpenseListComponent implements OnInit {
  userName: string;
  token: string;
  expensesList: Expense[] = [];

  constructor(
    private localStorageService: LocalStorageService,
    private expensesService: ExpensesService,
    private usersService: UsersService) { }

  ngOnInit() {    
    this.token =this.getToken();
    this.userName = this.getUser().userName;
    this.getExpenses();
  }

  getExpenses() {
    this.expensesService.getExpensesByOwnerName(this.userName, this.token)
      .subscribe(expenses => {
        this.expensesList = null;
        this.expensesList = expenses;
      });
  }

  getToken() {
    return this.localStorageService.get('expenses_token');
  }

  getUser() {
    return this.usersService.decodeToken(this.token);
  }

  clickExpense(id: string) {
    console.log(id);
  }
}
