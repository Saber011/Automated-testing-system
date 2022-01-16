/* tslint:disable */
/* eslint-disable */
import { TestTask } from './test-task';
export interface CreateTestRequest {
  categoryId?: number;
  task?: null | Array<TestTask>;
  testName?: null | string;
}
