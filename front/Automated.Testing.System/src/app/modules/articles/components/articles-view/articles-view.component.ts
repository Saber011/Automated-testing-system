import { Component, OnInit } from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-articles-view',
  templateUrl: './articles-view.component.html',
  styleUrls: ['./articles-view.component.css']
})
export class ArticlesViewComponent implements OnInit {
  articlesData!: Array<DictionaryItemDto>;
  constructor(private readonly dictionaryService: DictionaryService, private sanitizer:DomSanitizer) { }

  ngOnInit(): void {
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: 3})
      .subscribe(data => {
        if(data && data.content) {
          this.articlesData = data.content;
        }
      }
      );
  }

}
