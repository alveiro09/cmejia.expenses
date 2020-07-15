import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  save(name: string, value: string) {
    localStorage.setItem(name, value);
  }

  get(name: string) {
    return localStorage.getItem(name);
  }

  delete(name: string) {
    localStorage.removeItem(name);
  }

  clear() {
    localStorage.clear();
  }
}
