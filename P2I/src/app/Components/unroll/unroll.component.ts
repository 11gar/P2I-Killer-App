import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-unroll',
  templateUrl: './unroll.component.html',
  styleUrls: ['./unroll.component.scss'],
})
export class UnrollComponent {
  @Input() content = [];
}
