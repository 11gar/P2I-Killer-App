import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnstartComponent } from './onstart.component';

describe('OnstartComponent', () => {
  let component: OnstartComponent;
  let fixture: ComponentFixture<OnstartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OnstartComponent]
    });
    fixture = TestBed.createComponent(OnstartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
