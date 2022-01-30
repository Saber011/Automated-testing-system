import { Component, OnDestroy, OnInit } from '@angular/core';
import {Subject, takeUntil} from 'rxjs';
import {Routes} from '@angular/router';
import {LoginComponent} from "../modules/login/components/login.component";
import {HomeComponent} from "../modules/home/components/home/home.component";
import {AuthService} from "../core";
import {UserDto} from "../api/models/user-dto";

@Component({
  selector: 'app-app-shell',
  templateUrl: './app-shell.component.html',
  styleUrls: ['./app-shell.component.scss'],
})
export class AppShellComponent implements OnInit, OnDestroy {
  title = '';
  userInfo :UserDto | null | undefined;

  appRoutes: Routes = [
    { path: '/login', component: LoginComponent},
    { path: '/home', component: HomeComponent},
  ];

  private ngUnsubscribe$ = new Subject();

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.user$.pipe(takeUntil(this.ngUnsubscribe$))
      .subscribe((x) => {
      this.userInfo = x;
    });
  }

  ngOnDestroy() {
    this.ngUnsubscribe$.complete();
  }

  logout() {
    this.authService.logout();
  }
}
