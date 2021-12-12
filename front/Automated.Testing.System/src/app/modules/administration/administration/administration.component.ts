import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {DictionaryDto} from "../../../api/models/dictionary-dto";
import {MatSort} from "@angular/material/sort";
import {DictionaryService} from "../../../api/services/dictionary.service";
import {MatDialog} from "@angular/material/dialog";
import {DictionaryDialog} from "../../dictionary/components/dictionary-dialog/dictionary-dialog.component";
import {UserService} from "../../../api/services/user.service";
import {UserDto} from "../../../api/models/user-dto";

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css']
})
export class AdministrationComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'login', 'change'];
  dataSource!: MatTableDataSource<UserDto>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private readonly userService: UserService, public dialog: MatDialog) {
  }

  ngOnInit(){
    this.loadCategories();
  }

  private loadCategories() {
    this.userService.apiUserGetAllUsersGet()
      .subscribe(value => {
        if(value.content) {
          this.dataSource = new MatTableDataSource(value.content);
          this.dataSource.sort = this.sort;
        }

      });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(dictionaryId: number): void {
    const dialogRef = this.dialog.open(DictionaryDialog, {
      width: '80%',
      data: {
        id: dictionaryId
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

}
