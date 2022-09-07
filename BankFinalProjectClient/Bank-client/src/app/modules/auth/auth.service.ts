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

  login(login: Login):Observable<number>{
    return this._http.post<number>(`${this.baseUrl}/Login`,login)
  }
}
