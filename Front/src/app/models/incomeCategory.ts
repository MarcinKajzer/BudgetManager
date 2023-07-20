import { Income } from "./income";

export interface ExpenseCategory {
  id: string,
  name: string,
  incomes: Income[];
  dailyExpenses: number[];
}