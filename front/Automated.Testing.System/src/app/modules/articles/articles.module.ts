import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ArticlesRoutingModule} from './articles-routing.module';
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from '@angular/material/card';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import { ArticlesViewComponent } from './components/articles-view/articles-view.component';
import { MatExpansionModule} from "@angular/material/expansion";
import {FateMaterialModule, FateModule} from "fate-editor";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatSelectModule} from "@angular/material/select";
import {MatListModule} from "@angular/material/list";
import {MatDialogModule} from "@angular/material/dialog";


@NgModule({
  declarations: [ArticlesViewComponent],
  imports: [
    CommonModule,
    ArticlesRoutingModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatExpansionModule,
    FateMaterialModule,
    FateModule,
    MatPaginatorModule,
    MatSelectModule,
    MatListModule
  ],
})
export class ArticlesModule {}
