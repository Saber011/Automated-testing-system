import { Component, OnInit } from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  public form: FormGroup;
  category!: Array<DictionaryItemDto>;
  constructor(private readonly dictionaryService: DictionaryService, private router: Router, private fb: FormBuilder) {
    this.form = this.fb.group({
      category: ['', Validators.required],
      title: ['', [Validators.required]],
      someHtml: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: 1})
      .subscribe(data => {
        if(data && data.content) {
          this.category = data.content;
        }
      });
  }


  save() {
    this.dictionaryService.apiDictionaryCreateArticlePost({body:{
        title: this.form.get('title')?.value,
        text: this.form.get('someHtml')?.value,
        categoryIds: this.form.get('category')?.value
      }}).subscribe(value => {
        if(value.content){
          this.router.navigate(['dictionary'], {
            queryParams: {  },
          });
        }
    })
  }
}
