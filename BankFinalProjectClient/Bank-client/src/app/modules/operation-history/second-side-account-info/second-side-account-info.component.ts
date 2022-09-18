import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SecondSideAccountInfo } from 'src/app/models/second-side-account-info.model';
import { OperationsHistoryService } from '../operations-history.service';

@Component({
  selector: 'app-second-side-account-info',
  templateUrl: './second-side-account-info.component.html',
  styleUrls: ['./second-side-account-info.component.css']
})
export class SecondSideAccountInfoComponent implements OnInit {

  secondSideAccountInfo!: SecondSideAccountInfo;
  loading: boolean = false;
  subscription!: Subscription;

  constructor(private _operationHistoryService: OperationsHistoryService, public dialogRef: MatDialogRef<SecondSideAccountInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: number, private _router: Router,private _snackBar:MatSnackBar) { }

  ngOnInit(): void {
    this.getSecondSideAccountInfo(this.data);
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action,{
      duration:4000
    });
  }

  getSecondSideAccountInfo(accountID: number) {
    this.subscription = this._operationHistoryService.getSecondSideAccountInfo(accountID).subscribe(
      data => { this.secondSideAccountInfo = data; this.loading = true; },
      error => {
        error.status == 401 ? this._router.navigate(['/login']) : this.openSnackBar("An error occured!", "close");
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }


}
