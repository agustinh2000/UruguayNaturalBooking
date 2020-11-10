import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginUser } from '../../models/loginUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  informationOfUserToLogin: LoginUser;

  public formGroup: FormGroup;

  constructor( private formBuilder: FormBuilder) {
    this.formGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  public Login(): void{
    this.informationOfUserToLogin = new LoginUser(this.formGroup.value);

    window.alert(JSON.parse(this.informationOfUserToLogin.Email));
  }

}
