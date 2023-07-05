import { Expense } from "./expense";

export interface Subcategory {
  id: string,
  name: string,
  expenses: Expense[]
  dailyExpenses: number[]
};