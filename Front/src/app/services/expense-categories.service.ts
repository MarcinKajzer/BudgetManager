import { Injectable } from "@angular/core";
import { Observable, Subject, concatMap } from "rxjs";
import { ExpenseCategory } from "../types/expense-category.type";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { ExpenseSubcategory } from "../types/expense-subcategory.type";

@Injectable({
  providedIn: 'root'
})
export class ExpenseCategoriesService {

  apiUrl = environment.apiUrl;

  private categories: ExpenseCategory[];
  private categories$: Subject<ExpenseCategory[]>;

  constructor(private httpClient: HttpClient) {
    this.categories = [];
    this.categories$ = new Subject();
  }

  getCategories(): Observable<ExpenseCategory[]> {
    return this.categories$ as Observable<ExpenseCategory[]>;
  }

  refreshCategory(): void {
    this.httpClient.get<ExpenseCategory[]>(`${this.apiUrl}/expenseCategory`, {withCredentials: true})
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

    this.httpClient.post<string>(`${this.apiUrl}/expenseCategory`, payload)
      .subscribe((categoryId: string) => {
        const category: ExpenseCategory = {
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

    this.httpClient.put(`${this.apiUrl}/expenseCategory/${id}`, payload, {withCredentials: true})
      .subscribe(() => {
        this.recreateCategoryObj(id, (category) => {
          category.name = name;
        })
        this.categories$.next(this.categories); 
      }
    );
  }

  deleteCategory(id: string) {
    this.httpClient.delete(`${this.apiUrl}/expenseCategory/${id}`, {withCredentials: true})
      .subscribe(() => {
        this.categories = this.categories.filter(c => c.id != id);
        this.categories$.next(this.categories);
      }
    )
  }

  addSubcategory(name: string, categoryId: string) {
    const payload = {
      name,
      categoryId
    }

    this.httpClient.post<string>(`${this.apiUrl}/expenseSubcategory`, payload, {withCredentials: true})
      .subscribe((subcategoryId: string) => {
        const subcategory: ExpenseSubcategory = {
          id: subcategoryId,
          name: name
        };

        this.categories.find(c => c.id == categoryId)!.subcategories!.push(subcategory);
        this.categories$.next(this.categories);
      }
    );
  }

  editSubcategory(categoryId: string, subcategoryId: string, name: string) {
    const payload = {
      name
    }

    this.httpClient.put(`${this.apiUrl}/expenseSubcategory/${subcategoryId}`, payload, {withCredentials: true})
      .subscribe(() => {
        this.recreateCategoryObj(categoryId, (category) => {
          category.subcategories!.find(sc => sc.id == subcategoryId)!.name = name;
        });
        this.categories$.next(this.categories); 
      }) 
  }

  deleteSubcategory(categoryId: string, subcategoryId: string) {
    this.httpClient.delete(`${this.apiUrl}/expenseSubcategory/${subcategoryId}`, {withCredentials: true})
      .subscribe(() => {
        this.recreateCategoryObj(categoryId, (category) => {
          category.subcategories = category.subcategories!.filter(sc => sc.id != subcategoryId);
        });
        this.categories$.next(this.categories); 
      })
  }

  private recreateCategoryObj(categoryId: string, updateCallback: (category: ExpenseCategory) => void) {
    const categoryIndex = this.categories.findIndex(c => c.id == categoryId);
    const categoryCopy = {...this.categories.find(c => c.id == categoryId)} as ExpenseCategory;
    updateCallback(categoryCopy);
    this.categories.splice(categoryIndex, 1, categoryCopy);
  }
  
}