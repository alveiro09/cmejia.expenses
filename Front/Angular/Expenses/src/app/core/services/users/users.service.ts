import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Login } from './../../models/login.model';
import { CreateUser } from './../../models/createuser.model';
import { PatchDto } from './../../models/patchdto.model';

import { environment } from './../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) {
  }

  authenticate(login: Login) {
    console.log(login);
    return this.http.post(`${environment.urlUsers}/User/authenticate`, login);
  }

  getUser(username: string) {
    console.log(username);
    return this.http.get(`${environment.urlUsers}/User/username?username=${username}`);
  }

  getUsers() {
    return this.http.get(`${environment.urlUsers}/User`);
  }

  createUser(createUser: CreateUser) {
    console.log(createUser);
    return this.http.post(`${environment.urlUsers}/User`, createUser);
  }

  updateUser(username: string, patchDto: PatchDto[]) {
    console.log(username);
    return this.http.patch(`${environment.urlUsers}/User${username}`, patchDto);
  }
}
