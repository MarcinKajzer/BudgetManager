import { Injectable } from '@angular/core';
import { Observable, Subject, concatMap } from 'rxjs';
import { ExpenseCategory } from '../models/expenseCategory';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {

  apiUrl = 'https://localhost:7261/api';
  private incomes$: Subject<ExpenseCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.incomes$ = new Subject();
  }

  getIncomes(): Observable<ExpenseCategory[]> {
    return this.incomes$ as Observable<ExpenseCategory[]>;
  }

  refreshIncomes(): void {
    this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseTable`).subscribe(incomes => this.incomes$.next(incomes));
  }

  addExpense(categoryId: string, amount: number, date: Date) {
    const payload = {
      categoryId,
      date,
      amount: +amount,
      comment: ''
    }
    this.httpClient.post(`${this.apiUrl}/income/`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/incomeTable`))
      )
      .subscribe(incomes => this.incomes$.next(incomes))
  }

  updateExpense(incomeId: string, amount: number, comment: string) {
    const payload = {
      amount: +amount,
      comment
    }
    this.httpClient.put(`${this.apiUrl}/income/${incomeId}`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/incomeTable`))
      )
      .subscribe(incomes => this.incomes$.next(incomes))
  }
}
