import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppShellComponent} from './app-shell/app-shell.component';
import {rootLinks} from './core/constants/app-links';
import {SelectivePreloadingStrategy} from "./core/services/selective-preload-strategy";
import {NotFoundComponent} from "./component/not-found/not-found.component";
import {AuthGuard} from "./core";

export const routes: Routes = [
  {
    path: '',
    component: AppShellComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: rootLinks.login },
      {
        path: rootLinks.login,
        loadChildren: () => import('./modules/login/login.module').then(m => m.LoginModule),
        data: {
          preload: true,
          title: 'Login page',
        },
      },
      {
        path: rootLinks.home,
        loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule),
        data: {
          preload: true,
          title: 'home page',
        },
        canActivate: [AuthGuard],
      },
      {
        path: rootLinks.dictionary,
        loadChildren: () => import('./modules/dictionary/dictionary.module').then(m => m.DictionaryModule),
        data: {
          preload: true,
          title: 'dictionary page',
        },
        canActivate: [AuthGuard],
      },
      {
        path: rootLinks.administration,
        loadChildren: () => import('./modules/administration/administration.module').then(m => m.AdministrationModule),
        data: {
          preload: true,
          title: 'admin page',
        },
        canActivate: [AuthGuard],
      },
      {
        path: rootLinks.test,
        loadChildren: () => import('./modules/test/test.module').then(m => m.TestModule),
        data: {
          preload: true,
          title: 'test page',
        },
        canActivate: [AuthGuard],
      },
      {
        path: rootLinks.article,
        loadChildren: () => import('./modules/articles/articles.module').then(m => m.ArticlesModule),
        data: {
          preload: true,
          title: 'article page',
        },
        canActivate: [AuthGuard],
      },
    ],

  },
  {
    path: rootLinks.login,
    loadChildren: () => import('./modules/login/login.module').then(m => m.LoginModule),
    data: { preload: false, title: 'Login page' },
  },
  {
    path: '**',
    component: NotFoundComponent,
    data: {
      preload: true,
      title: '404',
    },
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      preloadingStrategy: SelectivePreloadingStrategy,
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
