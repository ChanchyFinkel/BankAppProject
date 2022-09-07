import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from 'src/app/models/transaction.model';

@Injectable({
  providedIn: 'root'
})
export class TransactionService{

  baseUrl: string = "/api/Transaction/";

  constructor(private _http:HttpClient) { }

  addTransaction(transaction: Transaction):Observable<void>{
    return this._http.post<void>(`${this.baseUrl}AddTransaction`,transaction)
  }
}
