import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DictionaryDialog } from './dictionary-dialog.component';

describe('DictionaryDialogComponent', () => {
  let component: DictionaryDialog;
  let fixture: ComponentFixture<DictionaryDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DictionaryDialog ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DictionaryDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
