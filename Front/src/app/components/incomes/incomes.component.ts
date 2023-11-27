import { Component, ElementRef, ViewChild } from '@angular/core';
import { PositionsPopoverService } from '../../services/positions-popover.service';
import { IncomesService } from '../../services/incomes.service';
import { Income } from '../../types/income.type';
import { IncomeTableCategory } from 'src/app/types/income-table-category.type';
import { DailyPositions } from 'src/app/types/selected-positions.type';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.scss']
})
export class IncomesComponent {

  @ViewChild('amountInput', {static: false}) amountInputRef?: ElementRef;

  incomesTable?: IncomeTableCategory[];
  dailySummary?: number[];
  monthlySummary?: number;

  dailyIncomes: DailyPositions = new DailyPositions;

  date = new Date()
  selectedYear: number = this.date.getFullYear();
  selectedMonth: number = this.date.getMonth() + 1;
  numberOfDays = this.daysInMonth(this.selectedYear, this.selectedMonth);
  days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);

  constructor(private popoverService: PositionsPopoverService, private incomesService: IncomesService) {
    
    this.incomesService.getIncomes()
      .subscribe(data => {
        this.prepareData(data);
        this.incomesTable = data
      });

    this.incomesService.refreshIncomes(this.selectedYear, this.selectedMonth)
      .subscribe();
  }

  prepareData(data: IncomeTableCategory[]) {
    this.dailySummary = Array(this.numberOfDays).fill(0);
    this.monthlySummary = 0;

    for (const category of data) {
      category.dailyIncomes = Array(this.numberOfDays).fill(0);
      for (const income of category.incomes) {
        const index = new Date(income.date).getDate() - 1
        category.dailyIncomes[index] += income.amount;
        this.dailySummary[index] += income.amount;
        this.monthlySummary += income.amount;
      }
    }

    if (this.dailyIncomes.areParamsSet()) {
      this.dailyIncomes.positions = this.getIncomesForDay();
    }
  }

  daysInMonth(year: number, month: number) {
    return new Date(year, month, 0).getDate();
  }

  changeDate(date: any) {
    this.selectedYear = date.year;
    this.selectedMonth = date.month;
    this.numberOfDays = this.daysInMonth(this.selectedYear, this.selectedMonth);

    this.incomesService.refreshIncomes(this.selectedYear, this.selectedMonth)
      .subscribe(() => {
        this.days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);
      });
  }

  getCategory() {
    return this.incomesTable!.find(x => x.id == this.dailyIncomes.categoryId!);
  }

  getIncomesForDay() {
    return this.getCategory()?.incomes.filter(x => new Date(x.date).getDate() == this.dailyIncomes.day);
  }

  showIncomesListPopup(event: any, categoryId: string, day: number) {
    
    this.dailyIncomes.categoryId = categoryId;
    this.dailyIncomes.day = day;
    let incomes = this.getIncomesForDay();
    
    if (incomes!.length == 0) {
      this.popoverService.setListPopoverSettings({isVisible: false});
      return;
    }

    this.dailyIncomes.positions = incomes;
   
    this.popoverService.setFormPopoverSettings({isVisible: false, xOffset: event.clientX, yOffset: event.clientY});
    this.popoverService.setListPopoverSettings({isVisible: true, xOffset: event.clientX, yOffset: event.clientY});
  }


  showIncomesFormPopup(event: any, categoryId: string, day: number) {
    this.popoverService.setFormPopoverSettings({isVisible: true, xOffset: event.clientX, yOffset: event.clientY});
    this.popoverService.setListPopoverSettings({isVisible: false, xOffset: event.clientX, yOffset: event.clientY});
  }

  addIncome(amount: number) {
    const date = new Date(this.selectedYear, this.selectedMonth - 1, this.dailyIncomes.day);
    this.incomesService.addIncome(this.dailyIncomes.categoryId!, amount, date).subscribe();
  }

  deleteIncome(id: string) {
    this.incomesService.deleteIncome(id);
  }

  updateIncome(income: Income) {
    this.incomesService.updateIncome(income);
  }

  sum(arr: number[]) {
    return arr.reduce((a, b) => a + b);
  }
}
