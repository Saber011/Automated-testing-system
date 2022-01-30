import { Component, OnInit } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {AddTestDialogComponent} from "../dialog/add-test-dialog/add-test-dialog.component";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {

  constructor(public dialog: MatDialog) {

  }

  ngOnInit(): void {
  }

  addTest() {
    const dialogRef = this.dialog.open(AddTestDialogComponent, {
      height: '400px',
      width: '350px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
    });
  }
}
