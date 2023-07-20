import { Expense } from "./expense";

export interface ExpenseSubcategory {
  id: string,
  name: string,
  expenses: Expense[]
  dailyExpenses: number[]
};