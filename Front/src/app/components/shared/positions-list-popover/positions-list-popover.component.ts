import { Component, Input } from '@angular/core';
import { PositionsPopoverService } from 'src/app/services/positions-popover.service';
import { PopoverSettings } from 'src/app/types/popover-settings.type';

@Component({
  selector: 'app-positions-list-popover',
  templateUrl: './positions-list-popover.component.html',
  styleUrls: ['./positions-list-popover.component.scss']
})
export class PositionsListPopoverComponent {
  
  @Input() positions?: any[]; //TO DO: change type

  settings?: PopoverSettings = {isVisible: false, xOffset: 0, yOffset: 0}

  constructor(private popoverService: PositionsPopoverService) {
    this.popoverService.getListPopoverSettings()
      .subscribe((settings: PopoverSettings) => {
        this.settings = settings;
      });  
  }

  showPositionsFormPopover() {
    this.popoverService.setFormPopoverSettings({isVisible: true});
    this.popoverService.setListPopoverSettings({isVisible: false});
  }
}
