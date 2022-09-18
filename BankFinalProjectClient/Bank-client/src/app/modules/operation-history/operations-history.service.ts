import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OperationDataList } from 'src/app/models/operations-data-list.model';
import { SecondSideAccountInfo } from 'src/app/models/second-side-account-info.model';

@Injectable({
  providedIn: 'root'
})
export class OperationsHistoryService {
  baseUrl: string = "/api/OperationsHistory/" 
  constructor(private _http:HttpClient) { }

  getOperationsHistory(currentPage: number, pageSize: number): Observable<OperationDataList> {
    return this._http.get<OperationDataList>(`${this.baseUrl}GetOperationsHistory/${pageSize}/${currentPage}`)
  }
  getSecondSideAccountInfo(accountID:number): Observable<SecondSideAccountInfo> {
    return this._http.get<SecondSideAccountInfo>(`api/Account/GetSecondSideAccountInfo/${accountID}`)
  }
  CreateOperationsHistoriesPDF(month:number,year: number): Observable<any>{
    return this._http.get(`${this.baseUrl}GetOperationsHistoryAsPDF/${month}/${year}`);
  }
}
