import { Expense } from "./expense";

export interface ExpenseTableSubcategory {
  id: string,
  name: string,
  expenses: Expense[]
  dailyExpenses: number[]
};