import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserModelForRequest } from '../../models/UserModelForRequest';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  informationOfUserToRegister: UserModelForRequest;

  public formGroup: FormGroup;

  private serviceUser: UserService;

  constructor(private formBuilder: FormBuilder, private serviceUserPassed: UserService) {
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      userName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  public Register(): void {
    this.informationOfUserToRegister = new UserModelForRequest(this.formGroup.value);
    this.serviceUser.Register(this.informationOfUserToRegister);
  }

}
