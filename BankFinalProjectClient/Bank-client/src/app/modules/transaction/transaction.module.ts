import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionComponent } from './transaction/transaction.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';

@NgModule({
  declarations: [TransactionComponent],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule
  ],
  bootstrap: [TransactionComponent],

})
export class TransactionModule { }
