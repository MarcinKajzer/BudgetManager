import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { PopoverSettings } from "../types/popover-settings.type";

@Injectable({
    providedIn: 'root'
})
export class PositionsPopoverService {

    private formPopoverSettings = new Subject<PopoverSettings>();
    private listPopoverSettings = new Subject<PopoverSettings>();

    private isIncomesPopoverVisible = new Subject();


    setIsIncomesPopoverVisible(isVisible: boolean) {
        this.isIncomesPopoverVisible.next(isVisible);
    }

    getIsIncomesPopoverVisible(): Observable<boolean> {
        return this.isIncomesPopoverVisible as Observable<boolean>;
    }




    getFormPopoverSettings(): Observable<PopoverSettings> {
        return this.formPopoverSettings as Observable<PopoverSettings>;
    }

    setFormPopoversettings(settings: PopoverSettings): void {
        this.formPopoverSettings.next(settings);
    }


    getListPopoverSettings(): Observable<PopoverSettings> {
        return this.listPopoverSettings as Observable<PopoverSettings>;
    }

    setListPopoversettings(settings: PopoverSettings): void {
        this.listPopoverSettings.next(settings);
    }
}