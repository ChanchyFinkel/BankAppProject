import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountInfoComponent } from './modules/account/account-info/account-info.component';
import { CreateAccountComponent } from './modules/account/create-account/create-account.component';
import { OperationsHistoryComponent } from './modules/account/operations-history/operations-history.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { CreateTransactionComponent } from './modules/transaction/create-transaction/create-transaction.component';
import { WrongRouteComponent } from './modules/wrong-route/wrong-route.component';

const APP_ROUTES: Routes = [

  { path: "", pathMatch: "full", redirectTo: "login" },
  { path: "login", component: LoginComponent },
  { path: "createAnAccount", component: CreateAccountComponent},
  { path: "accountInfo", component: AccountInfoComponent},
  { path: "createTransaction", component:CreateTransactionComponent},
  { path: "operationsHistory", component:OperationsHistoryComponent},
  { path: "**",pathMatch:"full", component: WrongRouteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(APP_ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
