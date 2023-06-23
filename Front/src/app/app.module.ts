import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CategoriesComponent } from './categories/categories.component';
import { RouterModule } from '@angular/router';
import { ExpensesComponent } from './expenses/expenses.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryComponent } from './category/category.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

const routes = [
  {
    path: 'expenses',
    component: ExpensesComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'categories', 
    component: CategoriesComponent
  }
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CategoriesComponent,
    ExpensesComponent,
    DashboardComponent,
    CategoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NgbModule,
    ReactiveFormsModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
