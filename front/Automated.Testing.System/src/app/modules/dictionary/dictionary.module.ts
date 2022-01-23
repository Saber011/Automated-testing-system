import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DictionaryRoutingModule} from './dictionary-routing.module';
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatGridListModule} from "@angular/material/grid-list";
import { DictionaryComponent } from './components/dictionary/dictionary.component';
import {MatTableModule} from "@angular/material/table";
import {MatSortModule} from "@angular/material/sort";
import { DictionaryDialog } from './components/dictionary-dialog/dictionary-dialog.component';
import {MatDialogModule} from "@angular/material/dialog";
import { DialogBoxComponent } from './components/dialog-box/dialog-box.component';
import {FateMaterialModule, FateModule} from "fate-editor";
import {FateMaterialComponent} from "fate-editor/app/fate-material/fate-material.component";
import { ArticleComponent } from './components/article/article.component';
import {MatSelectModule} from "@angular/material/select";
import {MatListModule} from "@angular/material/list";

@NgModule({
  declarations: [DictionaryComponent, DictionaryDialog, DialogBoxComponent, ArticleComponent],
  imports: [
    CommonModule,
    DictionaryRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatGridListModule,
    MatTableModule,
    MatFormFieldModule,
    MatSortModule,
    MatDialogModule,
    FateMaterialModule,
    FateModule,
    MatSelectModule,
    MatListModule
  ],
})
export class DictionaryModule {}
