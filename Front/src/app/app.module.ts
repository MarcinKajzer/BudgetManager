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
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { IncomesComponent } from './components/incomes/incomes.component';
import { DateSelectorComponent } from './components/shared/date-selector/date-selector.component';
import { ExpenseCategoriesListComponent } from './components/categories/expense-categories-list/expenses-categories-list.component';
import { IncomeCategoriesListComponent } from './components/categories/income-categories-list/income-categories-list.component';
import { LoginComponent as SignInComponent } from './components/auth/login/login.component';
import { RegisterComponent as SignUpComponent } from './components/auth/register/register.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { JWT_OPTIONS, JwtHelperService, JwtModule } from '@auth0/angular-jwt';

const routes = [
  {
    path: 'signin',
    component: SignInComponent
  },
  {
    path: 'signup',
    component: SignUpComponent
  },
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
    SignInComponent,
    SignUpComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({})
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
