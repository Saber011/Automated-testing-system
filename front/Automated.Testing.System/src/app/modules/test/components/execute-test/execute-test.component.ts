import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {TestService} from "../../../../api/services/test.service";
import {Subscription} from "rxjs";
import {TestTaskDto} from "../../../../api/models/test-task-dto";

@Component({
  selector: 'app-execute-test',
  templateUrl: './execute-test.component.html',
  styleUrls: ['./execute-test.component.css']
})
export class ExecuteTestComponent implements OnInit {

  tasks!: Array<TestTaskDto>;
  subscription!: Subscription;
  testId!: number;

  constructor(private activateRoute: ActivatedRoute, private readonly testService: TestService) { }

  ngOnInit(): void {
    this.subscription = this.activateRoute.params
      .subscribe(params=> this.testId = params['id']);
    this.testService.apiTestGetTestTaskGet({testId: this.testId})
      .subscribe(data => {
        if(data && data.content)
        this.tasks = data.content
      })
  }
}
