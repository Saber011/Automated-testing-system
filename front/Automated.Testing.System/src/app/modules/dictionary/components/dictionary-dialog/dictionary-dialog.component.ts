import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Dictionary} from "../Interfaces/dictionary";
import {MatTableDataSource} from "@angular/material/table";
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {DictionaryDto} from "../../../../api/models/dictionary-dto";
import {MatSort} from "@angular/material/sort";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";
import {DialogBoxComponent} from "../dialog-box/dialog-box.component";

@Component({
  selector: 'app-dictionary-dialog',
  templateUrl: './dictionary-dialog.component.html',
  styleUrls: ['./dictionary-dialog.component.css']
})
export class DictionaryDialog implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'action'];

  // @ts-ignore
  dataSource: MatTableDataSource<any[]>;
  // @ts-ignore
  @ViewChild(MatSort) sort: MatSort;
  constructor(public dialogRef: MatDialogRef<DictionaryDialog>,
              @Inject(MAT_DIALOG_DATA) public data: Dictionary,
              private readonly dictionaryService: DictionaryService,
              public dialog: MatDialog) { }

  ngOnInit(): void {
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: this.data.id})
      .subscribe(value => {
          console.log(value);
          // @ts-ignore
          this.dataSource = new MatTableDataSource(value.content);
          // @ts-ignore
          this.dataSource.sort = this.sort;
      });
  }

  openDialog(action :any,obj :any) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data:obj
    });

    dialogRef.afterClosed().subscribe((result: { event: string; data: any; }) => {
      if(result.event == 'Add'){
        this.addRowData(result.data);
      }else if(result.event == 'Update'){
        this.updateRowData(result.data);
      }else if(result.event == 'Delete'){
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(row_obj :any){

  }
  updateRowData(row_obj :any){
  }
  deleteRowData(row_obj :any){
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    // @ts-ignore
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
