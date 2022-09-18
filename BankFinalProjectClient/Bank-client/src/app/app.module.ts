import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountModule } from './modules/account/account.module';
import { AuthModule } from './modules/auth/auth.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MenuComponent } from './menu/menu.component';
import { UserService } from './services/user.service';
import { InterceptorService } from './services/interceptor.service';
import { TransactionModule } from './modules/transaction/transaction.module';
import { OperationsHistoryModule } from './modules/operation-history/operations-history.module';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AccountModule,
    OperationsHistoryModule,
    AuthModule,
    TransactionModule
  ],  providers: [
    UserService,{
    provide: HTTP_INTERCEPTORS,
    useClass: InterceptorService,
    multi: true
   }],
  bootstrap: [AppComponent]
})
export class AppModule { }
