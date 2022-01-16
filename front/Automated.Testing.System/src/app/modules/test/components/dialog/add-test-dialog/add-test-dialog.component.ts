import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CreateTestRequest} from "../../../../../api/models/create-test-request";

@Component({
  selector: 'app-add-test-dialog',
  templateUrl: './add-test-dialog.component.html',
  styleUrls: ['./add-test-dialog.component.css']
})
export class AddTestDialogComponent implements OnInit {

  test!: CreateTestRequest;

  constructor(
    public dialogRef: MatDialogRef<AddTestDialogComponent>) { }

  ngOnInit(): void {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
