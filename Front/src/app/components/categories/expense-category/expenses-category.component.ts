import { Component, Input } from '@angular/core';
import { ExpenseCategory } from '../../../types/expense-category.type';
import { ExpenseCategoriesService } from '../../../services/expense-categories.service';

@Component({
  selector: 'app-category',
  templateUrl: './expenses-category.component.html',
  styleUrls: ['./expenses-category.component.scss']
})
export class ExpensesCategoryComponent {

  protected category?: ExpenseCategory;

  @Input() set Category(value: ExpenseCategory) {
    this.category = value;
  }

  showCategoryNameInput: boolean = false;
  categoryNewName?: string;
  newSubcategoryName?: string;
  editedSubcategoryId?: string;
  editedSubcategoryNewName?: string;
  displayAddNewSubcategoryForm: boolean = false;

  constructor(private categoryService: ExpenseCategoriesService) { }

  showEditNameInput() {
    this.categoryNewName = this.category?.name;
    this.showCategoryNameInput = true;
  }

  hideEditNameInput() {
    this.showCategoryNameInput = false;
    this.categoryNewName = undefined;
  }

  editCategory() {
    if (this.categoryNewName != undefined) {
      this.categoryService.editCategory(this.category!.id, this.categoryNewName);
    }
  }

  deleteCategory() {
    this.categoryService.deleteCategory(this.category!.id);
  }

  displayAddSubcategoryForm() {
    this.displayAddNewSubcategoryForm = true;
  }

  closeAddSubcategoryForm() {
    this.displayAddNewSubcategoryForm = false;
  }

  addSubcategory() {
    this.categoryService.addSubcategory(this.newSubcategoryName!, this.category!.id)
    this.newSubcategoryName = undefined;
  }

  setEditedSubcategoryId(subcategoryId?: string) {
    this.editedSubcategoryId = subcategoryId;
    if (subcategoryId != undefined) {
      this.editedSubcategoryNewName = this.category!.subcategories!.find(s => s.id == subcategoryId)!.name;
    }
  }

  editSubcategoryName(subcategoryId: string) {
    if (this.editedSubcategoryNewName != undefined) {
      this.categoryService.editSubcategory(subcategoryId, this.editedSubcategoryNewName);
    }

    this.editedSubcategoryNewName = undefined;
    this.editedSubcategoryId = undefined;
  }

  deleteSubcategory(categoryId:string, subcategoryId: string) {
    this.categoryService.deleteSubcategory(categoryId, subcategoryId);
  }
}
