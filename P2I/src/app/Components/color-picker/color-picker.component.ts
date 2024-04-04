import { Component } from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-color-picker',
  templateUrl: './color-picker.component.html',
  styleUrls: ['./color-picker.component.scss'],
})
export class ColorPickerComponent {
  @Input() label: string = '';
  @Input() value: string = 'FF00FF';
  @Output() valueChange = new EventEmitter<string>();
  @Input() placeholder?: string;
  @Input() error?: string | null;
  @Input() type?: string = 'text';
  @Input() toptext = 'toptxt';

  protected get bindValue() {
    return this.value;
  }

  protected set bindValue(val: string) {
    this.valueChange.emit(val);
  }

  protected get internalPlaceholder(): string {
    return this.placeholder ? this.placeholder : '';
  }
}
