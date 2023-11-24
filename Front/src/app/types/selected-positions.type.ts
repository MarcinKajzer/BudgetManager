import { Expense } from "./expense.type";
import { Income } from "./income.type";

export interface DailyPositions {
  categoryId?: string;
  subcategoryId?: string;
  day?: number;
  positions?: Expense[] | Income[];
}