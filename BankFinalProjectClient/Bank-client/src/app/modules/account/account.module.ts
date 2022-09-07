import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAccountComponent } from './create-account/create-account.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountInfoDialogComponent } from './account-info-dialog/account-info-dialog.component';



@NgModule({
  declarations: [CreateAccountComponent,AccountInfoComponent,AccountInfoDialogComponent],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule
  ],
  exports:[]
})
export class AccountModule { }
