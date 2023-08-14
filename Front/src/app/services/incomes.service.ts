import { Injectable } from '@angular/core';
import { Observable, Subject, concatMap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IncomeCategory } from '../models/incomeCategory';
import { IncomeTableCategory } from '../models/incomeTableCategory';
import { Income } from '../models/income';

@Injectable({
  providedIn: 'root'
})
export class IncomesService {

  apiUrl = 'https://localhost:7261/api';

  private incomesTable: IncomeTableCategory[]
  private incomesTable$: Subject<IncomeTableCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.incomesTable = [];
    this.incomesTable$ = new Subject();
  }

  getIncomes(): Observable<IncomeTableCategory[]> {
    return this.incomesTable$ as Observable<IncomeTableCategory[]>;
  }

  refreshIncomes(year: number, month: number): void {
    this.httpClient.get<IncomeTableCategory[]>(`${this.apiUrl}/incomeTable?year=${year}&month=${month}`)
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
    this.httpClient.post(`${this.apiUrl}/income/`, payload, { observe: 'response' })
      .pipe(
        concatMap((res: any) => {
          var location = res.headers.get("Location");
          return this.httpClient.get<Income>(location);
        })
      )
      .subscribe((income: Income) => {
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
    this.httpClient.put(`${this.apiUrl}/income/${incomeId}`, payload)
      .subscribe(() => {
        const incomes = this.incomesTable.flatMap(i => i.incomes);
        const income = incomes.find(i => i.id == incomeId);
        income!.amount = +amount;
        income!.comment = comment;
        this.incomesTable$.next(this.incomesTable);
      })
  }
}
