import { Component, HostListener } from '@angular/core';
import { UtilitiesService } from './services/utilities.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'budget-manager';

  @HostListener('document:click', ['$event'])
  documentClick(event: any): void {
    if (!event.target.classList.contains("category-day") && event.target.closest("#custom-popover") == null) {
      this.utilitiesService.setIsExpensesPopoverVisible(false);
    }

  }

  constructor(private utilitiesService: UtilitiesService) {
    
    
  }
}
