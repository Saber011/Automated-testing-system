import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../../../core";
import {UserDto} from "../../../../api/models/user-dto";
import {UserRole} from "../../../../core/models/user-role";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  includeAdminModule!: boolean | undefined;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.user$.subscribe(user =>{
      this.includeAdminModule = user?.roles?.some(x => x == UserRole.Admin);
    })
  }

}
