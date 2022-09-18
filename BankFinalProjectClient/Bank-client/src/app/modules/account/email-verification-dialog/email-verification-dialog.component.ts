import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-email-verification-dialog',
  templateUrl: './email-verification-dialog.component.html',
  styleUrls: ['./email-verification-dialog.component.css']
})
export class EmailVerificationDialogComponent implements OnInit, OnDestroy {

  loading!: boolean;
  countClicks: number = 0;
  firstClickTime!: number;
  maximumEnableClicks: number = 10;
  timeRange: number = 15;
  disableClick: boolean = false;
  createAccountSubscription!: Subscription;
  getVerificationCodeSubscription!: Subscription;

  constructor(
    public dialogRef: MatDialogRef<EmailVerificationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public email: string, private _accountService: AccountService
    , private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getVerificationCode();
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 4000
    });
  }

  getVerificationCode() {
    this.getVerificationCodeSubscription = this._accountService.getVerificationCode(this.email).subscribe(() => {
      this.openSnackBar("The verification code has been sent successfully", "close");
    }, () => this.openSnackBar("Sending the verification code failed, please try again", "close"));
  }

  resendVerificationCode() {
    if (!this.disableClick) {
      this.getVerificationCode();
      if (this.countClicks == 0) {
        this.firstClickTime = Date.now();
      }
      this.countClicks++;
      if (this.countClicks == this.maximumEnableClicks) {
        this.countClicks = 0;
        if (new Date(this.firstClickTime + this.timeRange * 60000) > new Date()) {
          this.disableClick = true;
          setTimeout(() => {
            this.disableClick = false;
          }, this.timeRange * 60000);
        }
      }
    }
  }

  onkeypress(event: KeyboardEvent): void {
    if (event.key < '0' || event.key > '9') {
      event.preventDefault();
    }
  }

  ngOnDestroy(): void {
    this.getVerificationCodeSubscription?.unsubscribe();
  }
}
