import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  customerForm!: FormGroup
  hide = true;

  constructor(private _accuntService: AccountService,private _router:Router) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(): void {
    this.customerForm = new FormGroup({
      "firstName": new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      "lastName": new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      "email": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(100), Validators.email]),
      "password": new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(25)]),
    })
  }

  createAnAccount(): void {
    this._accuntService.createAnAccount(this.customerForm.value).subscribe((data: any) => {
      if(data){
        alert("Account created successfully!");
        this._router.navigate(['/login']);
      }
     // data ? alert("Account created successfully!"):alert("oops..., something went wrong try again");
    },(error: { message: string; }) => alert("Error creating account: " + error.message));
  }
}
