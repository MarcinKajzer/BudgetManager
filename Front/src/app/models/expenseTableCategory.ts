import { ExpenseTableSubcategory } from "./expenseTableSubcategroy";

export interface ExpenseTableCategory {
  id: string,
  name: string,
  subcategories: ExpenseTableSubcategory[];
  dailyExpenses: number[];
}