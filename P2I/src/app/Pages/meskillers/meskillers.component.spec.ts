import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeskillersComponent } from './meskillers.component';

describe('MeskillersComponent', () => {
  let component: MeskillersComponent;
  let fixture: ComponentFixture<MeskillersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MeskillersComponent]
    });
    fixture = TestBed.createComponent(MeskillersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
