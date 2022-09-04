import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  customerForm!: FormGroup
  constructor(private _accuntService: AccountService) { }

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

  createAccount(): void {
    this._accuntService.createAccount(this.customerForm.value).subscribe(data => {
      if (data) {
        alert("Account created successfully!");
      }
      else {
        alert("oops..., something went wrong try agin!");
      }
    });
  }
}
