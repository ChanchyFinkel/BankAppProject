import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAccountComponent } from './create-account/create-account.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmailVerificationDialogComponent } from './email-verification-dialog/email-verification-dialog.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/services/auth.guard';

const ACCOUNT_ROUTES: Routes = [
  { path: "accountInfo", component: AccountInfoComponent,canActivate: [AuthGuard] },
];
@NgModule({
  declarations: [CreateAccountComponent,AccountInfoComponent,EmailVerificationDialogComponent ],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule,RouterModule.forChild(ACCOUNT_ROUTES)
  ],
  exports:[RouterModule]
})
export class AccountModule { }