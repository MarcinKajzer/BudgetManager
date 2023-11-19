import { Component } from '@angular/core';
import { ExpenseCategory } from 'src/app/types/expense-category.type';
import { ExpenseCategoriesService } from 'src/app/services/expense-categories.service';

@Component({
  selector: 'app-expense-categories-list',
  templateUrl: './expenses-categories-list.component.html',
  styleUrls: ['./expenses-categories-list.component.scss']
})
export class ExpenseCategoriesListComponent {
  
  expensesCategories: ExpenseCategory[] = [];
  newCategoryName?: string;

  constructor(private expenseCategoriesService: ExpenseCategoriesService) {
    this.expenseCategoriesService.getCategories().subscribe(categories => {
      this.expensesCategories = categories
    });
    this.expenseCategoriesService.refreshCategory();
  }

  isNewCategoryNameValid(): boolean {
    return this.newCategoryName != null && this.newCategoryName.trim() !== '';
  }

  addCategory() {
    this.expenseCategoriesService.addCategory(this.newCategoryName!)
      .subscribe(() => {
        this.newCategoryName = undefined;
      });
  }
}
