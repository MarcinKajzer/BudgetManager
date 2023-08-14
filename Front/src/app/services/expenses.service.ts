import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, concatMap } from 'rxjs';
import { ExpenseTableCategory } from '../models/expenseTableCategory';
import { Expense } from '../models/expense';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  apiUrl = 'https://localhost:7261/api';

  private expensesTable: ExpenseTableCategory[];
  private expensesTable$: Subject<ExpenseTableCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.expensesTable = [];
    this.expensesTable$ = new Subject();
  }

  getExpenses(): Observable<ExpenseTableCategory[]> {
    return this.expensesTable$ as Observable<ExpenseTableCategory[]>;
  }

  refreshExpenses(year: number, month: number): void {
    this.httpClient.get<ExpenseTableCategory[]>(`${this.apiUrl}/expenseTable?Year=${year}&Month=${month}`)
      .subscribe(expenses => {
        this.expensesTable = expenses;
        this.expensesTable$.next(expenses)
      });
  }

  addExpense(subcategoryId: string, amount: number, date: Date) {
    const payload = {
      subcategoryId,
      date,
      amount: +amount,
      comment: ''
    }
    this.httpClient.post(`${this.apiUrl}/expenses/`, payload, { observe: 'response' })
      .pipe(
        concatMap((res: any) => {
          var location = res.headers.get("Location");
          return this.httpClient.get<Expense>(location);
        })
      )
      .subscribe((expense: Expense) => {
        const subcategory = this.expensesTable.flatMap(e => e.subcategories).find(s => s.id == subcategoryId);
        subcategory!.expenses.push(expense);
        this.expensesTable$.next(this.expensesTable);
      }
    )
  }

  updateExpense(expenseId: string, amount: number, comment: string) {
    const payload = {
      amount: +amount,
      comment
    }
    this.httpClient.put(`${this.apiUrl}/expenses/${expenseId}`, payload)
      .subscribe(() => {
        const expenses = this.expensesTable.flatMap(e => e.subcategories).flatMap(s => s.expenses);
        const expense = expenses.find(e => e.id == expenseId);
        expense!.amount = +amount;
        expense!.comment = comment;
        this.expensesTable$.next(this.expensesTable)
      })
  }
}
