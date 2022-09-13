import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-email-verification-dialog',
  templateUrl: './email-verification-dialog.component.html',
  styleUrls: ['./email-verification-dialog.component.css']
})
export class EmailVerificationDialogComponent implements OnInit, OnDestroy {

  loading!: boolean;
  createAccountSubscription!:Subscription;
  getVerificationCodeSubscription!: Subscription;


  constructor(
    public dialogRef: MatDialogRef<EmailVerificationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public email: string,private _accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.getVerificationCode();
  }

  getVerificationCode(){
    this.getVerificationCodeSubscription = this._accountService.getVerificationCode(this.email).subscribe();
  }

  ngOnDestroy():void {
    this.getVerificationCodeSubscription?.unsubscribe();
  }

}
