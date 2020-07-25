import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

import { Login } from '../../core/models/login.model';
import { UsersService } from '../../core/services/users/users.service';
import { LocalStorageService } from '../../core/services/localstorage/localstorage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private usersService: UsersService,
    private localStorageService: LocalStorageService
  ) { }

  ngOnInit() {
  }

  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });

  submit() {
    console.log('login');
    const login: Login = {
      mail: this.loginForm.get('email').value,
      password: this.loginForm.get('password').value

    };
    this.usersService.authenticate(login)
      .subscribe(token => {
        console.log(login);
        this.localStorageService.save('expenses_token', JSON.stringify(token));
        this.router.navigate(['/expensesList']);
      },
        (error) => {
          console.log(`error: ${error.message}`);
        }
      );
  }

  createUser() {
    this.router.navigate(['/users']);
  }
}
