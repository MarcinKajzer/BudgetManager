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

  showEditCategoryNameForm: boolean = false;
  categoryNewName?: string;

  showAddNewSubcategoryForm: boolean = false;
  newSubcategoryName?: string;
  
  editedSubcategoryId?: string;
  editedSubcategoryNewName?: string;
  
  constructor(private categoryService: ExpenseCategoriesService) { }

  showEditCategoryNameInput() {
    this.categoryNewName = this.category!.name;
    this.showEditCategoryNameForm = true;
  }

  hideEditCategoryNameInput() {
    this.showEditCategoryNameForm = false;
    this.categoryNewName = undefined;
  }

  isNameValid(name: string | undefined): boolean {
    return name != null && name.trim() !== '';
  }

  editCategory() {
    if (this.isNameValid(this.categoryNewName)) {
      this.categoryService.editCategory(this.category!.id, this.categoryNewName!)
        .subscribe(() => {
          this.hideEditCategoryNameInput();
        })
    }
    else {
      this.hideEditCategoryNameInput();
    }
  }

  deleteCategory() {
    this.categoryService.deleteCategory(this.category!.id);
  }

  showAddSubcategoryForm() {
    this.showAddNewSubcategoryForm = true;
  }

  hideAddSubcategoryForm() {
    this.showAddNewSubcategoryForm = false;
  }

  addSubcategory() {
    this.categoryService.addSubcategory(this.newSubcategoryName!, this.category!.id)
      .subscribe(() => {
        this.newSubcategoryName = undefined;
      })
  }

  setEditedSubcategoryId(subcategoryId?: string) {
    this.editedSubcategoryId = subcategoryId;
    if (subcategoryId != undefined) {
      this.editedSubcategoryNewName = this.category!.subcategories!.find(s => s.id == subcategoryId)!.name;
    }
  }

  editSubcategoryName(subcategoryId: string) {
    if (this.isNameValid(this.editedSubcategoryNewName)) {
      this.categoryService.editSubcategory(subcategoryId, this.editedSubcategoryNewName!)
      .subscribe(() => {
        this.editedSubcategoryNewName = undefined;
        this.editedSubcategoryId = undefined;
      });
    }
    else {
      this.editedSubcategoryNewName = undefined;
      this.editedSubcategoryId = undefined;
    }
  }

  deleteSubcategory(categoryId:string, subcategoryId: string) {
    this.categoryService.deleteSubcategory(categoryId, subcategoryId);
  }
}
