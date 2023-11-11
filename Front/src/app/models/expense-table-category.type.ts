import { ExpenseTableSubcategory } from "./expense-table-subcategory.type";

export interface ExpenseTableCategory {
  id: string,
  name: string,
  subcategories: ExpenseTableSubcategory[];
  dailyExpenses: number[];
}