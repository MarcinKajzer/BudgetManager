import { Component } from '@angular/core';

@Component({
    selector: 'app-categories-list',
    templateUrl: './categories-list.component.html',
    styleUrls: ['./categories-list.component.scss']
})
export class CategoriesListComponent {
 
  displayExpensesCategories: boolean = true;
  
  showEarnings() {
    this.displayExpensesCategories = false;
  }

  showExpenses() {
    this.displayExpensesCategories = true;
  }
}