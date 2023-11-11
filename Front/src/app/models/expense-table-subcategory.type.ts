import { Expense } from "./expense.type";

export interface ExpenseTableSubcategory {
  id: string,
  name: string,
  expenses: Expense[]
  dailyExpenses: number[]
};