import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { Auth } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private authUser: BehaviorSubject<Auth | any> = new BehaviorSubject(null);

  userDetailsKey = 'authUser'

  constructor() { }

  getAuthUser(): Observable<Auth | any> {

    if (!this.authUser.value) {
      const user = localStorage.getItem(this.userDetailsKey);
      if (user) {
        this.authUser.next(JSON.parse(user));
      }
    }
    return this.authUser.asObservable();
  }

  setAuthUser(_auth: Auth | any): void {
    this.authUser.next(_auth);
    if (_auth) {
      localStorage.setItem(this.userDetailsKey, JSON.stringify(_auth))
    } else {
      localStorage.removeItem(this.userDetailsKey)
    }
  }
}
