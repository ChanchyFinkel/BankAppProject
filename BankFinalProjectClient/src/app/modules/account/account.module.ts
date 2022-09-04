import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../auth/login/login.component';
import { AccountDetailsComponent } from './account-details/account-details.component';



@NgModule({
  declarations: [
    AccountDetailsComponent,
    LoginComponent],
  imports: [
    CommonModule
  ]
})
export class AccountModule { }
