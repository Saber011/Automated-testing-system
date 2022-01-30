import {Component,EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TestDto} from "../../../../api/models/test-dto";
import {TestService} from "../../../../api/services/test.service";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";
import {DictionaryService} from "../../../../api/services/dictionary.service";

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {
  @Input() buttonName: string | undefined;
  @Output() actionButton: EventEmitter<TestDto> = new EventEmitter<TestDto>();
  tests!: Array<TestDto>;
  category!: Array<DictionaryItemDto>;
  selectedValues!: number[];

  constructor(private readonly testService: TestService, private readonly dictionaryService: DictionaryService) { }

  ngOnInit(): void {
    this.refreshData();
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: 1})
      .subscribe(data => {
        if(data && data.content) {
          this.category = data.content;
        }
      });
  }

  refreshData(){
    if(this.selectedValues) {
      this.testService.apiTestGetTestsPost({ body:  this.selectedValues })
        .subscribe(data => {
          if (data && data.content) {
            this.tests = data.content;
          }
        });
    } else {
      this.testService.apiTestGetTestsPost({ body:  [] })
        .subscribe(data => {
          if (data && data.content) {
            this.tests = data.content;
          }
        });
    }
  }
}
