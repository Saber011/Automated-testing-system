import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LearningTestComponent} from "./components/lerning-test/learning-test.component";
import {ExecuteTestComponent} from "./components/execute-test/execute-test.component";
import {CreateTestComponent} from "./components/change-test/create-test.component";

const routes: Routes = [
  {
    path: '',
    component: LearningTestComponent,
  },
  {
    path: 'create',
    component: CreateTestComponent
  },
  {
    path: ':id',
    component: ExecuteTestComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TestRoutingModule {}
