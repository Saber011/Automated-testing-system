import {Component, Inject, OnInit, Optional} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})
export class DialogBoxComponent implements OnInit {

  action:string;
  local_data:any;

  constructor(
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any) {
    this.local_data = {...data};
    this.action = this.local_data.action;
  }

  doAction(){
    this.dialogRef.close({ event:this.action, data:this.local_data});
  }

  closeDialog(){
    this.dialogRef.close({event:'Cancel'});
  }

  convertToUi(isTitle: boolean | null): string {
    if(this.action == 'Add'){
      if(isTitle){
        return "Добавление";
      }
      return "Сохранить";
    }
    if(this.action == 'Update'){
      if(isTitle){
        return "Изменение";
      }
      return "Изменить";
    }
    if(this.action == 'Delete'){
      if(isTitle){
        return "Удаление";
      }
      return "Удалить";
    }
    return '';
  }

  ngOnInit(): void {
  }


}
