import { Income } from "./income";

export interface IncomeCategory {
  id: string,
  name: string,
  incomes: Income[];
  dailyExpenses: number[];
}