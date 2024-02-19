import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnifeComponent } from './knife.component';

describe('KnifeComponent', () => {
  let component: KnifeComponent;
  let fixture: ComponentFixture<KnifeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [KnifeComponent]
    });
    fixture = TestBed.createComponent(KnifeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
