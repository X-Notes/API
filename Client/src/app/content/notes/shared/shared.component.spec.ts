import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SharedComponent } from './shared.component';

describe('SharedComponent', () => {
  let component: SharedComponent;
  let fixture: ComponentFixture<SharedComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        declarations: [SharedComponent],
      }).compileComponents();
    }),
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(SharedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
