import { Injectable } from '@angular/core';
import { Observable, Subject, concatMap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IncomeTableCategory } from '../types/income-table-category.type';
import { Income } from '../types/income.type';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {

  apiUrl = environment.apiUrl;

  private incomesTable: IncomeTableCategory[];
  private incomesTable$: Subject<IncomeTableCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.incomesTable = [];
    this.incomesTable$ = new Subject();
  }

  getIncomes(): Observable<IncomeTableCategory[]> {
    return this.incomesTable$ as Observable<IncomeTableCategory[]>;
  }

  refreshIncomes(year: number, month: number): void {
    this.httpClient.get<IncomeTableCategory[]>(`${this.apiUrl}/incomeTable?year=${year}&month=${month}`, {withCredentials: true})
    .subscribe(incomes => {
      this.incomesTable = incomes;
      this.incomesTable$.next(incomes);
    });
  }

  addIncome(categoryId: string, amount: number, date: Date) {
    const payload = {
      categoryId,
      date,
      amount: +amount,
      comment: ''
    }

    this.httpClient.post<string>(`${this.apiUrl}/income/`, payload, { withCredentials: true })
      .subscribe((id: string) => {
        const income = {...payload, id} as Income;
        const subcategory = this.incomesTable.find(c => c.id == categoryId);
        subcategory!.incomes.push(income);
        this.incomesTable$.next(this.incomesTable);
      })
  }

  updateIncome(incomeId: string, amount: number, comment: string) {
    const payload = {
      amount: +amount,
      comment
    }
    
    this.httpClient.put(`${this.apiUrl}/income/${incomeId}`, payload, {withCredentials: true})
      .subscribe(() => {
        const incomes = this.incomesTable.flatMap(i => i.incomes);
        const income = incomes.find(i => i.id == incomeId);
        income!.amount = +amount;
        income!.comment = comment;
        this.incomesTable$.next(this.incomesTable);
      })
  }
}
