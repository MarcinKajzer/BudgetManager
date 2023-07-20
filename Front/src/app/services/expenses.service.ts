import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { Observable, Subject, concatMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  apiUrl = 'https://localhost:7261/api';
  private expenses$: Subject<Category[]>;

  constructor(private httpClient: HttpClient) {
    this.expenses$ = new Subject();
  }

  getExpenses(): Observable<Category[]> {
    return this.expenses$ as Observable<Category[]>;
  }

  refreshExpenses(): void {
    this.httpClient.get<Category[]>(`${this.apiUrl}/expenseTable`).subscribe(expenses => this.expenses$.next(expenses));
  }

  addExpense(subcategoryId: string, amount: number, date: Date) {
    const payload = {
        subcategoryId,
        date,
        amount: +amount,
        comment: ''
    }
    this.httpClient.post(`${this.apiUrl}/expenses/`, payload)
        .pipe(
            concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseTable`))
        )
        .subscribe(expenses => this.expenses$.next(expenses))
  }

  updateExpense(expenseId: string, amount: number, comment: string){
    const payload = {
        amount: +amount,
        comment
    }
    this.httpClient.put(`${this.apiUrl}/expenses/${expenseId}`, payload)
        .pipe(
            concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseTable`))
        )
        .subscribe(expenses => this.expenses$.next(expenses))
  }
}
