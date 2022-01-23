/* tslint:disable */
/* eslint-disable */
import { ArticleDto } from './article-dto';
import { ResponseInfo } from './response-info';
export interface ArticleDtoArrayServiceResponse {
  content?: null | Array<ArticleDto>;
  responseInfo: ResponseInfo;
}
