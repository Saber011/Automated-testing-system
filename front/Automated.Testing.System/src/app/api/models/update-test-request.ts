/* tslint:disable */
/* eslint-disable */
import { UpdateTestTask } from './update-test-task';
export interface UpdateTestRequest {
  categoryId?: number;
  task?: null | Array<UpdateTestTask>;
  testId?: number;
  testName?: null | string;
}
