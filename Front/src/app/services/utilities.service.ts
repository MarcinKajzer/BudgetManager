import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class UtilitiesService {
    private isExpensesPopoverVisible = new Subject();
    private isIncomesPopoverVisible = new Subject();

    setIsExpensesPopoverVisible(isVisible: boolean) {
        this.isExpensesPopoverVisible.next(isVisible);
    }

    getIsExpensesPopoverVisible(): Observable<boolean> {
        return this.isExpensesPopoverVisible as Observable<boolean>;
    }

    setIsIncomesPopoverVisible(isVisible: boolean) {
        this.isIncomesPopoverVisible.next(isVisible);
    }

    getIsIncomesPopoverVisible(): Observable<boolean> {
        return this.isIncomesPopoverVisible as Observable<boolean>;
    }
}