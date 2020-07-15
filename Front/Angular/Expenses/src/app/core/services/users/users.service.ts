import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from './../../models/login.model';

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
}
