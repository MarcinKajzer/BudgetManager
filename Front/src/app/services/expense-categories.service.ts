import { Injectable } from "@angular/core";
import { Observable, Subject, concatMap } from "rxjs";
import { Category } from "../models/category";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  apiUrl = 'https://localhost:7261/api'

  private categories$: Subject<Category[]>;

  constructor(private httpClient: HttpClient) {
    this.categories$ = new Subject();
  }

  getCategories(): Observable<Category[]> {
    return this.categories$ as Observable<Category[]>;
  }

  refreshCategory(): void {
    this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`).subscribe(categories => this.categories$.next(categories));
  }

  //Podejście z pobieraniem wszystkich kategorii za każdym razem (proste i powtarzalne rozwiązanie), ale nie najlepsze (doładowywać tylko to co się zmieniło)
  addCategory(categoryName: string) {
    const payload = {
      name: categoryName
    }
    this.httpClient.post(`${this.apiUrl}/expenseCategory`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  editCategory(id: string, name: string) {
    const payload = {
      name
    }
    this.httpClient.put(`${this.apiUrl}/expenseCategory/${id}`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  deleteCategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/expenseCategory/${id}`)
      .pipe(
        concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  addSubcategory(name: string, categoryId: string) {
    const payload = {
      name,
      categoryId
    }
    this.httpClient.post(`${this.apiUrl}/expenseSubcategory`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  editSubcategory(id: string, name: string) {
    const payload = {
      name
    }
    this.httpClient.put(`${this.apiUrl}/expenseSubcategory/${id}`, payload)
      .pipe(
        concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`))
      )
      .subscribe(categories => this.categories$.next(categories))
  }

  deleteSubcategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/expenseSubcategory/${id}`)
      .pipe(
        concatMap(() => this.httpClient.get<Category[]>(`${this.apiUrl}/expenseCategory`))
      )
      .subscribe(categories => this.categories$.next(categories))
  }
}