import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DictionaryComponent} from "./components/dictionary/dictionary.component";
import {ArticleComponent} from "./components/article/article.component";

const routes: Routes = [
  {
    path: '',
    component: DictionaryComponent,
  },
  {
    path: 'article',
    component: ArticleComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DictionaryRoutingModule {}
