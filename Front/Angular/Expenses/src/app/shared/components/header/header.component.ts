import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';

import { LocalStorageService } from '../../../core/services/localstorage/localstorage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private router: Router,
    private localStorageService: LocalStorageService) { }

  ngOnInit() {
  }

  logout() {
    this.localStorageService.delete('expenses_token');
    this.router.navigate(['/login']);
  }

}
