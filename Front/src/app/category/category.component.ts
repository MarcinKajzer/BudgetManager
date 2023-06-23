import { Component, Input } from '@angular/core';
import { Category } from '../models/category';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent {

  protected category?: Category;

  @Input() set Category(value: Category) {
    this.category = value;
  }
  
  newSubcategoryName?: string;
  editedSubcategoryId?: number;
  editedSubcategoryNewName?: string;
  displayAddNewSubcategoryForm: boolean = false;

  addSubcategory() {

    const subcategory = {
      id: this.category!.subcategories.length,
      name: this.newSubcategoryName!,
      displayEditForm: false
    }

    this.category!.subcategories.push(subcategory);
    this.newSubcategoryName = undefined;
  }

  closeAddSubcategoryForm() {
    this.displayAddNewSubcategoryForm = false;
  }

  displayAddSubcategoryForm() {
    this.displayAddNewSubcategoryForm = true;
  }

  setEditedSubcategoryId(subcategoryIndex?: number) {
    this.editedSubcategoryId = subcategoryIndex;
    
    if (subcategoryIndex != undefined) {
      this.editedSubcategoryNewName = this.category!.subcategories[subcategoryIndex].name;
    }
  }

  editSubcategoryName(subcategoryIndex: number) {
    if (this.editedSubcategoryNewName != undefined) {
      this.category!.subcategories[subcategoryIndex].name = this.editedSubcategoryNewName;
    }
    
    this.editedSubcategoryNewName = undefined;
    this.editedSubcategoryId = undefined;
  }

  deleteSubcategory(subcategoryIndex: number) {
    this.category!.subcategories.splice(subcategoryIndex, 1);
  }
}
