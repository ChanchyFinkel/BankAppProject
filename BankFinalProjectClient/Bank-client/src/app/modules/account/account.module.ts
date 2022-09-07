import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAccountComponent } from './create-account/create-account.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { MaterialModule } from '../material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [CreateAccountComponent,AccountInfoComponent],
  imports: [
    CommonModule,MaterialModule,ReactiveFormsModule,FormsModule
  ],exports:[CreateAccountComponent,AccountInfoComponent]
})
export class AccountModule { }
