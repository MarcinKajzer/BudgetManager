import { Injectable } from '@angular/core';
import { IncomeCategory } from '../types/income-category.type';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IncomeCategoriesService {

  apiUrl = environment.apiUrl;

  private categories: IncomeCategory[];
  private categories$: Subject<IncomeCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.categories = [];
    this.categories$ = new Subject();
  }

  getCategories(): Observable<IncomeCategory[]> {
    return this.categories$ as Observable<IncomeCategory[]>;
  }

  refreshCategory(): void {
    this.httpClient.get<IncomeCategory[]>(`${this.apiUrl}/IncomeCategory`, {withCredentials: true})
      .subscribe(categories => {
        this.categories = categories;
        this.categories$.next(categories);
      }
    );   
  }

  addCategory(categoryName: string) {
    const payload = {
      name: categoryName
    }

    this.httpClient.post<string>(`${this.apiUrl}/IncomeCategory`, payload, {withCredentials: true})
      .subscribe((categoryId: string) => {
        const category: IncomeCategory = {
          id: categoryId,
          name: categoryName
        }

        this.categories.push(category);
        this.categories$.next(this.categories);
      }
    );
  }

  editCategory(id: string, name: string) {
    const payload = {
      name
    }

    this.httpClient.put(`${this.apiUrl}/IncomeCategory/${id}`, payload, {withCredentials: true})
      .subscribe(() => {
        const category = this.categories.find(c => c.id == id);
        category!.name = name;
        this.categories$.next(this.categories);
      }
    )
  }

  deleteCategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/IncomeCategory/${id}`, {withCredentials: true})
      .subscribe(() => {
        this.categories = this.categories.filter(c => c.id != id);
        this.categories$.next(this.categories);
      }
    )
  }

}
