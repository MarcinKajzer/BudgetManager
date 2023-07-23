import { Injectable } from '@angular/core';
import { Observable, Subject, concatMap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IncomeCategory } from '../models/incomeCategory';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {

  apiUrl = 'https://localhost:7261/api';
  private incomes$: Subject<IncomeCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.incomes$ = new Subject();
  }

  getIncomes(): Observable<IncomeCategory[]> {
    return this.incomes$ as Observable<IncomeCategory[]>;
  }

  refreshIncomes(): void {
    this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/incomeTable`).subscribe(incomes => this.incomes$.next(incomes));
  }

  addIncome(categoryId: string, amount: number, date: Date) {
    const payload = {
      categoryId,
      date,
      amount: +amount,
      comment: ''
    }
    this.httpClient.post(`${this.apiUrl}/income/`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/incomeTable`))
      )
      .subscribe(incomes => this.incomes$.next(incomes))
  }

  updateIncome(incomeId: string, amount: number, comment: string) {
    const payload = {
      amount: +amount,
      comment
    }
    this.httpClient.put(`${this.apiUrl}/income/${incomeId}`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/incomeTable`))
      )
      .subscribe(incomes => this.incomes$.next(incomes))
  }
}
