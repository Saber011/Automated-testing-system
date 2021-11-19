/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { AuthenticateInfoServiceResponse } from '../models/authenticate-info-service-response';
import { AuthenticateRequest } from '../models/authenticate-request';
import { BooleanServiceResponse } from '../models/boolean-service-response';
import { RefreshTokenArrayServiceResponse } from '../models/refresh-token-array-service-response';
import { RegisterUserRequest } from '../models/register-user-request';
import { RevokeTokenRequest } from '../models/revoke-token-request';
import { UserDto } from '../models/user-dto';
import { UserDtoArrayServiceResponse } from '../models/user-dto-array-service-response';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiUserAuthenticatePost
   */
  static readonly ApiUserAuthenticatePostPath = '/api/User/Authenticate';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserAuthenticatePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserAuthenticatePost$Response(params?: {
    body?: AuthenticateRequest
  }): Observable<StrictHttpResponse<AuthenticateInfoServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserAuthenticatePostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AuthenticateInfoServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserAuthenticatePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserAuthenticatePost(params?: {
    body?: AuthenticateRequest
  }): Observable<AuthenticateInfoServiceResponse> {

    return this.apiUserAuthenticatePost$Response(params).pipe(
      map((r: StrictHttpResponse<AuthenticateInfoServiceResponse>) => r.body as AuthenticateInfoServiceResponse)
    );
  }

  /**
   * Path part for operation apiUserRefreshTokenPost
   */
  static readonly ApiUserRefreshTokenPostPath = '/api/User/RefreshToken';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserRefreshTokenPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserRefreshTokenPost$Response(params?: {
  }): Observable<StrictHttpResponse<AuthenticateInfoServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserRefreshTokenPostPath, 'post');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AuthenticateInfoServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserRefreshTokenPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserRefreshTokenPost(params?: {
  }): Observable<AuthenticateInfoServiceResponse> {

    return this.apiUserRefreshTokenPost$Response(params).pipe(
      map((r: StrictHttpResponse<AuthenticateInfoServiceResponse>) => r.body as AuthenticateInfoServiceResponse)
    );
  }

  /**
   * Path part for operation apiUserRevokeTokenPost
   */
  static readonly ApiUserRevokeTokenPostPath = '/api/User/RevokeToken';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserRevokeTokenPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserRevokeTokenPost$Response(params?: {
    body?: RevokeTokenRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserRevokeTokenPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<BooleanServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserRevokeTokenPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserRevokeTokenPost(params?: {
    body?: RevokeTokenRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiUserRevokeTokenPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiUserGetAllUsersGet
   */
  static readonly ApiUserGetAllUsersGetPath = '/api/User/GetAllUsers';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserGetAllUsersGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetAllUsersGet$Response(params?: {
  }): Observable<StrictHttpResponse<UserDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserGetAllUsersGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserGetAllUsersGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetAllUsersGet(params?: {
  }): Observable<UserDtoArrayServiceResponse> {

    return this.apiUserGetAllUsersGet$Response(params).pipe(
      map((r: StrictHttpResponse<UserDtoArrayServiceResponse>) => r.body as UserDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiUserGetUserByIdIdGet
   */
  static readonly ApiUserGetUserByIdIdGetPath = '/api/User/GetUserById/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserGetUserByIdIdGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetUserByIdIdGet$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<UserDto>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserGetUserByIdIdGetPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<UserDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserGetUserByIdIdGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetUserByIdIdGet(params: {
    id: number;
  }): Observable<UserDto> {

    return this.apiUserGetUserByIdIdGet$Response(params).pipe(
      map((r: StrictHttpResponse<UserDto>) => r.body as UserDto)
    );
  }

  /**
   * Path part for operation apiUserDeleteUserDelete
   */
  static readonly ApiUserDeleteUserDeletePath = '/api/User/DeleteUser';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserDeleteUserDelete()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserDeleteUserDelete$Response(params?: {
    id?: number;
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserDeleteUserDeletePath, 'delete');
    if (params) {
      rb.query('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<BooleanServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserDeleteUserDelete$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserDeleteUserDelete(params?: {
    id?: number;
  }): Observable<BooleanServiceResponse> {

    return this.apiUserDeleteUserDelete$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiUserRegisterUserPost
   */
  static readonly ApiUserRegisterUserPostPath = '/api/User/RegisterUser';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserRegisterUserPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserRegisterUserPost$Response(params?: {
    body?: RegisterUserRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserRegisterUserPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<BooleanServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserRegisterUserPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiUserRegisterUserPost(params?: {
    body?: RegisterUserRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiUserRegisterUserPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiUserGetRefreshTokensIdRefreshTokensGet
   */
  static readonly ApiUserGetRefreshTokensIdRefreshTokensGetPath = '/api/User/GetRefreshTokens/{id}/refresh-tokens';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiUserGetRefreshTokensIdRefreshTokensGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetRefreshTokensIdRefreshTokensGet$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<RefreshTokenArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, UserService.ApiUserGetRefreshTokensIdRefreshTokensGetPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<RefreshTokenArrayServiceResponse>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiUserGetRefreshTokensIdRefreshTokensGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiUserGetRefreshTokensIdRefreshTokensGet(params: {
    id: number;
  }): Observable<RefreshTokenArrayServiceResponse> {

    return this.apiUserGetRefreshTokensIdRefreshTokensGet$Response(params).pipe(
      map((r: StrictHttpResponse<RefreshTokenArrayServiceResponse>) => r.body as RefreshTokenArrayServiceResponse)
    );
  }

}
