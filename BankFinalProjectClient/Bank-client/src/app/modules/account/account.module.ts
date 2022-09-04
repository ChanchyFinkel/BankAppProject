import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAccountComponent } from './create-account/create-account.component';
import { AccountInfoComponent } from './account-info/account-info.component';



@NgModule({
  declarations: [CreateAccountComponent,AccountInfoComponent],
  imports: [
    CommonModule
  ],
})
export class AccountModule { }
