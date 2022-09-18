import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Account } from 'src/app/models/account.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit, OnDestroy {

  accountInfo!: Account;
  loading = false;
  subscribtion!: Subscription;

  constructor(private _accountService: AccountService, private _router: Router, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getAccountInfo();
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action,{
      duration:4000
    });
  }

  getAccountInfo(): void {
    this.subscribtion = this._accountService.getAccountInfo().subscribe(data => {
      this.loading = true;
      this.accountInfo = data;
    }, error => error.status == 401 ? this._router.navigate(['/login']) : this.openSnackBar("An error occured!", "close"));
  }

  ngOnDestroy(): void {
    this.subscribtion.unsubscribe();
  }
}
