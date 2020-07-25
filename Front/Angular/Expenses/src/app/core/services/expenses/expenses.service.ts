import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

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

  getExpensesById(ExpensesId: string) {
    console.log(ExpensesId);
    return this.http.get<Expense[]>(`${environment.urlExpenses}/Expense/id?id=${ExpensesId}`);
  }

  getExpensesByOwnerName(userNameOwner: string, token: string) {
    const headers = this.setHeaders(token);
    return this.http.get<Expense[]>(`${environment.urlExpenses}/Expense/userNameOwner?userNameOwner=${userNameOwner}`,
      { headers: headers });
  }

  createExpenses(createExpense: CreateExpense, token: string) {
    const headers = this.setHeaders(token);
    console.log(createExpense);
    return this.http.post<Expense>(`${environment.urlExpenses}/Expense`, createExpense,
      { headers: headers });
  }

  updateExpenses(ExpensesId: string, patchDto: PatchDto[]) {
    console.log(ExpensesId);
    return this.http.patch<Expense>(`${environment.urlExpenses}/Expense${ExpensesId}`, patchDto);
  }

  setHeaders(token: string) {
    var headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + JSON.parse(token)
    });
    return headers;
  }
}
