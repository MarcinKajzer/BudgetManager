import { ExpenseSubcategory } from "./expense-subcategory.type";

export interface ExpenseCategory {
  id: string,
  name: string,
  subcategories?: ExpenseSubcategory[];
}