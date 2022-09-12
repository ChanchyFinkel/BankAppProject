import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnDestroy {
  
  constructor(private _userservice: UserService) {  }

  title = 'Bank-client';
  _authorized: boolean = false;
  subscription!:Subscription;

  ngOnInit() {
    this.subscription=this._userservice.getAuthUser().subscribe(data => {
      data ? this._authorized = true : this._authorized = false;
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
