import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class UtilitiesService {
    private isExpensesPopoverVisible = new Subject();

    setIsExpensesPopoverVisible(isVisible: boolean) {
        this.isExpensesPopoverVisible.next(isVisible);
    }

    getIsExpensesPopoverVisible(): Observable<boolean> {
        return this.isExpensesPopoverVisible as Observable<boolean>;
    }
}