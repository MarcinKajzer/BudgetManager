import { Expense } from "./expense.type";
import { Income } from "./income.type";

export class DailyPositions {
  categoryId?: string;
  subcategoryId?: string;
  day?: number;
  positions?: Expense[] | Income[];

  areParamsSet(): boolean {
    return this.categoryId != undefined && this.subcategoryId != undefined && this.day != undefined;
  }
}