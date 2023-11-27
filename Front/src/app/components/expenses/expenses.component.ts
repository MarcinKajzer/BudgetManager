import { Component, ElementRef, ViewChild } from '@angular/core';
import { ExpenseTableCategory } from 'src/app/types/expense-table-category.type';
import { Expense } from '../../types/expense.type';
import { ExpensesService } from '../../services/expenses.service';
import { PositionsPopoverService } from '../../services/positions-popover.service';
import { DailyPositions } from 'src/app/types/selected-positions.type';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.scss']
})
export class ExpensesComponent {

  @ViewChild('amountInput', {static: false}) amountInputRef?: ElementRef;

  expensesTable?: ExpenseTableCategory[];
  dailySummary?: number[];
  monthlySummary?: number;

  dailyExpenses: DailyPositions = new DailyPositions;

  date = new Date()
  selectedYear: number = this.date.getFullYear();
  selectedMonth: number = this.date.getMonth() + 1;
  numberOfDays = this.daysInMonth(this.selectedYear, this.selectedMonth);
  days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);

  constructor(private popoverService: PositionsPopoverService, private expensesService: ExpensesService) {
    
    this.expensesService.getExpenses()
      .subscribe(data => { 
        this.prepareData(data);
        this.expensesTable = data
      });

    this.expensesService.refreshExpenses(this.selectedYear, this.selectedMonth)
      .subscribe();
  }

  prepareData(data: ExpenseTableCategory[]) {
    this.dailySummary = Array(this.numberOfDays).fill(0);
    this.monthlySummary = 0;
    
    for (const category of data){
      category.dailyExpenses = Array(this.numberOfDays).fill(0);
      for (const subcategory of category.subcategories){
        subcategory.dailyExpenses = Array(this.numberOfDays).fill(0);
        for (const expense of subcategory.expenses) {
          const index = new Date(expense.date).getDate()-1;
          subcategory.dailyExpenses[index] += expense.amount;
          category.dailyExpenses[index] += expense.amount;
          this.dailySummary[index] += expense.amount; 
          this.monthlySummary += expense.amount;
        }
      }
    }

    if (this.dailyExpenses.areParamsSet()) {
      this.dailyExpenses.positions = this.getExpensesForDay();
    }
  }

  daysInMonth(year: number, month: number) {
    return new Date(year, month, 0).getDate();
  }

  changeDate(date: any) {
    this.selectedYear = date.year;
    this.selectedMonth = date.month;
    this.numberOfDays = this.daysInMonth(this.selectedYear, this.selectedMonth);

    this.expensesService.refreshExpenses(this.selectedYear, this.selectedMonth)
      .subscribe(() => {
        this.days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);
      });
  }

  getCategory() {
    return this.expensesTable!.find(x => x.id == this.dailyExpenses.categoryId!);
  }

  getSubcategory() {
    return this.getCategory()?.subcategories.find(x => x.id == this.dailyExpenses.subcategoryId);
  }

  getExpensesForDay() {
    return this.getSubcategory()!.expenses.filter(x => new Date(x.date).getDate() == this.dailyExpenses.day);
  }

  showExpensesListPopup(event: any, categoryId: string, subcategoryId: string, day: number) {
    
    this.dailyExpenses.categoryId = categoryId;
    this.dailyExpenses.subcategoryId = subcategoryId;
    this.dailyExpenses.day = day;
    let expenses = this.getExpensesForDay();

    if (expenses!.length == 0) {
      this.popoverService.setListPopoverSettings({isVisible: false});
      return;
    }

    this.dailyExpenses.positions = expenses;
   
    this.popoverService.setFormPopoverSettings({isVisible: false, xOffset: event.clientX, yOffset: event.clientY});
    this.popoverService.setListPopoverSettings({isVisible: true, xOffset: event.clientX, yOffset: event.clientY});
  }

  showExpensesFormPopover(event: any) {
    this.popoverService.setFormPopoverSettings({isVisible: true, xOffset: event.clientX, yOffset: event.clientY});
    this.popoverService.setListPopoverSettings({isVisible: false, xOffset: event.clientX, yOffset: event.clientY});
  }

  addExpense(amount: number) {
    const date = new Date(this.selectedYear, this.selectedMonth - 1, this.dailyExpenses.day);
    this.expensesService.addExpense(this.dailyExpenses.subcategoryId!, amount, date).subscribe();
  }

  deleteExpense(id: string) {
    this.expensesService.deleteExpense(id);
  }

  updateExpense(expense: Expense) {
    this.expensesService.updateExpense(expense);
  }

  sum(arr: number[]) {
    return arr.reduce((a, b) => a + b);
  }
}
