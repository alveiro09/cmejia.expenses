import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { CreateExpense } from './../../models/createexpense.model';
import { PatchDto } from './../../models/patchdto.model';

import { environment } from './../../../../environments/environment';
import { Expense } from '../../models/expense.model';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  constructor(private http: HttpClient) {
  }
  getExpenses() {
    return this.http.get<Expense[]>(`${environment.urlExpenses}/Expense`);
  }

  getExpenseById(ExpensesId: string) {
    console.log(ExpensesId);
    return this.http.get<Expense>(`${environment.urlExpenses}/Expense/id?id=${ExpensesId}`);
  }

  getExpenseByOwnerName(userNameOwner: string) {
    console.log(userNameOwner);
    return this.http.get<Expense>(`${environment.urlExpenses}/Expense/userNameOwner${userNameOwner}`);
  }

  createExpenses(createExpense: CreateExpense) {
    console.log(createExpense);
    return this.http.post<Expense>(`${environment.urlExpenses}/Expense`, createExpense);
  }

  updateExpenses(ExpensesId: string, patchDto: PatchDto[]) {
    console.log(ExpensesId);
    return this.http.patch<Expense>(`${environment.urlExpenses}/Expense${ExpensesId}`, patchDto);
  }
}
