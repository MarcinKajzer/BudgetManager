import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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

  date = new Date()
  selectedYear: number = this.date.getFullYear();
  selectedMonth: number = this.date.getMonth() + 1;
  numberOfDays = this.daysInMonth(this.selectedYear, this.selectedMonth);
  days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);

  selectedDailyPositions: DailyPositions = {};


  selectedCategoryId: any;
  selectedSubcategoryId?: any;
  selectedDay: any;
 


  savingExpensesId?: string;
  amountInputFocused: boolean = false;

  constructor(private utilitiesService: PositionsPopoverService, private expensesService: ExpensesService) {
    
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

    if (this.selectedCategoryId && this.selectedSubcategoryId && this.selectedDay) {
      this.selectedDailyPositions.positions = this.getExpensesForDay(this.selectedCategoryId, this.selectedSubcategoryId, this.selectedDay);
    }
  }

  daysInMonth(year: number, month: number) {
    return new Date(year, month, 0).getDate();
  }

  ngAfterViewChecked() { // poprawić 
    if (this.amountInputFocused && this.amountInputRef) {
      this.amountInputRef.nativeElement.focus();
      this.amountInputFocused = false;
    } 
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

  getCategory(categoryId: string) {
    return this.expensesTable!.find(x => x.id == categoryId);
  }

  getSubcategory(categoryId: string, subcategoryId: string) {
    return this.getCategory(categoryId)?.subcategories.find(x => x.id == subcategoryId);
  }

  getExpensesForDay(categoryId: string, subcategoryId: string, day: number) {
    return this.getSubcategory(categoryId, subcategoryId)?.expenses.filter(x => new Date(x.date).getDate() == day);
  }

  showExpensesListPopup(event: any, categoryId: string, subcategoryId: string, day: number) {
    let expenses = this.getExpensesForDay(categoryId, subcategoryId, day);

    if (expenses!.length == 0) {
      this.utilitiesService.setListPopoversettings({isVisible: false});
      return;
    }

    this.selectedCategoryId = categoryId;
    this.selectedSubcategoryId = subcategoryId;
    this.selectedDay = day;
    this.selectedDailyPositions.positions = expenses;
   
    this.utilitiesService.setFormPopoversettings({isVisible: false, xOffset: event.clientX, yOffset: event.clientY});
    this.utilitiesService.setListPopoversettings({isVisible: true, xOffset: event.clientX, yOffset: event.clientY});
    
  }

  showExpensesFormPopover(event: any) {
    this.utilitiesService.setFormPopoversettings({isVisible: true, xOffset: event.clientX, yOffset: event.clientY});
    this.utilitiesService.setListPopoversettings({isVisible: false, xOffset: event.clientX, yOffset: event.clientY});
  }

  focusAmountInput() {
    this.amountInputFocused = true;
  }




  addExpense(amount: number) {
    const date = new Date(this.selectedYear, this.selectedMonth - 1, this.selectedDay);
    this.expensesService.addExpense(this.selectedSubcategoryId!, amount, date).subscribe();
  }

  deleteExpense(id: number) {
    this.expensesService.deleteExpense(id);
  }

  updateExpense(expense: Expense) {
    this.expensesService.updateExpense(expense);
  }


  sum(arr: number[]) {
    return arr.reduce((a, b) => a + b);
  }
}
