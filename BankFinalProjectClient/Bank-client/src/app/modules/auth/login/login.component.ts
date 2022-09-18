import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
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

  constructor(private _authService: AuthService, private _router: Router, private _userService: UserService, private _snackBar: MatSnackBar) { }

  hide = true;
  subscription!: Subscription;
  loading!: boolean;

  loginForm: FormGroup = new FormGroup({
    "email": new FormControl("", [Validators.required, Validators.email, Validators.maxLength(40)]),
    "password": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(25)]),
  })

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 4000
    });
  }

  Login() {
    this.loading = true;
    this.subscription = this._authService.login(this.loginForm.value).subscribe(data => {
      this.loading = false;
      this._userService.setAuthUser(data);
      this._router.navigate(['/operationHistory/operationsHistoryList']);
    }, error => {
      error.status == 401 ?
        this.openSnackBar("One or more details wrong, if you are a new customer - create a new account", "close")
        : this.openSnackBar("Login failed,Try again later", "close");
    });
  };

  createAnAccount() {
    this._router.navigate(['/createAccount']);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
