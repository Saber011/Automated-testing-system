import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {TestDto} from "../../../../../api/models/test-dto";
import {TestService} from "../../../../../api/services/test.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-delete-test-dialog',
  templateUrl: './delete-test-dialog.component.html',
  styleUrls: ['./delete-test-dialog.component.css']
})
export class DeleteTestDialogComponent implements OnInit {

  constructor(
    private router: Router,
    public dialogRef: MatDialogRef<DeleteTestDialogComponent>,
    public testService: TestService,
    @Inject(MAT_DIALOG_DATA) public data: TestDto) { }

  ngOnInit(): void {
  }

  deleteTest(){
    this.testService.apiTestRemoveTestPost({testId: this.data.testId})
  .subscribe(result => {
    this.dialogRef.close();
  });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
