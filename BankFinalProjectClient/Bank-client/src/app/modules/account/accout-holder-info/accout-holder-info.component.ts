import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AccountHolderInfo } from 'src/app/models/account-holder-info.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-accout-holder-info',
  templateUrl: './accout-holder-info.component.html',
  styleUrls: ['./accout-holder-info.component.css']
})
export class AccoutHolderInfoComponent implements OnInit , OnDestroy {

  accountHolderInfo!:AccountHolderInfo;
  loading:boolean = false;
  subscription!:Subscription;

  constructor(private _accountService: AccountService, public dialogRef: MatDialogRef<AccoutHolderInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: number, private _router: Router) { }

  ngOnInit(): void {
    this.getAccountHolderInfo(this.data);
  }

  getAccountHolderInfo(accountNumber: number){
    this.subscription=this._accountService.getAccountHolderInfo(accountNumber).subscribe(data => { this.accountHolderInfo = data; this.loading=true; },error=>{
      error.status == 401 ? this._router.navigate(['/login']) : alert("An error has occurred :(");
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
