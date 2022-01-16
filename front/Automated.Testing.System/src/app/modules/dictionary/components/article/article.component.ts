import { Component, OnInit } from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {
  someHtml: any;
  constructor(private readonly dictionaryService: DictionaryService, private router: Router) { }

  ngOnInit(): void {
  }


  save() {
    this.dictionaryService.apiDictionaryCreateDictionaryItemPost({body:{
      dictionaryId: 3,
        name: this.someHtml,
      }}).subscribe(value => {
        if(value.content){
          this.router.navigate(['dictionary'], {
            queryParams: {  },
          });
        }
    })
  }
}
