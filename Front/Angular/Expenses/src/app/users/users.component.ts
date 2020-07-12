import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  constructor() { }

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
    console.log('user added');
  }
}
