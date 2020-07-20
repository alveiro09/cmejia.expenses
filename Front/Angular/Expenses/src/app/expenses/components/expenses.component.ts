import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

import { Expense } from '../../core/models/expense.model';
import { CreateExpense } from '../../core/models/createexpense.model';
import { ExpensesService } from '../../core/services/expenses/expenses.service';
import { LocalStorageService } from '../../core/services/localstorage/localstorage.service';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
  expense: Expense;
  createUser: CreateExpense;

  constructor(
    private route: ActivatedRoute,
    private expensesService: ExpensesService,
    private localStorageService: LocalStorageService) { }

  ngOnInit() {
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

  submit() {
    console.log('create expense');
    const createExpense: CreateExpense = {
      name: this.addExpenseForm.get('email').value,
      description: this.addExpenseForm.get('description').value,
      value: this.addExpenseForm.get('value').value,
      idExpenseType: this.addExpenseForm.get('expenseType').value,
      userNameOwner: this.addExpenseForm.get('userNameOwner').value,
      expirationDate: this.addExpenseForm.get('expirationDate').value,
      idExpenseStatus: this.addExpenseForm.get('expenseStatus').value,
      idExpenseRecurrenceType: this.addExpenseForm.get('expenseRecurrenceType').value
    };
    this.expensesService.createExpenses(createExpense)
      .subscribe(expenseCreated => {
        console.log(expenseCreated);
      });
  }
}
