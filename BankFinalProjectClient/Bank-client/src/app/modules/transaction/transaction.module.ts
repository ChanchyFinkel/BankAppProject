import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { CreateTransactionComponent } from './create-transaction/create-transaction.component';

@NgModule({
  declarations: [CreateTransactionComponent],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule
  ],
  bootstrap: [],

})
export class TransactionModule { }
