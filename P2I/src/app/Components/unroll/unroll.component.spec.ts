import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnrollComponent } from './unroll.component';

describe('UnrollComponent', () => {
  let component: UnrollComponent;
  let fixture: ComponentFixture<UnrollComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UnrollComponent]
    });
    fixture = TestBed.createComponent(UnrollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
