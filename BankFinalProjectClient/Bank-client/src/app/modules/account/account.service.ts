import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from 'src/app/models/customer.model';
import { Account } from 'src/app/models/account.model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  baseUrl: string = "/api/CustomerAccount";

  constructor(private _http:HttpClient) { }

  createAccount(newCustomer: Customer):Observable<boolean>{
    return this._http.post<boolean>(`${this.baseUrl}CreateAccount`,newCustomer)
  }
  getAccountInfo(accountID: Number):Observable<Account>{
    return this._http.get<Account>(`${this.baseUrl}/GetAccountInfo/${accountID}`)
  }
}
