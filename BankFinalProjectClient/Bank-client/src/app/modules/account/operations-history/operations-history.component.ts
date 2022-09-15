import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { OperationHistory } from 'src/app/models/operation-history.model';
import { AccountService } from '../account.service';
import { AccoutHolderInfoComponent } from '../accout-holder-info/accout-holder-info.component';

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css']
})
export class OperationsHistoryComponent implements OnInit, OnDestroy {

  constructor(private _accountService: AccountService, public dialog: MatDialog, private _router: Router) { }

  columnsToDisplay: string[] = ['IconType', 'Date', 'From/to Account', 'Debit/Credit', 'Balance'];
  dataSource: MatTableDataSource<OperationHistory> = new MatTableDataSource();
  totalRows = 0;
  pageSize = 15;
  currentPage = 0;
  pageSizeOptions: number[] = [10, 15, 20];
  subscription!: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.getOpeartionsHistory();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  getOpeartionsHistory(): void {
    this.subscription = this._accountService.getOperationsHistory(this.currentPage, this.pageSize).subscribe(data => {
      if (data) {
        this.dataSource.data = data.operations;
        this.paginator.pageIndex = this.currentPage;
        setTimeout(() => {
          this.paginator.pageIndex = this.currentPage;
          this.paginator.length = data.totalRows;
        })
      }
      else {
        alert("There is no operations to show")
      }
    }, error => {
      error.status == 401 ? this._router.navigate(['/login']) : alert("Oops! Something went wrong. Please try again later.");
    })
  }

  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.getOpeartionsHistory();
  }

  openDialog(accountNumber: number) {
    this.dialog.open(AccoutHolderInfoComponent, {
      data: accountNumber,
    });
  }

  OpenInPDFFile() {
    this.subscription = this._accountService.CreateOperationsHistoriesPDF().subscribe(data => {
      console.log(data);
      console.log(typeof data)
      console.log(data instanceof Blob)
      // var blob = new Blob([data.arrayBuffer.toString()], { type: "string" });
      // var newBlob = new Blob([data]);
      // console.log(newBlob)
      var link = document.createElement('a');
      link.href = URL.createObjectURL(data);
      link.download = "Report.pdf";
      link.click();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}