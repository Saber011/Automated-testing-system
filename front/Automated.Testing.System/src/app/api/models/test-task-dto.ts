/* tslint:disable */
/* eslint-disable */
import { ResponseOption } from './response-option';
export interface TestTaskDto {
  description?: null | string;
  responseOptions?: null | Array<ResponseOption>;
  testId?: number;
  testTaskId?: number;
  typeId?: number;
}
