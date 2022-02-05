import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from "../../core";
import {UserRole} from "../../core/models/user-role";

@Component({
  selector: 'app-side-nav-content',
  templateUrl: './side-nav-content.component.html',
  styleUrls: ['./side-nav-content.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SideNavContentComponent implements OnInit {

  navItems = [
    { label: 'Главная', route: '/home'},
    { label: 'Прохождение тестов', route: '/test'},
    { label: 'Обучающий материал', route: '/article'},
    { label: 'Добавление новых тестов', route: '/test/create'},

  ];

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.user$.subscribe(user => {
      if(user?.roles?.some(x => x === UserRole.Admin)){
        this.navItems.push({ label: 'Администрирование', route: '/administration'});
        this.navItems.push({ label: 'Введение справочной информации', route: '/dictionary'});
      }
    })
  }

  onNavigationSelection(navItem: any) {
    this.router.navigate([navItem.route]);
  }

  logout() {
    this.authService.logout();
  }

}
