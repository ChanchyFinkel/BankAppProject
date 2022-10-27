import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as _moment from 'moment';
import { Moment } from 'moment';
import { Subscription } from 'rxjs';
import { OperationsHistoryService } from '../operations-history.service';

const moment = _moment;

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY',
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-download-as-pdf-dialog',
  templateUrl: './download-as-pdf-dialog.component.html',
  styleUrls: ['./download-as-pdf-dialog.component.css'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class DownloadAsPdfDialogComponent implements OnInit {

  subscription!: Subscription;

  constructor(private _operationsHistoryService: OperationsHistoryService, private _snackBar: MatSnackBar ,private _dialogRef: MatDialogRef<DownloadAsPdfDialogComponent>) { }

  ngOnInit(): void {
  }

  date = new FormControl(moment());

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action,{
      duration:4000
    });
  }

  DownloadAsPDFFile() {
    if (this.date.value > new Date()){
      this.openSnackBar("Date isn't valid!", "close");
    }
    else {
      this.openSnackBar("The download is already in progress", "close");
      // this._dialogRef.close();
      this.subscription = this._operationsHistoryService.CreateOperationsHistoriesPDF(this.date.value.month() + 1, this.date.value.year()).subscribe(data => {
        const byteCharacters = atob(data);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: 'application/pdf' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = "OperationHistories.pdf";
        link.click();
        this._dialogRef.close();
      }, error=>{
        this.openSnackBar("An error occured, Please try again", "close");
      });
    }
  }

  setMonthAndYear(normalizedMonthAndYear: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.date.value!;
    ctrlValue.month(normalizedMonthAndYear.month());
    ctrlValue.year(normalizedMonthAndYear.year());
    this.date.setValue(ctrlValue);
    datepicker.close();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
