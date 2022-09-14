import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(public _userService: UserService, public _router: Router) { }
  canActivate(): boolean {
    if (this._userService.getAuthUser()) {
      return true;
    }
    this._router.navigate(['login']);
    return false;
  }
}
