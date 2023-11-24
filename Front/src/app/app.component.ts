import { Component, HostListener } from '@angular/core';
import { PositionsPopoverService } from './services/positions-popover.service';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'budget-manager';
  isUserAuthenticated: boolean = false;

  @HostListener('document:click', ['$event'])
  documentClick(event: any): void {
    if (event.target.closest('#expenses-list-popup') == null && 
      !event.target.classList.contains("category-day") && 
      event.target.closest("#edit-expenses-popup") == null) {
      this.utilitiesService.setFormPopoversettings({isVisible: false})
    }

    if (event.target.closest('#incomes-list-popup') == null && 
      !event.target.classList.contains("category-day") && 
      event.target.closest("#edit-incomes-popup") == null) {
      this.utilitiesService.setIsIncomesPopoverVisible(false);
    }
  }

  constructor(private utilitiesService: PositionsPopoverService, private authService: AuthService) {
    this.authService.isUserAuthenticated()
      .subscribe((isAuthenticated: boolean) => this.isUserAuthenticated = isAuthenticated);
  }
}
