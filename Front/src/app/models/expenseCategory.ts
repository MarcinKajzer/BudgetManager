import { ExpenseSubcategory } from "./expenseSubcategory";

export interface ExpenseCategory {
  id: string,
  name: string,
  subcategories: ExpenseSubcategory[];
}