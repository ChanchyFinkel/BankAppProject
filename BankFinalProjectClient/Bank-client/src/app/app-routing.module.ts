import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountInfoDialogComponent } from './modules/account/account-info-dialog/account-info-dialog.component';
import { CreateAccountComponent } from './modules/account/create-account/create-account.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { WrongRouteComponent } from './modules/wrong-route/wrong-route.component';

const APP_ROUTES: Routes = [

  { path: "", pathMatch: "full", redirectTo: "login" },
  { path: "login", component: LoginComponent },
  { path: "createAnAccount", component: CreateAccountComponent},
  { path: "accountInfo", component: AccountInfoDialogComponent},
  { path: "**",pathMatch:"full", component: WrongRouteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(APP_ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
