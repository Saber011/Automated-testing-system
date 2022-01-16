import { Component, OnInit } from '@angular/core';
import {TestDto} from "../../../../api/models/test-dto";
import {Router} from "@angular/router";


@Component({
  selector: 'app-learning-test',
  templateUrl: './learning-test.component.html',
  styleUrls: ['./learning-test.component.css']
})

export class LearningTestComponent implements OnInit {
  constructor(private router: Router) { }

  ngOnInit(): void {
  }


  redirect(item: TestDto ) {
    this.router.navigate(['test', item.testId], {
      queryParams: {  },
    });
  }
}
