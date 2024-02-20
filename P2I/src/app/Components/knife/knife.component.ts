import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-knife',
  templateUrl: './knife.component.html',
  styleUrls: ['./knife.component.scss']
})
export class KnifeComponent {
  @Input() color = "#FFFFFF";
}
