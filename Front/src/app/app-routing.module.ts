import { NgModule } from '@angular/core';
import { Route, RouterModule, Routes } from '@angular/router';
import { IncomesComponent } from './components/incomes/incomes.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CategoriesListComponent } from './components/categories/categories-list.component';
import { ExpensesComponent } from './components/expenses/expenses.component';
import { SignInComponent } from './components/auth/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { IncomeCategoriesListComponent } from './components/categories/income-categories-list/income-categories-list.component';
import { ExpenseCategoriesListComponent } from './components/categories/expense-categories-list/expenses-categories-list.component';

const routes: Route[] = [
  {
    path: 'signin',
    title: 'BM - Sign In',
    component: SignInComponent
  },
  {
    path: 'signup',
    title: 'BM - Sign Up',
    component: SignUpComponent
  },
  {
    path: 'expenses',
    title: 'BM - Expenses',
    component: ExpensesComponent
  },
  {
    path: 'incomes',
    title: 'BM - Incomes',
    component: IncomesComponent
  },
  {
    path: 'dashboard',
    title: 'BM - Dashboard',
    component: DashboardComponent
  },
  {
    path: 'categories', 
    component: CategoriesListComponent,
    children: [
      {
        path: '',
        redirectTo: '/categories/expenses',
        pathMatch: 'full',
      },
      {
        path: 'incomes',
        title: 'BM - Incomes Categories',
        component: IncomeCategoriesListComponent, 
      },
      {
        path: 'expenses',
        title: 'BM - Expenses Categories',
        component: ExpenseCategoriesListComponent, 
      },
    ]
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
