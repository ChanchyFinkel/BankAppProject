import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Customer } from 'src/app/models/customer.model';
import { AccountService } from '../account.service';
import { EmailVerificationDialogComponent } from '../email-verification-dialog/email-verification-dialog.component';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit, OnDestroy {

  hide = true;
  customer!:Customer;
  loading!: boolean;
  createAccountSubscription!: Subscription;

  constructor(private _accountService: AccountService, private _router: Router,public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  customerForm: FormGroup = new FormGroup({
    "firstName": new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
    "lastName": new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
    "email": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(100), Validators.email]),
    "password": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(25)]),
  })

  openDialogForVerificationCode() {
    const dialogRef = this.dialog.open(EmailVerificationDialogComponent, {
      // width: '250px',
      data: this.customerForm.controls['email'].value,
    });

    dialogRef.afterClosed().subscribe(result => {
      this.customer=this.customerForm.value;
      this.customer.email=result;
      this.createAnAccount();
      console.log('The dialog was closed');
      // this.animal = result;
    });
    // this.getVerificationCode();
  }

  // getVerificationCode() {
  //   this.getVerificationCodeSubscription = this._accountService.getVerificationCode(this.customerForm.controls['email'].value).subscribe();
  // }

  createAnAccount() {
    this.loading = true;
    this.createAccountSubscription = this._accountService.createAnAccount(this.customer).subscribe((data: any) => {
      this.loading = false;
      if (data) {
        alert("Account created successfully!");
        this._router.navigate(['/login']);
      }
    }, (error: { message: string; }) => alert("Oops! Something went wrong! try again later!"));
  }

  ngOnDestroy(): void {
    // this.getVerificationCodeSubscription?.unsubscribe();
    this.createAccountSubscription?.unsubscribe();
  }
}
