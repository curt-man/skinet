import { BusyService } from './../../../core/services/busy.service';
import { Component, inject, input, InputSignal, output } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-empty-state',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    RouterLink
  ],
  templateUrl: './empty-state.component.html',
  styleUrl: './empty-state.component.scss'
})
export class EmptyStateComponent {
  public busyService = inject(BusyService);
  message = input.required<string>();
  icon = input.required<string>();
  actionText = input.required<string>();
  action = output<void>();

  onAction() {
    this.action.emit();
  }

}
