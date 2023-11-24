import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PositionsPopoverService } from '../../services/positions-popover.service';
import { IncomesService } from '../../services/incomes.service';
import { Income } from '../../types/income.type';
import { IncomeTableCategory } from 'src/app/types/income-table-category.type';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.scss']
})
export class IncomesComponent {

  @ViewChild('amountInput', {static: false}) amountInputRef?: ElementRef;

  amountInputFocused: boolean = false;

  date = new Date()
  selectedYear: number = this.date.getFullYear();
  selectedMonth: number = this.date.getMonth() + 1 ;

  numberOfDays = 30;
  days = Array.from({ length: this.numberOfDays }, (_, index) => index + 1);
  incomesForm: FormGroup;

  selectedDayExpenses?: Income[];

  selectedCategoryId: any;
  selectedDay: any;
  newIncomeAmount: any;
  newIncomeComment: any
  savingIncomeId?: string;

  data?: IncomeTableCategory[];
  dailySummary?: number[];
  totalSummary?: number;

  isIncomesListPopupVisible: boolean = false;
  incomesListPopupXOffset: number = 0;
  incomesListPopupYOffset: number = 0;

  isEditIncomesPopupVisible: boolean = false;
  editIncomesPopupXOffset: number = 0;
  editIncomesPopupYOffset: number = 0;

  constructor(private utilitiesService: PositionsPopoverService, private incomesService: IncomesService) {
    //safesub
    this.utilitiesService.getIsIncomesPopoverVisible().subscribe(isVisible => this.isEditIncomesPopupVisible = isVisible);

    this.incomesService.getIncomes().subscribe(data => {
      this.prepareData(data);
      this.data = data
    });
    this.incomesService.refreshIncomes(this.selectedYear, this.selectedMonth);

    this.incomesForm = new FormGroup({
      amount: new FormControl(''),
      comment: new FormControl('')
    });
  }

  prepareData(data: IncomeTableCategory[]) {
    this.dailySummary = Array(this.numberOfDays).fill(0);
    this.totalSummary = 0;

    for (const category of data) {
      category.dailyIncomes = Array(this.numberOfDays).fill(0);
      for (const income of category.incomes) {
        const index = new Date(income.date).getDate() - 1
        category.dailyIncomes[index] += income.amount;
        this.dailySummary[index] += income.amount;
        this.totalSummary += income.amount;
      }
    }

    if (this.selectedCategoryId && this.selectedDay) {
      this.selectedDayExpenses = this.getIncomesForDay(this.selectedCategoryId, this.selectedDay);
      this.newIncomeAmount = null;
      this.newIncomeComment = null;
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
    this.incomesService.refreshIncomes(this.selectedYear, this.selectedMonth);
  }

  sum(arr: number[]) {
    return arr.reduce((a, b) => a + b);
  }

  getCategory(categoryId: string) {
    return this.data!.find(x => x.id == categoryId);
  }

  getIncomesForDay(categoryId: string, day: number) {
    return this.getCategory(categoryId)?.incomes.filter(x => new Date(x.date).getDate() == day);
  }

  showIncomesListPopup(event: any, categoryId: string, day: number) {
    let expenses = this.getIncomesForDay(categoryId, day);

    if (expenses!.length == 0) {
      this.isIncomesListPopupVisible = false;
      return;
    }

    this.newIncomeAmount = null;
    this.newIncomeComment = null;

    this.selectedCategoryId = categoryId;
    this.selectedDay = day;
    this.selectedDayExpenses = expenses;

    this.isIncomesListPopupVisible = true;
    this.incomesListPopupXOffset = event.clientX;
    this.incomesListPopupYOffset = event.clientY;

    this.isEditIncomesPopupVisible = false;
  }

  showPopup() {
    this.isEditIncomesPopupVisible = true;
    this.editIncomesPopupXOffset = this.incomesListPopupXOffset;
    this.editIncomesPopupYOffset = this.incomesListPopupYOffset;

    this.isIncomesListPopupVisible = false;
  }

  showEditIncomesPopup(event: any, categoryId: string, day: number) {
    this.newIncomeAmount = null;
    this.newIncomeComment = null;

    this.selectedCategoryId = categoryId;
    this.selectedDay = day;
    this.selectedDayExpenses = this.getIncomesForDay(categoryId, day);

    this.isEditIncomesPopupVisible = true;
    this.editIncomesPopupXOffset = event.clientX;
    this.editIncomesPopupYOffset = event.clientY;

    this.isIncomesListPopupVisible = false;
  }

  addIncome(event: any) {
    const date = new Date();
    date.setDate(this.selectedDay)
    this.incomesService.addIncome(this.selectedCategoryId!, event.target.value, date);
  }

  updateIncomeAmount(event: any, income: Income) {
    this.incomesService.updateIncome(income.id, event.target.value, income.comment);
  }

  updateIncomeComment(event: any, income: Income) {
    this.incomesService.updateIncome(income.id, income.amount, event.target.value);
  }

  focusAmountInput() {
    console.log("amount-focus")
    this.amountInputFocused = true;
  }
}
