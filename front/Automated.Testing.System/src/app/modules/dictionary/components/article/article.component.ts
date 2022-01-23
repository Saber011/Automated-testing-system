import { Component, OnInit } from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  public form: FormGroup;
  someHtml: any;
  constructor(private readonly dictionaryService: DictionaryService, private router: Router, private fb: FormBuilder) {
    this.form = this.fb.group({
      category: ['', Validators.required],
      title: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }


  save() {
    this.dictionaryService.apiDictionaryCreateArticlePost({body:{
        title: this.form.get('title')?.value,
        text: this.someHtml,
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
