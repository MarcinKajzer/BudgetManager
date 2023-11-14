import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ExpenseTableCategory } from 'src/app/types/expense-table-category.type';
import { Expense } from '../../types/expense.type';
import { ExpensesService } from '../../services/expenses.service';
import { UtilitiesService } from '../../services/utilities.service';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.scss']
})
export class ExpensesComponent {

  @ViewChild('amountInput', {static: false}) amountInputRef?: ElementRef;

  amountInputFocused: boolean = false;

  date = new Date()
  selectedYear: number = this.date.getFullYear();
  selectedMonth: number = this.date.getMonth() + 1 ;

  numberOfDays = 30;
  days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);
  expensesForm: FormGroup;
  
  selectedDayExpenses?: Expense[];

  selectedCategoryId: any;
  selectedSubcategoryId?: any;
  selectedDay: any;
  newExpenseAmount: any;
  newExpenseComment: any
  savingExpensesId?: string;

  data?: ExpenseTableCategory[];
  dailySummary?: number[];
  totalSummary?: number;

  isExpensesListPopupVisible: boolean = false;
  expensesListPopupXOffset: number = 0;
  expensesListPopupYOffset: number = 0;

  isEditExpensesPopupVisible: boolean = false;
  editExpensesPopupXOffset: number = 0;
  editExpensesPopupYOffset: number = 0;

  constructor(private utilitiesService: UtilitiesService, private expensesService: ExpensesService) {
    //safesub
    this.utilitiesService.getIsExpensesPopoverVisible().subscribe(isVisible => this.isEditExpensesPopupVisible = isVisible);

    this.expensesService.getExpenses().subscribe(data => { 
      this.prepareData(data);
      this.data = data
    });
    this.expensesService.refreshExpenses(this.selectedYear, this.selectedMonth);

    this.expensesForm = new FormGroup({
      amount: new FormControl(''),
      comment: new FormControl('')
    });
  }

  prepareData(data: ExpenseTableCategory[]) {
    this.dailySummary = Array(this.numberOfDays).fill(0);
    this.totalSummary = 0;
    
    for (const category of data){
      category.dailyExpenses = Array(this.numberOfDays).fill(0);
      for (const subcategory of category.subcategories){
        subcategory.dailyExpenses = Array(this.numberOfDays).fill(0);
        for (const expense of subcategory.expenses) {
          const index = new Date(expense.date).getDate()-1
          subcategory.dailyExpenses[index] += expense.amount;
          category.dailyExpenses[index] += expense.amount;
          this.dailySummary[index] += expense.amount; 
          this.totalSummary += expense.amount;
        }
      }
    }

    if (this.selectedCategoryId && this.selectedSubcategoryId && this.selectedDay) {
      this.selectedDayExpenses = this.getExpensesForDay(this.selectedCategoryId, this.selectedSubcategoryId, this.selectedDay);
      this.newExpenseAmount = null;
      this.newExpenseComment = null;
    }
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
    this.expensesService.refreshExpenses(this.selectedYear, this.selectedMonth);
  }

  sum(arr: number[]) {
    return arr.reduce((a, b) => a + b);
  }

  getCategory(categoryId: string) {
    return this.data!.find(x => x.id == categoryId);
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
      this.isExpensesListPopupVisible = false;
      return;
    }

    this.newExpenseAmount = null;
    this.newExpenseComment = null;

    this.selectedCategoryId = categoryId;
    this.selectedSubcategoryId = subcategoryId;
    this.selectedDay = day;
    this.selectedDayExpenses = expenses;
   
    this.isExpensesListPopupVisible = true;
    this.expensesListPopupXOffset = event.clientX;
    this.expensesListPopupYOffset = event.clientY;

    this.isEditExpensesPopupVisible = false;
  }

  showPopup() {
    this.isEditExpensesPopupVisible = true;
    this.editExpensesPopupXOffset = this.expensesListPopupXOffset;
    this.editExpensesPopupYOffset = this.expensesListPopupYOffset;

    this.isExpensesListPopupVisible = false;
  }

  showEditExpensesPopup(event: any, categoryId: string, subcategoryId: string, day: number) {
    this.newExpenseAmount = null;
    this.newExpenseComment = null;

    this.selectedCategoryId = categoryId;
    this.selectedSubcategoryId = subcategoryId;
    this.selectedDay = day;
    this.selectedDayExpenses = this.getExpensesForDay(categoryId, subcategoryId, day);
   
    this.isEditExpensesPopupVisible = true;
    this.editExpensesPopupXOffset = event.clientX;
    this.editExpensesPopupYOffset = event.clientY;
    
    this.isExpensesListPopupVisible = false;
  }

  addExpense(event: any) {
    if (event.target.value == "") {
      return
    }

    const date = new Date(this.selectedYear, this.selectedMonth - 1, this.selectedDay);
    this.expensesService.addExpense(this.selectedSubcategoryId!, event.target.value, date);
  }

  updateExpenseAmount(event: any, expense: Expense) {
    if (event.target.value != expense.amount && event.target.value != "") { //obsłużyć wprowadzenie przez użytkownika pustego stringa w tabeli
      this.expensesService.updateExpense(expense.id, event.target.value, expense.comment);
    }
  }

  updateExpenseComment(event: any, expense: Expense) {
    if (event.target.value != expense.comment) {
      this.expensesService.updateExpense(expense.id, expense.amount, event.target.value);
    }
  }

  focusAmountInput() {
    console.log("amount-focus")
    this.amountInputFocused = true;
  }
}
