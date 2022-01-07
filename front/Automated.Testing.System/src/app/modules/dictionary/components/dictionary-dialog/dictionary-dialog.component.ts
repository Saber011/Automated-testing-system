import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Dictionary} from "../Interfaces/dictionary";
import {MatTableDataSource} from "@angular/material/table";
import {DictionaryService} from "../../../../api/services/dictionary.service";
import {MatSort} from "@angular/material/sort";
import {DialogBoxComponent} from "../dialog-box/dialog-box.component";
import {DictionaryItemDto} from "../../../../api/models/dictionary-item-dto";

@Component({
  selector: 'app-dictionary-dialog',
  templateUrl: './dictionary-dialog.component.html',
  styleUrls: ['./dictionary-dialog.component.css']
})
export class DictionaryDialog implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'action'];

  // @ts-ignore
  dataSource: MatTableDataSource<DictionaryItemDto>;
  // @ts-ignore
  @ViewChild(MatSort) sort: MatSort;
  constructor(public dialogRef: MatDialogRef<DictionaryDialog>,
              @Inject(MAT_DIALOG_DATA) public data: Dictionary,
              private readonly dictionaryService: DictionaryService,
              public dialog: MatDialog) { }

  ngOnInit(): void {
    this.refreshData();
  }

  refreshData(){
    this.dictionaryService.apiDictionaryGetDictionaryElementsByDictionaryIdGet({id: this.data.id})
      .subscribe(value => {
        if(value.content) {
          this.dataSource = new MatTableDataSource(value.content);
          this.dataSource.sort = this.sort;
        }
      });
  }

  openDialog(action :any,obj :any) {
    console.log(action)
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data:obj
    });

    dialogRef.afterClosed().subscribe((result: { event: string; data: any; }) => {
      if(result.event == 'Add'){
        this.addRowData(result.data);
      }
      if(result.event == 'Update'){
        this.updateRowData(result.data);
      }else if(result.event == 'Delete'){
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(row_obj :any){
this.dictionaryService.apiDictionaryCreateDictionaryItemPost({body:{
    dictionaryId: this.data.id,
    name: row_obj.name
  }}).subscribe(value => {
  this.refreshData();
});
  }

  updateRowData(row_obj :any){
    this.dictionaryService.apiDictionaryUpdateDictionaryItemPut({body:{
      dictionaryId: this.data.id,
      elementId: row_obj.elementId,
      name: row_obj.name
    }}).subscribe(value => {
      this.refreshData();
    });
  }
  deleteRowData(row_obj :any){
    this.dictionaryService.apiDictionaryDeleteDictionaryItemDelete({body:{
        dictionaryId: this.data.id,
        elementId: row_obj.elementId
      }}).subscribe(value => {
      this.refreshData();
    });
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
