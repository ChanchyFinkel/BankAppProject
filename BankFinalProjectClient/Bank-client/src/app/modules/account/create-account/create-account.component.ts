import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
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
  subscription!: Subscription;

  constructor(private _accountService: AccountService, private _router: Router,public dialog: MatDialog, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  customerForm: FormGroup = new FormGroup({
    "firstName": new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
    "lastName": new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
    "email": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(100), Validators.email]),
    "password": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(25)]),
  });

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 4000
    });
  }

  openDialogForVerificationCode() {
    const dialogRef = this.dialog.open(EmailVerificationDialogComponent, {
      data: this.customerForm.controls['email'].value,
    });

    dialogRef.afterClosed().subscribe(result => {
      this.customer=this.customerForm.value;
      this.customer.verificationCode=result;
      this.createAnAccount();
    });
  }

  createAnAccount() {
    this.loading = true;
    this.subscription = this._accountService.createAnAccount(this.customer).subscribe(data => {
      this.loading = false;
      if (data) {
      this.openSnackBar("Account created successfully!", "close");
        this._router.navigate(['/login']);
      }
    },error =>this.openSnackBar("Oops! Something went wrong! try again later!", "close"));
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
