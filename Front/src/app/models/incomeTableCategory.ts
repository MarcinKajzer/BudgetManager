import { Income } from "./income";

export interface IncomeTableCategory {
  id: string,
  name: string,
  incomes: Income[];
  dailyIncomes: number[];
}