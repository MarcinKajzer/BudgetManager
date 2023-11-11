import { Injectable } from "@angular/core";
import { Observable, Subject, concatMap } from "rxjs";
import { ExpenseCategory } from "../models/expense-category.type";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ExpenseCategoriesService {

  apiUrl = environment.apiUrl;

  private categories$: Subject<ExpenseCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.categories$ = new Subject();
  }

  getCategories(): Observable<ExpenseCategory[]> {
    return this.categories$ as Observable<ExpenseCategory[]>;
  }

  refreshCategory(): void {
    this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}).subscribe(categories => this.categories$.next(categories));
  }

  //Podejście z pobieraniem wszystkich kategorii za każdym razem (proste i powtarzalne rozwiązanie), ale nie najlepsze (doładowywać tylko to co się zmieniło)
  addCategory(categoryName: string) {
    const payload = {
      name: categoryName
    }
    this.httpClient.post(`${this.apiUrl}/expenseCategory`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  editCategory(id: string, name: string) {
    const payload = {
      name
    }
    this.httpClient.put(`${this.apiUrl}/expenseCategory/${id}`, payload, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  deleteCategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/expenseCategory/${id}`, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  addSubcategory(name: string, categoryId: string) {
    const payload = {
      name,
      categoryId
    }
    this.httpClient.post(`${this.apiUrl}/expenseSubcategory`, payload, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  editSubcategory(id: string, name: string) {
    const payload = {
      name
    }
    this.httpClient.put(`${this.apiUrl}/expenseSubcategory/${id}`, payload, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  deleteSubcategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/expenseSubcategory/${id}`, {withCredentials: true})
      .pipe(
        concatMap(() => this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true}))
      )
      .subscribe(categories => this.categories$.next(categories))
  }
}