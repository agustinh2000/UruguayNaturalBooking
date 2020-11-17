import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserModelForLoginRequest } from '../../models/UserModelForLoginRequest';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  informationOfUserToLogin: UserModelForLoginRequest;

  public formGroup: FormGroup;

  private serviceUser: UserService;

  constructor(private formBuilder: FormBuilder, private serviceUserPassed: UserService) {
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  public Login(): void {
    this.informationOfUserToLogin = new UserModelForLoginRequest(this.formGroup.value);
    this.serviceUser.Login(this.informationOfUserToLogin);
  }
}