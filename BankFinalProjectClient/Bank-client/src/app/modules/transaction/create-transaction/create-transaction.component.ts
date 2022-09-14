import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Transaction } from 'src/app/models/transaction.model';
import { AccountService } from '../../account/account.service';
import { TransactionService } from '../transaction.service';

@Component({
  selector: 'app-create-transaction',
  templateUrl: './create-transaction.component.html',
  styleUrls: ['./create-transaction.component.css']
})
export class CreateTransactionComponent implements OnInit {

  constructor(private _transactionService: TransactionService, private _accountService: AccountService, private _snackBar: MatSnackBar, private _router: Router) { }

  balance: number = 1000000;
  transactionSubscribtion!: Subscription;
  accountSubscribtion!: Subscription;
  fromAccount: number = 0;
  transaction!: Transaction;
  durationInSeconds: number = 5;

  ngOnInit(): void {
    this.accountSubscribtion = this._accountService.getBalanceAccount().subscribe(balance => balance < this.balance ? this.balance = balance : '')
  }

  transactionForm: FormGroup = new FormGroup({
    "toAccount": new FormControl("", [Validators.required, Validators.pattern("[1-90-9]*"), Validators.min(100000)]),
    "ammount": new FormControl("", [Validators.required, Validators.pattern("[1-90-9]*"), Validators.min(1), Validators.max(1000000)]),
  });

  onkeypress(event: KeyboardEvent): void {
    if (event.key < '0' || event.key > '9') {
      event.preventDefault();
    }
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action,{
      duration:4000
    });
  }

  addTransaction() {
    this.transactionSubscribtion = this._transactionService.addTransaction(this.transactionForm.value).subscribe(
      () => { this.openSnackBar("The operation was successfully received", "close") ;
      this.transactionForm.reset();},
      error => error.status == 401 ? this._router.navigate(['/login']) : this.openSnackBar("The operation was failed :(", "close"))
  }

  ngOnDestroy(): void {
    this.transactionSubscribtion?.unsubscribe();
    this.accountSubscribtion.unsubscribe();
  }

}
