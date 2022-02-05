import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {TestService} from "../../../../api/services/test.service";
import {Subscription} from "rxjs";
import {TestTaskDto} from "../../../../api/models/test-task-dto";
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialog} from "@angular/material/dialog";
import {ResultTestDialogComponent} from "../dialog/result-test-dialog/result-test-dialog.component";

@Component({
  selector: 'app-execute-test',
  templateUrl: './execute-test.component.html',
  styleUrls: ['./execute-test.component.css']
})
export class ExecuteTestComponent implements OnInit {

  tasks!: Array<TestTaskDto>;
  subscription!: Subscription;
  testId!: number;
  myForm : FormGroup;

  constructor(
    private router: Router,
    public dialog: MatDialog,
    private activateRoute: ActivatedRoute,
    private readonly testService: TestService,
    private formBuilder: FormBuilder) {
    this.myForm = formBuilder.group({
      "executeTasks": formBuilder.array([])
    });
  }

  ngOnInit(): void {
    this.subscription = this.activateRoute.params
      .subscribe(params=> this.testId = params['id']);
    this.testService.apiTestGetTestTaskGet({testId: this.testId})
      .subscribe(data => {
        if(data && data.content) {
          this.tasks = data.content;

          data.content.forEach((element) => {
            (<FormArray>this.myForm.controls["executeTasks"]).push(
              this.formBuilder.group({
                "taskId": [element.testTaskId, [Validators.required]],
                "answer": new FormControl("", null),
              }),
            );
          })


        }
      })
  }

  getFormsControls() : FormArray{
    return this.myForm.controls['executeTasks'] as FormArray;
  }

  getTextAnswerControl(index: number){
    return (this.getFormsControls()['controls'][index] as FormArray).get('answer')
  }

  submit(){
    this.testService.apiTestPassTestPost({body:{
      testId: Number(this.testId),
      executeTasks: this.myForm.get("executeTasks")?.value,
      }}).pipe().subscribe(result => {
      this.dialog.open(ResultTestDialogComponent, {
        data: result.content,
      }).afterClosed().subscribe(x => {
        this.router.navigate(['/test']);
      });
    })
  }
}
