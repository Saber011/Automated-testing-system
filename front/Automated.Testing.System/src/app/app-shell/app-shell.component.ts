import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import {Routes} from '@angular/router';
import {LoginComponent} from "../modules/login/components/login.component";
import {HomeComponent} from "../modules/home/components/home/home.component";

@Component({
  selector: 'app-app-shell',
  templateUrl: './app-shell.component.html',
  styleUrls: ['./app-shell.component.scss'],
})
export class AppShellComponent implements OnInit, OnDestroy {
  title = '';

  appRoutes: Routes = [
    { path: '/login', component: LoginComponent},
    { path: '/home', component: HomeComponent},
  ];

  private ngUnsubscribe$ = new Subject();

  constructor() {}

  ngOnInit() {
    // todo
  }

  ngOnDestroy() {
    this.ngUnsubscribe$.complete();
  }
}
