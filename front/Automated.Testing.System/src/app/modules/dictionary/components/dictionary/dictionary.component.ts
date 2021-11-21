import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {DictionaryDto} from "../../../../api/models/dictionary-dto";
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-dictionary',
  templateUrl: './dictionary.component.html',
  styleUrls: ['./dictionary.component.css']
})
export class DictionaryComponent implements OnInit {
  displayedColumns: string[] = ['dictionaryId', 'name', 'change'];
  // @ts-ignore
  dataSource: MatTableDataSource<DictionaryDto> ;
  // @ts-ignore
  @ViewChild(MatSort) sort: MatSort;
  constructor(private readonly dictionaryService: DictionaryService) {
  }

  ngOnInit(){
    this.loadCategories();
  }

  private loadCategories() {
    this.dictionaryService.apiDictionaryGetAllDictionaryGet()
      .subscribe(value => {
        if(value.content) {
          console.log(value);
          // @ts-ignore
          this.dataSource = new MatTableDataSource(value.content);
          // @ts-ignore
          this.dataSource.sort = this.sort;
        }

      });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    // @ts-ignore
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
