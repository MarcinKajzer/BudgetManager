import { Component } from '@angular/core';
import { IncomeCategory } from 'src/app/types/income-category.type';
import { IncomeCategoriesService } from 'src/app/services/income-categories.service';

@Component({
  selector: 'app-income-categories-list',
  templateUrl: './income-categories-list.component.html',
  styleUrls: ['./income-categories-list.component.scss']
})
export class IncomeCategoriesListComponent {
  categories: IncomeCategory[] = [];
  newCategoryName?: string;

  editedCategoryId?: string;
  editedCategoryNewName? :string;

  constructor(private incomeCategoriesService: IncomeCategoriesService) {
    this.incomeCategoriesService.getCategories()
      .subscribe(categories => {
        this.categories = categories
      });

    this.incomeCategoriesService.refreshCategory();
  }

  isNameValid(name: string | undefined): boolean {
    return name != null && name.trim() !== '';
  }

  addCategory() {
    this.incomeCategoriesService.addCategory(this.newCategoryName!)
      .subscribe(() => {
        this.newCategoryName = undefined;
      })
  }

  setEditedCategoryId(categoryId?: string) {
    this.editedCategoryId = categoryId;
    if (categoryId != undefined) {
      this.editedCategoryNewName = this.categories!.find(c => c.id == categoryId)!.name;
    }
  }

  editCategoryName(categoryId: string) {
    if (this.isNameValid(this.editedCategoryNewName)) {
      this.incomeCategoriesService.editCategory(categoryId, this.editedCategoryNewName!)
        .subscribe(() => {
          this.editedCategoryNewName = undefined;
          this.editedCategoryId = undefined;
        })
      };
  }

  deleteCategory(id: string) {
    this.incomeCategoriesService.deleteCategory(id);
  }
}
