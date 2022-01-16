import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExecuteTestComponent } from './execute-test.component';

describe('ExecuteTestComponent', () => {
  let component: ExecuteTestComponent;
  let fixture: ComponentFixture<ExecuteTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExecuteTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExecuteTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
