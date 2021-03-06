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

import { ArticleDtoArrayServiceResponse } from '../models/article-dto-array-service-response';
import { BooleanServiceResponse } from '../models/boolean-service-response';
import { CreateArticleRequest } from '../models/create-article-request';
import { CreateDictionaryElementRequest } from '../models/create-dictionary-element-request';
import { DeleteDictionaryElementRequest } from '../models/delete-dictionary-element-request';
import { DictionaryDtoArrayServiceResponse } from '../models/dictionary-dto-array-service-response';
import { DictionaryItemDtoArrayServiceResponse } from '../models/dictionary-item-dto-array-service-response';
import { UpdateArticleRequest } from '../models/update-article-request';
import { UpdateDictionaryElementRequest } from '../models/update-dictionary-element-request';

@Injectable({
  providedIn: 'root',
})
export class DictionaryService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation apiDictionaryGetAllDictionaryGet
   */
  static readonly ApiDictionaryGetAllDictionaryGetPath = '/api/Dictionary/GetAllDictionary';

  /**
   * Получить все справочники.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryGetAllDictionaryGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryGetAllDictionaryGet$Response(params?: {
  }): Observable<StrictHttpResponse<DictionaryDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryGetAllDictionaryGetPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<DictionaryDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * Получить все справочники.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryGetAllDictionaryGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryGetAllDictionaryGet(params?: {
  }): Observable<DictionaryDtoArrayServiceResponse> {

    return this.apiDictionaryGetAllDictionaryGet$Response(params).pipe(
      map((r: StrictHttpResponse<DictionaryDtoArrayServiceResponse>) => r.body as DictionaryDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryGetDictionaryElementsByDictionaryIdGet
   */
  static readonly ApiDictionaryGetDictionaryElementsByDictionaryIdGetPath = '/api/Dictionary/GetDictionaryElementsByDictionaryId';

  /**
   * Получить элементы словаря по справочнику.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryGetDictionaryElementsByDictionaryIdGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryGetDictionaryElementsByDictionaryIdGet$Response(params?: {
    id?: number;
  }): Observable<StrictHttpResponse<DictionaryItemDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryGetDictionaryElementsByDictionaryIdGetPath, 'get');
    if (params) {
      rb.query('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<DictionaryItemDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * Получить элементы словаря по справочнику.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryGetDictionaryElementsByDictionaryIdGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryGetDictionaryElementsByDictionaryIdGet(params?: {
    id?: number;
  }): Observable<DictionaryItemDtoArrayServiceResponse> {

    return this.apiDictionaryGetDictionaryElementsByDictionaryIdGet$Response(params).pipe(
      map((r: StrictHttpResponse<DictionaryItemDtoArrayServiceResponse>) => r.body as DictionaryItemDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryCreateDictionaryItemPost
   */
  static readonly ApiDictionaryCreateDictionaryItemPostPath = '/api/Dictionary/CreateDictionaryItem';

  /**
   * Создать новый элемент.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryCreateDictionaryItemPost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryCreateDictionaryItemPost$Response(params?: {
    body?: CreateDictionaryElementRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryCreateDictionaryItemPostPath, 'post');
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
   * Создать новый элемент.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryCreateDictionaryItemPost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryCreateDictionaryItemPost(params?: {
    body?: CreateDictionaryElementRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiDictionaryCreateDictionaryItemPost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryUpdateDictionaryItemPut
   */
  static readonly ApiDictionaryUpdateDictionaryItemPutPath = '/api/Dictionary/UpdateDictionaryItem';

  /**
   * Обновить данные элемента словаря.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryUpdateDictionaryItemPut()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryUpdateDictionaryItemPut$Response(params?: {
    body?: UpdateDictionaryElementRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryUpdateDictionaryItemPutPath, 'put');
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
   * Обновить данные элемента словаря.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryUpdateDictionaryItemPut$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryUpdateDictionaryItemPut(params?: {
    body?: UpdateDictionaryElementRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiDictionaryUpdateDictionaryItemPut$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryDeleteDictionaryItemDelete
   */
  static readonly ApiDictionaryDeleteDictionaryItemDeletePath = '/api/Dictionary/DeleteDictionaryItem';

  /**
   * Удалить элемент словаря.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryDeleteDictionaryItemDelete()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryDeleteDictionaryItemDelete$Response(params?: {
    body?: DeleteDictionaryElementRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryDeleteDictionaryItemDeletePath, 'delete');
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
   * Удалить элемент словаря.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryDeleteDictionaryItemDelete$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryDeleteDictionaryItemDelete(params?: {
    body?: DeleteDictionaryElementRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiDictionaryDeleteDictionaryItemDelete$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryGetArticlesGet
   */
  static readonly ApiDictionaryGetArticlesGetPath = '/api/Dictionary/GetArticles';

  /**
   * Получить статьи.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryGetArticlesGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryGetArticlesGet$Response(params?: {
    Title?: string;
    CategoryIds?: Array<number>;
    PageSize?: number;
    PageNumber?: number;
  }): Observable<StrictHttpResponse<ArticleDtoArrayServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryGetArticlesGetPath, 'get');
    if (params) {
      rb.query('Title', params.Title, {});
      rb.query('CategoryIds', params.CategoryIds, {});
      rb.query('PageSize', params.PageSize, {});
      rb.query('PageNumber', params.PageNumber, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<ArticleDtoArrayServiceResponse>;
      })
    );
  }

  /**
   * Получить статьи.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryGetArticlesGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryGetArticlesGet(params?: {
    Title?: string;
    CategoryIds?: Array<number>;
    PageSize?: number;
    PageNumber?: number;
  }): Observable<ArticleDtoArrayServiceResponse> {

    return this.apiDictionaryGetArticlesGet$Response(params).pipe(
      map((r: StrictHttpResponse<ArticleDtoArrayServiceResponse>) => r.body as ArticleDtoArrayServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryCreateArticlePost
   */
  static readonly ApiDictionaryCreateArticlePostPath = '/api/Dictionary/CreateArticle';

  /**
   * Cоздать статью.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryCreateArticlePost()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryCreateArticlePost$Response(params?: {
    body?: CreateArticleRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryCreateArticlePostPath, 'post');
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
   * Cоздать статью.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryCreateArticlePost$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryCreateArticlePost(params?: {
    body?: CreateArticleRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiDictionaryCreateArticlePost$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryUpdateArticlePut
   */
  static readonly ApiDictionaryUpdateArticlePutPath = '/api/Dictionary/UpdateArticle';

  /**
   * Обновить статью.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryUpdateArticlePut()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryUpdateArticlePut$Response(params?: {
    body?: UpdateArticleRequest
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryUpdateArticlePutPath, 'put');
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
   * Обновить статью.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryUpdateArticlePut$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  apiDictionaryUpdateArticlePut(params?: {
    body?: UpdateArticleRequest
  }): Observable<BooleanServiceResponse> {

    return this.apiDictionaryUpdateArticlePut$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

  /**
   * Path part for operation apiDictionaryDeleteArticleDelete
   */
  static readonly ApiDictionaryDeleteArticleDeletePath = '/api/Dictionary/DeleteArticle';

  /**
   * Удалить статью.
   *
   *
   *
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `apiDictionaryDeleteArticleDelete()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryDeleteArticleDelete$Response(params?: {
    articleId?: number;
  }): Observable<StrictHttpResponse<BooleanServiceResponse>> {

    const rb = new RequestBuilder(this.rootUrl, DictionaryService.ApiDictionaryDeleteArticleDeletePath, 'delete');
    if (params) {
      rb.query('articleId', params.articleId, {});
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
   * Удалить статью.
   *
   *
   *
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `apiDictionaryDeleteArticleDelete$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  apiDictionaryDeleteArticleDelete(params?: {
    articleId?: number;
  }): Observable<BooleanServiceResponse> {

    return this.apiDictionaryDeleteArticleDelete$Response(params).pipe(
      map((r: StrictHttpResponse<BooleanServiceResponse>) => r.body as BooleanServiceResponse)
    );
  }

}
