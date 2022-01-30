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

import { BooleanServiceResponse } from '../models/boolean-service-response';
import { CreateTestRequest } from '../models/create-test-request';
import { TestDtoArrayServiceResponse } from '../models/test-dto-array-service-response';
import { TestTaskDtoArrayServiceResponse } from '../models/test-task-dto-array-service-response';
import { UpdateTestRequest } from '../models/update-test-request';

@Injectable({
  providedIn: 'root',
})
export class TestService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiTestGetTestsPost
   */
  static readonly ApiTestGetTestsPostPath = '/api/Test/GetTests';

  /**
   * Получить все тесты.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiTestGetTestsPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiTestGetTestsPost$Response(params?: {
    body?: Array<number>
  }): Observable<StrictHttpResponse<TestDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, TestService.ApiTestGetTestsPostPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TestDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * Получить все тесты.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiTestGetTestsPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiTestGetTestsPost(params?: {
    body?: Array<number>
  }): Observable<TestDtoArrayServiceResponse> {

    return this.apiTestGetTestsPost$Response(params).pipe(
      map((r: StrictHttpResponse<TestDtoArrayServiceResponse>) => r.body as TestDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiTestGetTestTaskGet
   */
  static readonly ApiTestGetTestTaskGetPath = '/api/Test/GetTestTask';

  /**
   * Получить все задачи теста.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiTestGetTestTaskGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiTestGetTestTaskGet$Response(params?: {
    testId?: number;
  }): Observable<StrictHttpResponse<TestTaskDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, TestService.ApiTestGetTestTaskGetPath, 'get');
    if (params) {
      rb.query('testId', params.testId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TestTaskDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * Получить все задачи теста.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiTestGetTestTaskGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiTestGetTestTaskGet(params?: {
    testId?: number;
  }): Observable<TestTaskDtoArrayServiceResponse> {

    return this.apiTestGetTestTaskGet$Response(params).pipe(
      map((r: StrictHttpResponse<TestTaskDtoArrayServiceResponse>) => r.body as TestTaskDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiTestCheckTestPost
   */
  static readonly ApiTestCheckTestPostPath = '/api/Test/CheckTest';

  /**
   * Проверить решение теста.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiTestCheckTestPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiTestCheckTestPost$Response(params?: {
    testId?: number;
  }): Observable<StrictHttpResponse<TestTaskDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, TestService.ApiTestCheckTestPostPath, 'post');
    if (params) {
      rb.query('testId', params.testId, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TestTaskDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * Проверить решение теста.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiTestCheckTestPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiTestCheckTestPost(params?: {
    testId?: number;
  }): Observable<TestTaskDtoArrayServiceResponse> {

    return this.apiTestCheckTestPost$Response(params).pipe(
      map((r: StrictHttpResponse<TestTaskDtoArrayServiceResponse>) => r.body as TestTaskDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiTestAddTestPost
   */
  static readonly ApiTestAddTestPostPath = '/api/Test/AddTest';

  /**
   * Добавить тест.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiTestAddTestPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiTestAddTestPost$Response(params?: {
    body?: CreateTestRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, TestService.ApiTestAddTestPostPath, 'post');
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
   * Добавить тест.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiTestAddTestPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiTestAddTestPost(params?: {
    body?: CreateTestRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiTestAddTestPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiTestRemoveTestPost
   */
  static readonly ApiTestRemoveTestPostPath = '/api/Test/RemoveTest';

  /**
   * Удалить тест.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiTestRemoveTestPost()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiTestRemoveTestPost$Response(params?: {
    testId?: number;
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, TestService.ApiTestRemoveTestPostPath, 'post');
    if (params) {
      rb.query('testId', params.testId, {});
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
   * Удалить тест.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiTestRemoveTestPost$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiTestRemoveTestPost(params?: {
    testId?: number;
  }): Observable<BooleanServiceResponse> {

    return this.apiTestRemoveTestPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiTestUpdateTestPost
   */
  static readonly ApiTestUpdateTestPostPath = '/api/Test/UpdateTest';

  /**
   * Обновить тест.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiTestUpdateTestPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiTestUpdateTestPost$Response(params?: {
    body?: UpdateTestRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, TestService.ApiTestUpdateTestPostPath, 'post');
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
   * Обновить тест.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiTestUpdateTestPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiTestUpdateTestPost(params?: {
    body?: UpdateTestRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiTestUpdateTestPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

}
