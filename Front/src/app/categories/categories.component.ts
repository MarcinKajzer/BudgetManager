import { Component } from '@angular/core';
import { Category } from '../models/category';


@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent {
  categories: Category[] = [];
  newCategoryName?: string;
  

  editedSubcategoryId?: number;

  constructor() {
    this.categories = [
      {
          "name": "Dom",
          "id": 0,
          "subcategories": [
              {
                  "id": 0,
                  "name": "Czynsz"
              },
              {
                  "id": 1,
                  "name": "Opłaty"
              }
          ],
          "displayAddNewSubcategoryForm": false
      },
      {
          "name": "Transport",
          "id": 1,
          "subcategories": [
              {
                  "id": 0,
                  "name": "Paliwo"
              },
              {
                  "id": 1,
                  "name": "Serwis samochodu"
              },
              {
                  "id": 2,
                  "name": "Ubezpieczenie OC"
              },
              {
                  "id": 3,
                  "name": "Przegląd"
              },
              {
                  "id": 4,
                  "name": "Komunikacja zbiorowa"
              },
              {
                  "id": 5,
                  "name": "Inne"
              }
          ],
          "displayAddNewSubcategoryForm": false
      },
      {
          "name": "Zdrowie",
          "id": 2,
          "subcategories": [
              {
                  "id": 0,
                  "name": "Odżywki"
              },
              {
                  "id": 1,
                  "name": "Zabiegi"
              },
              {
                  "id": 2,
                  "name": "Prywatne badania"
              }
          ],
          "displayAddNewSubcategoryForm": false
      },
      {
          "name": "Rozwój osobisty",
          "id": 3,
          "subcategories": [
              {
                  "id": 0,
                  "name": "Książki"
              },
              {
                  "id": 1,
                  "name": "Kursy i szkolenia"
              },
              {
                  "id": 2,
                  "name": "Inne"
              }
          ],
          "displayAddNewSubcategoryForm": false
      }
    ]
  }

  addCategory() {
    
    if (this.newCategoryName){
      const category = {
        name: this.newCategoryName,
        id: this.categories.length,
        subcategories: [],
        displayAddNewSubcategoryForm: false
      }

      this.categories.push(category);
      this.newCategoryName = undefined;
    }

    console.log(this.categories)
  }

  closeAddSubcategoryForm(categoryId: number) {
    this.categories[categoryId].displayAddNewSubcategoryForm = false;
  }

  displayAddSubcategoryForm(categoryId: number) {
    this.categories[categoryId].displayAddNewSubcategoryForm = true;
  }

  setEditedSubcategoryId(subcategoryId?: number) {
    this.editedSubcategoryId = subcategoryId;
  }
}
