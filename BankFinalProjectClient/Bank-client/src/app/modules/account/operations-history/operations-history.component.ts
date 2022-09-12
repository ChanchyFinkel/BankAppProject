import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
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

  constructor(private _accountService: AccountService, public dialog: MatDialog) { }

  columnsToDisplay: string[] = ['IconType', 'Date', 'From/to Account', 'Debit/Credit', 'Balance'];
  dataSource: MatTableDataSource<OperationHistory> = new MatTableDataSource();
  // operationsList: OperationHistory[] = [];
  totalRows = 0;
  pageSize = 15;
  currentPage = 0;
  pageSizeOptions: number[] = [10,15,20];
  subscription!: Subscription;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  // @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    this.getOpeartionsHistory();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;
  }
  getOpeartionsHistory(): void {
    this.subscription = this._accountService.getOperationsHistory(this.currentPage, this.pageSize).subscribe(data => {
      if (data) {
        // this.operationsList = data.operations;
        this.dataSource.data = data.operations;
        this.paginator.pageIndex = this.currentPage;
        this.totalRows = data.totalRows;
        this.paginator.length = this.totalRows;
      }
      else {
        alert("There is no operations to show")
      }
    }, error => {
      alert("Oops! Something went wrong. Please try again later.");
    })
  }

  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.getOpeartionsHistory();
  }

  // applyFilter(event: Event) {
  //   const filterValue = (event.target as HTMLInputElement).value;
  //   this.dataSource.filter = filterValue.trim().toLowerCase();
  // }

  openDialog(accountNumber: number) {
    this.dialog.open(AccoutHolderInfoComponent, {
      data: accountNumber,
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}