import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TestRoutingModule} from './test-routing.module';
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import { LearningTestComponent } from './components/lerning-test/learning-test.component';
import {MatSelectModule} from "@angular/material/select";
import { ExecuteTestComponent } from './components/execute-test/execute-test.component';
import { TestListComponent } from './components/test-list/test-list.component';
import { MatListModule} from "@angular/material/list";
import { CreateTestComponent } from './components/change-test/create-test.component';
import { AddTestDialogComponent } from './components/dialog/add-test-dialog/add-test-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {MatRippleModule} from "@angular/material/core";
import {MatIconModule} from "@angular/material/icon";
import {MatCheckboxModule} from "@angular/material/checkbox";
import { ResultTestDialogComponent } from './components/dialog/result-test-dialog/result-test-dialog.component';
import { DeleteTestDialogComponent } from './components/dialog/delete-test-dialog/delete-test-dialog.component';


@NgModule({
  declarations: [LearningTestComponent, ExecuteTestComponent, TestListComponent, CreateTestComponent, AddTestDialogComponent, ResultTestDialogComponent, DeleteTestDialogComponent],
  imports: [
    CommonModule,
    TestRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatSelectModule,
    MatListModule,
    MatDialogModule,
    MatRippleModule,
    MatIconModule,
    MatCheckboxModule
  ],
})
export class TestModule {}
