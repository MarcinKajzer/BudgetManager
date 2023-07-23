import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CategoriesListComponent } from './categories/categories-list.component';
import { RouterModule } from '@angular/router';
import { ExpensesComponent } from './expenses/expenses.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ExpensesCategoryComponent } from './expenses-category/expenses-category.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClientModule} from '@angular/common/http';
import { IncomesComponent } from './incomes/incomes.component';

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
    IncomesComponent
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
