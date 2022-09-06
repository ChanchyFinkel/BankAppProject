import { Component } from '@angular/core';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Bank-client';
  _authorized: boolean = false;

  constructor(private _userservice: UserService) {

  }
  ngOnInit() {
    this._userservice.getAuthUser().subscribe(data => {
      
      data ? this._authorized = true : this._authorized = false;

    })
  }
}
