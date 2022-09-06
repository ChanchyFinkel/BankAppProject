import { Component, OnChanges, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AccountInfoComponent } from '../account-info/account-info.component';

@Component({
  selector: 'app-account-info-dialog',
  template: '',
})
export class AccountInfoDialogComponent implements OnInit {

  constructor(private _dialog: MatDialog) { }

  ngOnInit(): void {
    this._dialog.open(AccountInfoComponent);
  }
}