import { Income } from "./income.type";

export interface IncomeTableCategory {
  id: string,
  name: string,
  incomes: Income[];
  dailyIncomes: number[];
}