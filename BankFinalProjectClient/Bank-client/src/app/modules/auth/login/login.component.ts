import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/login.model';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private _authService: AuthService, private router: Router) { }

  hide = true;
  // login:Login=new Login();

  loginForm: FormGroup = new FormGroup({
    "email": new FormControl("", [Validators.required,Validators.email,Validators.maxLength(40)]),
    "password": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(25)]),
  })

  Login() {
    this._authService.login(this.loginForm.value).subscribe(data => {
      alert(data);
    });
    // this.userAdmin = this.loginForm.value;
    // this.userAdmin.name = this.userAdmin.name.replace(/\s/g, '');
    // this.userAdmin.password = this.userAdmin.password.replace(/\s/g, '');
    // this._loginService.getAdmin(this.userAdmin).subscribe((data: any) => {
    //   if (data) {
    //     this.userAdminDTO = data;
    //     console.log(this.userAdminDTO);
    //     alert("Welcome to " + this.userAdminDTO.name);
    //     // this._userService.setAuthorized(true);
    //     // this._userService.setUserAdmin(data);
     
    //     this.router.navigate(['/calendar']);
    //   } 
    //   else { console.log("no such user"); }
    // })
  };

  ngOnInit(): void {
    // this.loginForm = new FormGroup({
    //   name: new FormControl("", [Validators.required, Validators.email]),
    //   password: new FormControl("", [Validators.required, Validators.minLength(8)]),
    // });
  }
}
