import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})

export class MenuComponent {

  constructor(private _router: Router,private _userService: UserService) { }
  logOut() {
    this._userService.setAuthUser(null);
    this._router.navigate(['/login']);
  }
}