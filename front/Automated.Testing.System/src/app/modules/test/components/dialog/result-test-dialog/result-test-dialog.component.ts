import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {TestPassedResultDto} from "../../../../../api/models/test-passed-result-dto";

@Component({
  selector: 'app-result-test-dialog',
  templateUrl: './result-test-dialog.component.html',
  styleUrls: ['./result-test-dialog.component.css']
})
export class ResultTestDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: TestPassedResultDto) { }

  ngOnInit(): void {
  }

}
