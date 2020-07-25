import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

import { Expense } from '../../../core/models/expense.model';
import { CreateExpense } from '../../../core/models/createexpense.model';
import { UsersService } from '../../../core/services/users/users.service';
import { ExpensesService } from '../../../core/services/expenses/expenses.service';
import { LocalStorageService } from '../../../core/services/localstorage/localstorage.service';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {

  token: string;  
  userName: string;
  expense: Expense;
  createUser: CreateExpense;

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    private expensesService: ExpensesService,
    private localStorageService: LocalStorageService) { }

  ngOnInit() {
    this.token = this.getToken();
    this.userName = this.getUser().userName;
  }

  addExpenseForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    value: new FormControl(''),
    expenseType: new FormControl(''),
    userNameOwner: new FormControl(''),
    expirationDate: new FormControl(''),
    expenseStatus: new FormControl(''),
    expenseRecurrenceType: new FormControl('')
  });


  getToken() {
    return this.localStorageService.get('expenses_token');
  }

  getUser() {
    return this.usersService.decodeToken(this.token);
  }


  submit() {
    console.log('create expense');
    const createExpense: CreateExpense = {
      name: this.addExpenseForm.get('name').value,
      description: this.addExpenseForm.get('description').value,
      value: this.addExpenseForm.get('value').value,
      idExpenseType: this.addExpenseForm.get('expenseType').value,
      userNameOwner: this.userName,
      expirationDate: this.addExpenseForm.get('expirationDate').value,
      idExpenseStatus: this.addExpenseForm.get('expenseStatus').value,
      idExpenseRecurrenceType: this.addExpenseForm.get('expenseRecurrenceType').value
    };
    console.log(createExpense);
    this.expensesService.createExpenses(createExpense, this.token)
      .subscribe(expenseCreated => {
        console.log(expenseCreated);
      });
  }
}
