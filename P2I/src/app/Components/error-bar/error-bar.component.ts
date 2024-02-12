import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-error-bar',
  templateUrl: './error-bar.component.html',
  styleUrls: ['./error-bar.component.scss'],
})
export class ErrorBarComponent {
  @Input() error = '';

  constructor(private location: Location) {}

  retour() {
    this.location.back();
  }
}
