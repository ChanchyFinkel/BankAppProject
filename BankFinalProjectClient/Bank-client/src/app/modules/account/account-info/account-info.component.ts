import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Account } from 'src/app/models/account.model';
import { AccountService } from '../account.service';
// import { DialogElementsExampleDialog } from './DialogElementsExampleDialog';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {

  accountInfo!: Account;
  constructor(private _accountService: AccountService) { }

  ngOnInit(): void {
    const accountId = sessionStorage.getItem("accountId")?.toString();
    accountId ? this._accountService.getAccountInfo().subscribe(data => {
      this.accountInfo = data;
    }) : alert("An error has occurred :(");
  }

}
