import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AccountHolderInfo } from 'src/app/models/account-holder-info.model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-accout-holder-info',
  templateUrl: './accout-holder-info.component.html',
  styleUrls: ['./accout-holder-info.component.css']
})
export class AccoutHolderInfoComponent implements OnInit {

  // constructor(private _accountService: AccountService) { }

  accountHolderInfo!:AccountHolderInfo;
  loading:boolean = false;

  constructor(private _accountService: AccountService, public dialogRef: MatDialogRef<AccoutHolderInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: number) { }


  ngOnInit(): void {
    this.getAccountHolderInfo(this.data);
  }

  getAccountHolderInfo(accountNumber: number){
    this._accountService.getAccountHolderInfo(accountNumber).subscribe(data => { this.accountHolderInfo = data; this.loading=true; },error=>{
      alert("An error has occurred :(");
    });
  }

}
