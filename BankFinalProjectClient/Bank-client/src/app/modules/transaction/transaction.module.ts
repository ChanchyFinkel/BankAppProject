import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { CreateTransactionComponent } from './create-transaction/create-transaction.component';
import { AuthGuard } from 'src/app/services/auth.guard';
import { RouterModule, Routes } from '@angular/router';

const TRANSACTION_ROUTES: Routes = [
  { path: "createTransaction", component: CreateTransactionComponent,canActivate: [AuthGuard] },
];
@NgModule({
  declarations: [CreateTransactionComponent],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule,RouterModule.forChild(TRANSACTION_ROUTES)
  ],
  bootstrap: [],

})
export class TransactionModule { }
