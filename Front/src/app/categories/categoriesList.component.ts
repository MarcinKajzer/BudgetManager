import { Component } from '@angular/core';
import { Category } from '../models/category';
import { CategoriesService } from '../services/categories.service';


@Component({
    selector: 'app-categories-list',
    templateUrl: './categoriesList.component.html',
    styleUrls: ['./categoriesList.component.scss']
})
export class CategoriesListComponent {
  categories: Category[] = [];
  newCategoryName?: string;
  
  constructor(private categoriesService: CategoriesService) {
    this.categoriesService.getCategories().subscribe(categories => this.categories = categories);
    this.categoriesService.refreshCategory();
  }

  isNewCategoryNameValid(): boolean {
    return this.newCategoryName != null && this.newCategoryName.trim() !== '';
  }

  addCategory() {
    this.categoriesService.addCategory(this.newCategoryName!);
    this.newCategoryName = undefined;
  }
}