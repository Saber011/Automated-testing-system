import { Component, OnInit } from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleDto} from "../../../../api/models/article-dto";
import {PageEvent} from "@angular/material/paginator";
import {combineLatest, debounceTime, distinctUntilChanged, startWith, Subject, switchMap, takeUntil} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";

@Component({
  selector: 'app-articles-view',
  templateUrl: './articles-view.component.html',
  styleUrls: ['./articles-view.component.css']
})
export class ArticlesViewComponent implements OnInit {
  articlesData!: Array<ArticleDto>;
  category!: Array<DictionaryItemDto>;
  protected ngUnsubscribe: Subject<any> = new Subject();
  public search!: FormControl;
  public searchStr = '';
  pageEvent!: PageEvent;
  pageIndex: number | undefined;
  pageTotal: number | undefined;
  pageSize: number | undefined;
  public form: FormGroup;
  constructor(private readonly dictionaryService: DictionaryService, private fb: FormBuilder) {
    this.form = this.fb.group({
      category: ['', Validators.required],
      title: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.initData();
    this.form.get("title")?.valueChanges.pipe(
      debounceTime(600),
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.ngUnsubscribe)
    ).subscribe(() =>
    {
      return this.dictionaryService.apiDictionaryGetArticlesGet({
        PageNumber: this.pageIndex,
        PageSize: this.pageSize,
        Title: this.form.get("title")?.value,
        CategoryIds: this.form.get("category")?.value
      }).subscribe(data => {
        if(data && data.content) {
          this.articlesData = data.content;
          this.pageTotal =  data.content[0]?.total;
        }});
    });
   // combineLatest([this.form.get("category")?.valueChanges,this.form.get("")?.valueChanges])
    this.form.get("category")?.valueChanges.pipe(
      debounceTime(600),
      startWith(''),
      distinctUntilChanged(),
      takeUntil(this.ngUnsubscribe)
    ).subscribe(() =>
    {
      return this.dictionaryService.apiDictionaryGetArticlesGet({
        PageNumber: this.pageIndex,
        PageSize: this.pageSize,
        Title: this.form.get("title")?.value ?? null,
        CategoryIds: this.form.get("category")?.value
      }).subscribe(data => {
        if(data && data.content) {
          this.articlesData = data.content;
          this.pageTotal =  data.content[0]?.total;
        }});
    });
  }

  private initData(){
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: 1})
      .subscribe(data => {
        if(data && data.content) {
          this.category = data.content;
        }
      });

    this.dictionaryService.apiDictionaryGetArticlesGet({
      PageNumber: 1,
      PageSize: 5,
    }).subscribe(data => {
      if(data && data.content) {
        this.articlesData = data.content;
        this.pageTotal =  data.content[0]?.total;
      }});
  }

  public getServerData(event:PageEvent){
    this.dictionaryService.apiDictionaryGetArticlesGet({
      PageNumber: event.pageIndex + 1,
      PageSize: event?.pageSize,
      Title: this.form.get("title")?.value,
      CategoryIds: this.form.get("category")?.value
    })
      .subscribe(data => {
        if(data && data.content) {
          this.articlesData = data.content;
          this.pageTotal =  data.content[0]?.total;
          this.pageIndex = event.pageIndex;
        }
      });
    return event;
  }

}
