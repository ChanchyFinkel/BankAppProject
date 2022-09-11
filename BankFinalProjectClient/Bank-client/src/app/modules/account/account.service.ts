import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from 'src/app/models/customer.model';
import { Account } from 'src/app/models/account.model';
import { OperationDataList } from 'src/app/models/operationsDataList.model';
import { AccountHolderInfo } from 'src/app/models/account-holder-info.model';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = "/api/CustomerAccount/";

  constructor(private _http: HttpClient) { }

  createAnAccount(newCustomer: Customer): Observable<boolean> {
    return this._http.post<boolean>(`${this.baseUrl}CreateAccount`, newCustomer)
  }
  getAccountInfo(): Observable<Account> {
    return this._http.get<Account>(`${this.baseUrl}GetAccountInfo`)
  }

  getOperationsHistory(currentPage: number, pageSize: number): Observable<OperationDataList> {
    return this._http.get<OperationDataList>(`api/OperationsHistory/GetOperationsHistories/${pageSize}/${currentPage}`)
  }

  getBalanceAccount(): Observable<number> {
    return this._http.get<number>(`${this.baseUrl}GetAccountBalance`)
  }

  getAccountHolderInfo(accountNumber:number): Observable<AccountHolderInfo> {
    return this._http.get<AccountHolderInfo>(`${this.baseUrl}GetAccountHolderInfo/${accountNumber}`)
  }
}
