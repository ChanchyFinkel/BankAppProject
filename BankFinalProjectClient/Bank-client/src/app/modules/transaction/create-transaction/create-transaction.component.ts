import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  constructor(private _transactionService: TransactionService, private _accountService: AccountService) { }

  balance: number = 1000000;
  transactionSubscribtion!: Subscription;
  accountSubscribtion!: Subscription;
  fromAccount: number = 0;
  transaction!: Transaction;

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

  addTransaction() {
    let authUser = sessionStorage.getItem('authUser');
    if (authUser)
      this.fromAccount = JSON.parse(authUser).accountID;
    this.transaction = this.transactionForm.value;
    this.transaction.fromAccount = this.fromAccount;
    this.transactionSubscribtion = this._transactionService.addTransaction(this.transaction).subscribe(
      () => { alert("transaction done!"); },
      () => { alert("transaction failed!"); })
  }

  ngOnDestroy(): void {
    this.transactionSubscribtion.unsubscribe();
    this.accountSubscribtion.unsubscribe();
  }

}
