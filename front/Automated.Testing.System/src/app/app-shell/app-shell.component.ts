import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subject, takeUntil} from 'rxjs';
import {AuthService} from "../core";
import {UserDto} from "../api/models/user-dto";
import {SideNavDirection} from "../component/side-nav/side-nav-direction";

@Component({
  selector: 'app-app-shell',
  templateUrl: './app-shell.component.html',
  styleUrls: ['./app-shell.component.scss'],
})
export class AppShellComponent implements OnInit, OnDestroy {
  userInfo :UserDto | null | undefined;
  navDirection: SideNavDirection = SideNavDirection.Left;

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
}
