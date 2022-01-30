/* tslint:disable */
/* eslint-disable */
import { TestTask } from './test-task';
export interface CreateTestRequest {
  categoryIds?: null | Array<number>;
  task?: null | Array<TestTask>;
  testName?: null | string;
  userId?: number;
}
