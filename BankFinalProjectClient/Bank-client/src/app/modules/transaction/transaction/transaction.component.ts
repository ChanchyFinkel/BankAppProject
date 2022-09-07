import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../account/account.service';
import { TransactionService } from '../transaction.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  constructor(private _transactionService: TransactionService, private _accountService: AccountService) { }

  balance: number = 1000000;

  ngOnInit(): void {
    this._accountService.getBalanceAccount().subscribe(balance => balance < this.balance ? this.balance = balance : '')
  }

  transactionForm: FormGroup = new FormGroup({
    "toAccount": new FormControl("", [Validators.required, Validators.pattern("[1-90-9]*"), Validators.min(100000)]),
    "ammount": new FormControl("", [Validators.required, Validators.pattern("[1-90-9]*"), Validators.min(1), Validators.max(1000000)]),
  });

  onkeypress(event: KeyboardEvent): void {
    if(event.key<'0'||event.key>'9') {
      event.preventDefault();
    }
  }

  addTransaction() {
    this._transactionService.addTransaction(this.transactionForm.value).subscribe(
      () => { alert("transaction done!"); },
      () => { alert("transaction failed!"); })
  }
}
