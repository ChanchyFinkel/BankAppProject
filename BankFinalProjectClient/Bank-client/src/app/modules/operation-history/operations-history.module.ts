import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DownloadAsPdfDialogComponent } from './download-as-pdf-dialog/download-as-pdf-dialog.component';
import { OperationsHistoryComponent } from './operations-history/operations-history.component';
import { SecondSideAccountInfoComponent } from './second-side-account-info/second-side-account-info.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/services/auth.guard';

const OPERATION_HISTORY_ROUTES: Routes = [
  { path: "operationsHistoryList", component:OperationsHistoryComponent,canActivate: [AuthGuard]},
];


@NgModule({
  declarations: [OperationsHistoryComponent,DownloadAsPdfDialogComponent,SecondSideAccountInfoComponent],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule,RouterModule.forChild(OPERATION_HISTORY_ROUTES)
  ]
})
export class OperationsHistoryModule { }
