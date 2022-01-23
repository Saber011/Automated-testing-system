/* tslint:disable */
/* eslint-disable */
import { ResponseInfo } from './response-info';
import { UserDto } from './user-dto';
export interface UserDtoArrayServiceResponse {
  content?: null | Array<UserDto>;
  responseInfo: ResponseInfo;
}
