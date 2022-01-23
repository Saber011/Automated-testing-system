import { Component, OnInit } from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";
import {DomSanitizer} from "@angular/platform-browser";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-articles-view',
  templateUrl: './articles-view.component.html',
  styleUrls: ['./articles-view.component.css']
})
export class ArticlesViewComponent implements OnInit {
  articlesData!: Array<DictionaryItemDto>;
  category!: Array<DictionaryItemDto>;
  public form: FormGroup;
  constructor(private readonly dictionaryService: DictionaryService, private fb: FormBuilder) {
    this.form = this.fb.group({
      category: ['', Validators.required],
      title: ['', [Validators.required]],
      pageSize: ['', [Validators.required]],
      pageNumber: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: 1})
      .subscribe(data => {
        if(data && data.content) {
          this.category = data.content;
        }
      });

    this.dictionaryService.apiDictionaryGetArticlesGet({
        Title: this.form.get('title')?.value,
        CategoryIds: this.form.get('category')?.value,
        PageNumber: this.form.get('pageNumber')?.value,
        PageSize: this.form.get('pageSize')?.value,
    })
      .subscribe(data => {
        if(data && data.content) {
          this.articlesData = data.content;
        }
      }
      );
  }

}
