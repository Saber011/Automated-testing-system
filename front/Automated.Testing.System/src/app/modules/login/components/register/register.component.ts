import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthService} from "../../../../core";
import {UserService} from "../../../../api/services/user.service";
import {AccountService} from "../../../../api/services/account.service";

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
              private readonly accountService: AccountService,
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

    this.accountService.apiAccountRegisterUserPost({body: {password: password, login: username}})
      .subscribe(response => {
        if(response.content)
        {
          this.router.navigate(['login']);
        }
      })
  }
}
