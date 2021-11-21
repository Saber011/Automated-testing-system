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

@NgModule({
  declarations: [DictionaryComponent],
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
    MatSortModule
  ],
})
export class DictionaryModule {}
