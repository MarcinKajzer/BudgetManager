import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, concatMap, tap } from 'rxjs';
import { ExpenseTableCategory } from '../types/expense-table-category.type';
import { Expense } from '../types/expense.type';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  apiUrl = environment.apiUrl;

  private expensesTable: ExpenseTableCategory[];
  private expensesTable$: Subject<ExpenseTableCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.expensesTable = [];
    this.expensesTable$ = new Subject();
  }

  getExpenses(): Observable<ExpenseTableCategory[]> {
    return this.expensesTable$ as Observable<ExpenseTableCategory[]>;
  }

  refreshExpenses(year: number, month: number): Observable<any> {
    return this.httpClient.get<ExpenseTableCategory[]>(`${this.apiUrl}/expenseTable?Year=${year}&Month=${month}`, {withCredentials: true})
      .pipe(
        tap(expenses => {
          this.expensesTable = expenses;
          this.expensesTable$.next(expenses);
        })
      );
  }

  addExpense(subcategoryId: string, amount: number, date: Date): Observable<string> {
    const payload = {
      subcategoryId,
      date,
      amount,
      comment: ''
    }
    
    return this.httpClient.post<string>(`${this.apiUrl}/expenses/`, payload, { withCredentials: true })
      .pipe(
        tap((id: string) => {
          const expense: Expense = {...payload, id};
          
          const subcategory = this.expensesTable.flatMap(e => e.subcategories).find(s => s.id == subcategoryId);
          subcategory!.expenses.push(expense);
          this.expensesTable$.next(this.expensesTable);
        })
      )  
  }

  deleteExpense(id: string) {
    
  }

  updateExpense(exp: Expense) {
    this.httpClient.put(`${this.apiUrl}/expenses/${exp.id}`, exp, {withCredentials: true})
      .subscribe(() => {
        const expenses = this.expensesTable.flatMap(e => e.subcategories).flatMap(s => s.expenses);
        const expense = expenses.find(e => e.id == exp.id);
        expense!.amount = exp.amount;
        expense!.comment = exp.comment;
        this.expensesTable$.next(this.expensesTable)
      })
  }
}
