import { Component } from '@angular/core';
import { Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss'],
})
export class TextInputComponent {
  @Input() label: string = '';
  @Input() value!: string;
  @Output() valueChange = new EventEmitter<string>();
  @Input() placeholder?: string;
  @Input() error?: string | null;
  @Input() type?: string;
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
