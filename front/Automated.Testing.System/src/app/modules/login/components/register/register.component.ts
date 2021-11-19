import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthService} from "../../../../core";
import {UserService} from "../../../../api/services/user.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  public loginInvalid = false;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private readonly userService: UserService,
              private fb: FormBuilder){
    this.form = this.fb.group({
    username: ['', Validators.email],
    password: ['', Validators.required]
  });
  }

  ngOnInit(): void {
  }

  registration() {
    const username = this.form.get('username')?.value;
    const password = this.form.get('password')?.value;

    this.userService.apiUserRegisterUserPost({body: {password: password, login: username}})
      .subscribe(response => {
        if(response.content)
        {
          alert('win');
          this.router.navigate(['login']);
        }
      })
  }
}
