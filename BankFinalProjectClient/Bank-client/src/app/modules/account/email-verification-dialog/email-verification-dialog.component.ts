import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
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

  onNoClick(): void {
    this.dialogRef.close();
  }
  // constructor(private _accountService: AccountService, private _router: Router) { }

  ngOnInit(): void {
    this.getVerificationCode();
  }

  // VerificationForm: FormGroup = new FormGroup({
  //   "verificationCode": new FormControl("", [Validators.required, Validators.min(1000),Validators.max(9999)])
  // });

  getVerificationCode(){
    this.getVerificationCodeSubscription = this._accountService.getVerificationCode(this.email).subscribe();
  }

  // createAccount(){
  //   this.loading = true;
  //   this.createAccountSubscription = this._accountService.createAnAccount(this.customerForm.value).subscribe((data: any) => {
  //     this.loading = false;
  //     if (data) {
  //       alert("Account created successfully!");
  //       this._router.navigate(['/login']);
  //     }
  //   }, (error: { message: string; }) => alert("Oops! Something went wrong! try again later!"));
  // }

  ngOnDestroy():void {
    this.getVerificationCodeSubscription?.unsubscribe();
  }

}
