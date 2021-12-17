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

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiAccountAuthenticatePost
   */
  static readonly ApiAccountAuthenticatePostPath = '/api/Account/Authenticate';

  /**
   * Авторизация.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountAuthenticatePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountAuthenticatePost$Response(params?: {
    body?: AuthenticateRequest
  }): Observable<StrictHttpResponse<AuthenticateInfoServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountAuthenticatePostPath, 'post');
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
   * Авторизация.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountAuthenticatePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountAuthenticatePost(params?: {
    body?: AuthenticateRequest
  }): Observable<AuthenticateInfoServiceResponse> {

    return this.apiAccountAuthenticatePost$Response(params).pipe(
      map((r: StrictHttpResponse<AuthenticateInfoServiceResponse>) => r.body as AuthenticateInfoServiceResponse)
    );
  }

  /**
   * Path part for operation apiAccountRefreshTokenPost
   */
  static readonly ApiAccountRefreshTokenPostPath = '/api/Account/RefreshToken';

  /**
   * Получить рефреш токен.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRefreshTokenPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountRefreshTokenPost$Response(params?: {
  }): Observable<StrictHttpResponse<AuthenticateInfoServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRefreshTokenPostPath, 'post');
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
   * Получить рефреш токен.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountRefreshTokenPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountRefreshTokenPost(params?: {
  }): Observable<AuthenticateInfoServiceResponse> {

    return this.apiAccountRefreshTokenPost$Response(params).pipe(
      map((r: StrictHttpResponse<AuthenticateInfoServiceResponse>) => r.body as AuthenticateInfoServiceResponse)
    );
  }

  /**
   * Path part for operation apiAccountRevokeTokenPost
   */
  static readonly ApiAccountRevokeTokenPostPath = '/api/Account/RevokeToken';

  /**
   * Удалить токен.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRevokeTokenPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRevokeTokenPost$Response(params?: {
    body?: RevokeTokenRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRevokeTokenPostPath, 'post');
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
   * Удалить токен.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountRevokeTokenPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRevokeTokenPost(params?: {
    body?: RevokeTokenRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiAccountRevokeTokenPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiAccountRegisterUserPost
   */
  static readonly ApiAccountRegisterUserPostPath = '/api/Account/RegisterUser';

  /**
   * Регистрация нового пользователя.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountRegisterUserPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRegisterUserPost$Response(params?: {
    body?: RegisterUserRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountRegisterUserPostPath, 'post');
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
   * Регистрация нового пользователя.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountRegisterUserPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiAccountRegisterUserPost(params?: {
    body?: RegisterUserRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiAccountRegisterUserPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiAccountGetRefreshTokensGet
   */
  static readonly ApiAccountGetRefreshTokensGetPath = '/api/Account/GetRefreshTokens';

  /**
   * Получить рефреш токены.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiAccountGetRefreshTokensGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountGetRefreshTokensGet$Response(params?: {
    id?: number;
  }): Observable<StrictHttpResponse<RefreshTokenArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, AccountService.ApiAccountGetRefreshTokensGetPath, 'get');
    if (params) {
      rb.query('id', params.id, {});
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
   * Получить рефреш токены.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiAccountGetRefreshTokensGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiAccountGetRefreshTokensGet(params?: {
    id?: number;
  }): Observable<RefreshTokenArrayServiceResponse> {

    return this.apiAccountGetRefreshTokensGet$Response(params).pipe(
      map((r: StrictHttpResponse<RefreshTokenArrayServiceResponse>) => r.body as RefreshTokenArrayServiceResponse)
    );
  }

}
