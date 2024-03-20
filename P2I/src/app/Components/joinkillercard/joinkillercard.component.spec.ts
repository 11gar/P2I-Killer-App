import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinkillercardComponent } from './joinkillercard.component';

describe('JoinkillercardComponent', () => {
  let component: JoinkillercardComponent;
  let fixture: ComponentFixture<JoinkillercardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JoinkillercardComponent]
    });
    fixture = TestBed.createComponent(JoinkillercardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
