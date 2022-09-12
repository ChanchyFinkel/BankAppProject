import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {

  constructor(private _authService: AuthService, private _router: Router, private _userService: UserService) { }

  hide = true;
  subscription!: Subscription;

  loginForm: FormGroup = new FormGroup({
    "email": new FormControl("", [Validators.required, Validators.email, Validators.maxLength(40)]),
    "password": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(25)]),
  })

  loading!: boolean;

  Login() {
    this.loading = true;
    this.subscription = this._authService.login(this.loginForm.value).subscribe(data => {
      this.loading = false;
      this._userService.setAuthUser(data);
      this._router.navigate(['/operationsHistory']);
    }, error => {
      error.status == 401 ? alert("The identification process failed, if you are a new customer - create a new account, otherwise try again") : alert("Login failed,Try again later");
    });
  };

  createAnAccount() {
    this._router.navigate(['/createAnAccount']);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
