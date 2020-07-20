import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';

import { User } from '../../core/models/user.model';
import { CreateUser } from '../../core/models/createuser.model';
import { UsersService } from '../../core/services/users/users.service';
import { LocalStorageService } from '../../core/services/localstorage/localstorage.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  user: User;
  createUser: CreateUser;

  constructor(private route: ActivatedRoute,
    private usersService: UsersService,
    private localStorageService: LocalStorageService) { }

  ngOnInit() {
  }

  addUserForm = new FormGroup({
    firstName: new FormControl(''),
    secondName: new FormControl(''),
    lastName: new FormControl(''),
    identityNumber: new FormControl(''),
    identityDocument: new FormControl(''),
    email: new FormControl(''),
    secondEmail: new FormControl(''),
    userName: new FormControl(''),
    password: new FormControl(''),
    age: new FormControl(''),
  });

  submit() {
    console.log('create user');
    const createUser: CreateUser = {
      firstName: this.addUserForm.get('firstName').value,
      secondName: this.addUserForm.get('secondName').value,
      lastName: this.addUserForm.get('lastName').value,
      identityNumber: this.addUserForm.get('identityNumber').value,
      idIdentityDocument: this.addUserForm.get('identityDocument').value,
      email: this.addUserForm.get('email').value,
      secondEmail: this.addUserForm.get('secondEmail').value,
      userName: this.addUserForm.get('userName').value,
      password: this.addUserForm.get('password').value,
      age: this.addUserForm.get('age').value
    };
    this.usersService.createUser(createUser)
      .subscribe(userCreated => {
        console.log(userCreated);
      });
  }
}
