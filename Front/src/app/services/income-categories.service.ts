import { Injectable } from '@angular/core';
import { IncomeCategory } from '../types/income-category.type';
import { Observable, Subject, concatMap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IncomeCategoriesService {

  apiUrl = environment.apiUrl;

  private categories$: Subject<IncomeCategory[]>;

   constructor(private httpClient: HttpClient) {
    this.categories$ = new Subject();
  }

  getCategories(): Observable<IncomeCategory[]> {
    return this.categories$ as Observable<IncomeCategory[]>;
  }

  refreshCategory(): void {
    this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/IncomeCategory`, {withCredentials: true}).subscribe(categories => this.categories$.next(categories));
  }

  //Podejście z pobieraniem wszystkich kategorii za każdym razem (proste i powtarzalne rozwiązanie), ale nie najlepsze (doładowywać tylko to co się zmieniło)
  addCategory(categoryName: string) {
    const payload = {
      name: categoryName
    }
    this.httpClient.post(`${this.apiUrl}/IncomeCategory`, payload, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/IncomeCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  editCategory(id: string, name: string) {
    const payload = {
      name
    }
    this.httpClient.put(`${this.apiUrl}/IncomeCategory/${id}`, payload, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/IncomeCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  deleteCategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/IncomeCategory/${id}`)
      .pipe(
        concatMap(() => this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/IncomeCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }
}
