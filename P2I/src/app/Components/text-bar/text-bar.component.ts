import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-text-bar',
  templateUrl: './text-bar.component.html',
  styleUrls: ['./text-bar.component.scss'],
})
export class TextBarComponent {
  @Input() text: string = 'Text';
  @Input() width = '70vw';
}
