import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  @Input() text: string = 'Button';
  @Input() width = '70vw';
  @Input() height = '70px';
  @Input() fontSize = '1.5rem';
}
