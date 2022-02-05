import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteTestDialogComponent } from './delete-test-dialog.component';

describe('DeleteTestDialogComponent', () => {
  let component: DeleteTestDialogComponent;
  let fixture: ComponentFixture<DeleteTestDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteTestDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteTestDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
