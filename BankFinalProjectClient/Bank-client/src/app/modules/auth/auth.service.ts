import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Login } from 'src/app/models/login.model';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = "/api/Auth";

  constructor(private _http:HttpClient) { }

  login(login: Login):Observable<Number>{
    return this._http.post<Number>(`${this.baseUrl}/Login`,login)
  }
}
