import { Component, OnInit } from '@angular/core';
import { Account } from 'src/app/models/account.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {

  accountInfo!: Account;
  loading=false;

  constructor(private _accountService: AccountService) { }

  ngOnInit(): void {
    this.getAccountInfo();
  }
  getAccountInfo(): void {
    this._accountService.getAccountInfo().subscribe(data => {
      this.loading=true;
      this.accountInfo = data;
    }, error => alert("An error has occurred :("));
  }
}
