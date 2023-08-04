import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CategoriesListComponent } from './components/categories/categories-list.component';
import { RouterModule } from '@angular/router';
import { ExpensesComponent } from './components/expenses/expenses.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ExpensesCategoryComponent } from './components/categories/expense-category/expenses-category.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClientModule} from '@angular/common/http';
import { IncomesComponent } from './components/incomes/incomes.component';
import { DateSelectorComponent } from './components/shared/date-selector/date-selector.component';
import { ExpenseCategoriesListComponent } from './components/categories/expense-categories-list/expenses-categories-list.component';
import { IncomeCategoriesListComponent } from './components/categories/income-categories-list/income-categories-list.component';
import { IncomeCategoryComponent } from './components/categories/income-category/income-category.component';

const routes = [
  {
    path: 'expenses',
    component: ExpensesComponent
  },
  {
    path: 'incomes',
    component: IncomesComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'categories', 
    component: CategoriesListComponent
  }
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoriesListComponent,
    ExpensesComponent,
    DashboardComponent,
    ExpensesCategoryComponent,
    IncomesComponent,
    DateSelectorComponent,
    ExpenseCategoriesListComponent,
    IncomeCategoriesListComponent,
    IncomeCategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
