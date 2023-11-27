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

  @Input() positions?: any[]; //TO DO: change type

  @Output() positionAdded = new EventEmitter<number>();
  @Output() positionDeleted = new EventEmitter<string>();
  @Output() positionUpdated = new EventEmitter<any>(); //TO DO: change type

  newPositionAmount?: string;
  newPositionComment?: string;
  
  settings?: PopoverSettings = {isVisible: false, xOffset: 0, yOffset: 0}

  constructor(private utilitesService: PositionsPopoverService) {
    this.utilitesService.getFormPopoverSettings()
      .subscribe((settings: PopoverSettings) => {
        this.settings = settings;
      });  
  }

  addPosition(event: any): void {
    if (event.target.value == "") {
      return
    }

    this.positionAdded.emit(+event.target.value);
    this.newPositionAmount = undefined;
  }

  deletePosition(id: string): void {
    this.positionDeleted.emit(id);
  }

  updatePositionAmount(event: any, position: any): void { //TO DO: change type
    if (event.target.value != position.amount && event.target.value.trim() != "") {
      position.amount = +event.target.value;
      this.positionUpdated.emit(position);
    }
  }

  updatePositionComment(event: any, position: any): void { //TO DO: change type
    if (event.target.value != position.comment) {
      position.comment = event.target.value;
      this.positionUpdated.emit(position);
    }
  }
}
