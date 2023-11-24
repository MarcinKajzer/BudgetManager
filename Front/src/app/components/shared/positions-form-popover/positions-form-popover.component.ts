import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PositionsPopoverService } from 'src/app/services/positions-popover.service';
import { Expense } from 'src/app/types/expense.type';
import { Income } from 'src/app/types/income.type';
import { PopoverSettings } from 'src/app/types/popover-settings.type';

@Component({
  selector: 'app-positions-form-popover',
  templateUrl: './positions-form-popover.component.html',
  styleUrls: ['./positions-form-popover.component.scss']
})
export class PositionsFormPopoverComponent {

  @Input() positions?: Expense[] | Income[];

  @Output() expenseAdded = new EventEmitter<number>();
  @Output() expenseDeleted = new EventEmitter<number>();
  @Output() expenseUpdated = new EventEmitter<Expense>();

  newExpenseAmount?: string;
  newExpenseComment?: string;
  
  settings?: PopoverSettings = {isVisible: false, xOffset: 0, yOffset: 0}

  constructor(private utilitesService: PositionsPopoverService) {
    this.utilitesService.getFormPopoverSettings()
      .subscribe((settings: PopoverSettings) => {
        this.settings = settings;
      });  
  }

  addExpense(event: any): void {
    if (event.target.value == "") {
      return
    }

    this.expenseAdded.emit(+event.target.value);
    this.newExpenseAmount = undefined;
  }

  deleteExpense(id: number): void {
    this.expenseDeleted.emit(id);
  }

  updateExpenseAmount(event: any, expense: Expense): void {
    if (event.target.value != expense.amount && event.target.value.trim() != "") {
      expense.amount = +event.target.value;
      this.expenseUpdated.emit(expense);
    }
  }

  updateExpenseComment(event: any, expense: Expense): void {
    if (event.target.value != expense.comment) {
      expense.comment = event.target.value;
      this.expenseUpdated.emit(expense);
    }
  }
}
