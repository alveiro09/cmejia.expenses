import {
  Component, Input, Output, EventEmitter,
  OnChanges, SimpleChange, OnInit, DoCheck, OnDestroy
} from '@angular/core';

import { Expense } from '../../../core/models/expense.model';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.css']
})
export class ExpenseComponent implements OnInit, DoCheck, OnDestroy {
  @Input() expense: Expense;
  @Output() expenseClicked: EventEmitter<any> = new EventEmitter(true);
  constructor() { }

  ngOnInit() {
  }

  ngDoCheck() {
  }

  ngOnDestroy() {
  }
}
