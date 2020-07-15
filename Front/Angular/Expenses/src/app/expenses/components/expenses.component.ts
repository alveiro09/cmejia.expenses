import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {

  constructor() { }

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
    console.log('user added');
  }
}
