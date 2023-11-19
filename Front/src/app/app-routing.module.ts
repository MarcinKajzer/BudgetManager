import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IncomesComponent } from './components/incomes/incomes.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CategoriesListComponent } from './components/categories/categories-list.component';
import { ExpensesComponent } from './components/expenses/expenses.component';
import { SignInComponent } from './components/auth/sign-in/sign-in.component';
import { SignUpComponent } from './components/auth/sign-up/sign-up.component';
import { IncomeCategoriesListComponent } from './components/categories/income-categories-list/income-categories-list.component';
import { ExpenseCategoriesListComponent } from './components/categories/expense-categories-list/expenses-categories-list.component';

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
    component: CategoriesListComponent,
    children: [
      // {
      //   path: '',
      //   redirectTo: 'expenses',
      //   pathMatch: 'full'
      // },
      {
        path: 'incomes',
        component: IncomeCategoriesListComponent, 
      },
      {
        path: 'expenses',
        component: ExpenseCategoriesListComponent, 
      }
    ]
  }
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
