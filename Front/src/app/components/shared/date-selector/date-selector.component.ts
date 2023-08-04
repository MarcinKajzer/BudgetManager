import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-date-selector',
  templateUrl: './date-selector.component.html',
  styleUrls: ['./date-selector.component.scss']
})
export class DateSelectorComponent {

  @Input() year: number | undefined;  
  @Input() month: number | undefined; 

  @Output() selectDate = new EventEmitter();

  years: number[] = [2023]

  months: { key: string; value: number }[] = [
    { key: 'January', value: 1 },
    { key: 'February', value: 2 },
    { key: 'March', value: 3 },
    { key: 'April', value: 4 },
    { key: 'May', value: 5 },
    { key: 'June', value: 6 },
    { key: 'July', value: 7 },
    { key: 'August', value: 8 },
    { key: 'September', value: 9 },
    { key: 'October', value: 10 },
    { key: 'November', value: 11 },
    { key: 'December', value: 12 }
  ];
 
  date = new Date()

  selectedDate = { // przypisać inputy
    year: this.date.getFullYear(),
    month: this.date.getMonth() + 1
  }

  confirmDate() {
    this.selectDate.emit(this.selectedDate)
  }
}
