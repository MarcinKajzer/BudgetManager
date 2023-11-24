import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DateSelectorService {

  private selectedDate: Date;
  private selectedDate$?: Subject<Date>;

  //Dokończyć
  date = new Date()
  selectedYear: number = this.date.getFullYear();
  selectedMonth: number = this.date.getMonth() + 1;
  numberOfDays = this.daysInMonth(this.selectedYear, this.selectedMonth);
  days = Array.from({length: this.numberOfDays}, (_, index) => index + 1);

  daysInMonth(year: number, month: number) {
    return new Date(year, month, 0).getDate();
  }

  constructor() {
    this.selectedDate = new Date();
    this.selectedDate$ = new BehaviorSubject<Date>(this.selectedDate);
  }

  getDate(): Observable<Date> {
    return this.selectedDate$ as Observable<Date>;
  }

  setDate(date: Date) {
    this.selectedDate = date;
    this.selectedDate$?.next(date);
  }
}
