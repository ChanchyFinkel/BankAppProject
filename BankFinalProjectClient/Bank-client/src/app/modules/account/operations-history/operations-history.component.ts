import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { OperationHistory } from 'src/app/models/operationHistory.model';
import { AccountService } from '../account.service';
import { AccoutHolderInfoComponent } from '../accout-holder-info/accout-holder-info.component';

@Component({
  selector: 'app-operations-history',
  templateUrl: './operations-history.component.html',
  styleUrls: ['./operations-history.component.css']
})
export class OperationsHistoryComponent implements OnInit {

  columnsToDisplay: string[] = ['IconType','Date', 'From/to Account', 'Debit/Credit', 'Balance'];
  dataSource: MatTableDataSource<OperationHistory> = new MatTableDataSource();
  operationsList: OperationHistory[] = [];
  totalRows = 0;
  pageSize = 15;
  currentPage = 1;
  pageSizeOptions: number[] = [20];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private _accountService: AccountService,public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getOpeartionsHistory();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  getOpeartionsHistory(): void {
    this._accountService.getOperationsHistory(this.currentPage, this.pageSize).subscribe(data => {
      if (data) {
        this.operationsList = data.operations;
        this.dataSource.data = this.operationsList;
        this.paginator.pageIndex = this.currentPage;
        this.paginator.length = data.totalRows;
        console.log(this.operationsList);
      }
      else {
        alert("There is no operations to show")
      }
    }, error => {
      alert("Oops! Something went wrong. Please try again later.");
      console.log(error);
    })
  }
  pageChanged(event: PageEvent) {
    console.log({ event });
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.getOpeartionsHistory();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(accountNumber: number){
    this.dialog.open(AccoutHolderInfoComponent, {
      // width: '80%',
      // height:'80%',
      data: accountNumber,
    });
  }
}