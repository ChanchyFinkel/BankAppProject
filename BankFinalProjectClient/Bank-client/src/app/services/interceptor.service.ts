import { Injectable, OnDestroy } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor, OnDestroy {

  constructor(private _userService: UserService) { }

  token: string="";
  subscription!: Subscription;

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.subscription=this._userService.getAuthUser().subscribe(data => {
      if (data) {
        this.token = data.token;
      }
    });
    if (this.token!="") {
      const tokenizedReq = req.clone({ headers: req.headers.set('Authorization', 'Bearer ' + this.token) });
      return next.handle(tokenizedReq);
    }
    return next.handle(req);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}