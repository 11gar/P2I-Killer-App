import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KillerComponent } from './killer.component';

describe('KillerComponent', () => {
  let component: KillerComponent;
  let fixture: ComponentFixture<KillerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [KillerComponent]
    });
    fixture = TestBed.createComponent(KillerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
