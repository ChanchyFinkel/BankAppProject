import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAccountComponent } from './modules/account/create-account/create-account.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { WrongRouteComponent } from './modules/wrong-route/wrong-route.component';

const APP_ROUTES: Routes = [

  { path: "", pathMatch: "full", redirectTo: "login", },
  { path: "login", component: LoginComponent },
  { path: "createAccount", component: CreateAccountComponent },
  { path: "account", loadChildren: () => import("./modules/account/account.module").then(m => m.AccountModule) },
  { path: "transaction", loadChildren: () => import("./modules/transaction/transaction.module").then(m => m.TransactionModule) },
  { path: "operationHistory",loadChildren: () => import("./modules/operation-history/operations-history.module").then(m => m.OperationsHistoryModule) },
  { path: "**", pathMatch: "full", component: WrongRouteComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(APP_ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
