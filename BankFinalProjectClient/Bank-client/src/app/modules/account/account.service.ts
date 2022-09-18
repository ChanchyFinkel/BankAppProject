import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from 'src/app/models/customer.model';
import { Account } from 'src/app/models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = "/api/Account/";

  constructor(private _http: HttpClient) { }

  createAnAccount(newCustomer: Customer): Observable<boolean> {
    return this._http.post<boolean>(`${this.baseUrl}CreateAccount`, newCustomer)
  }

  getAccountInfo(): Observable<Account> {
    return this._http.get<Account>(`${this.baseUrl}GetAccountInfo`)
  }
  
  getBalanceAccount(): Observable<number> {
    return this._http.get<number>(`${this.baseUrl}GetAccountBalance`)
  }

  getVerificationCode(email: string):Observable<void> {
    return this._http.post<void>(`api/EmailVerification/SendEmailVerification/`,JSON.stringify(email),{headers: {'Content-Type': 'application/json'}})
  }

}
