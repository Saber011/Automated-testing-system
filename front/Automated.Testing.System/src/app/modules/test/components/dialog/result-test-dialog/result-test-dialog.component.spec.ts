import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultTestDialogComponent } from './result-test-dialog.component';

describe('ResultTestDialogComponent', () => {
  let component: ResultTestDialogComponent;
  let fixture: ComponentFixture<ResultTestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResultTestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultTestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
