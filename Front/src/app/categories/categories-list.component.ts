import { Component } from '@angular/core';
import { Category } from '../models/category';
import { CategoriesService } from '../services/expense-categories.service';


@Component({
    selector: 'app-categories-list',
    templateUrl: './categories-list.component.html',
    styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent {
  expensesCategories: Category[] = [];

  //DO PRZENIESIENIA
  earningsCategories = [
    {
      "id": "3d106a41-c5a1-455e-a457-c3e9afd8c345",
      "name": "Praca"
    },
    {
      "id": "3d106a41-c5a1-455e-a457-c3e9afd8c378",
      "name": "Dorobek"
    },
  ]

  editedCategoryId?: string;
  editedCategoryNewName? :string;

  setEditedCategoryId(id?: string) {
    this.editedCategoryId = id;
  }

  deleteCategory(id: string) {

  }

  editCategoryName(id: string) {

  }

  //...........................
  newCategoryName?: string;
  displayExpensesCategories: boolean = true;
  
  constructor(private categoriesService: CategoriesService) {
    this.categoriesService.getCategories().subscribe(categories => this.expensesCategories = categories);
    this.categoriesService.refreshCategory();
  }

  isNewCategoryNameValid(): boolean {
    return this.newCategoryName != null && this.newCategoryName.trim() !== '';
  }

  addCategory() {
    this.categoriesService.addCategory(this.newCategoryName!);
    this.newCategoryName = undefined;
  }

  showEarnings() {
    this.displayExpensesCategories = false;
  }

  showExpenses() {
    this.displayExpensesCategories = true;
  }
}