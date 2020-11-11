import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserModelForRequest } from '../../models/UserModelForRequest';
import { UserModelForResponse } from '../../models/UserModelForResponse';

@Component({
  selector: 'app-modify-user',
  templateUrl: './modify-user.component.html',
  styleUrls: ['./modify-user.component.css']
})
export class ModifyUserComponent implements OnInit {

  usersOfTheSystem: UserModelForResponse [];

  informationOfUserToRegister: UserModelForRequest;

  public formGroup: FormGroup;

  private serviceUser: UserService;

  selected = new FormControl([Validators.required]);

  selectedUser: UserModelForResponse;

  userSelectedInModelOfRequest: UserModelForRequest;

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
    this.usersOfTheSystem = this.serviceUser.getUsersOfSystem();
  }

  public Register(): void {
    this.informationOfUserToRegister = new UserModelForRequest(this.formGroup.value);
    this.serviceUser.Register(this.informationOfUserToRegister);
  }

  public ChargeInfoInFields(): void{
    this.userSelectedInModelOfRequest = this.serviceUser.getUserById(this.selectedUser.Id);
    this.formGroup = this.formBuilder.group({
      name: [this.userSelectedInModelOfRequest.Name, [Validators.required]],
      lastName: [this.userSelectedInModelOfRequest.LastName, [Validators.required]],
      userName: [this.userSelectedInModelOfRequest.UserName, [Validators.required]],
      email: [this.userSelectedInModelOfRequest.Mail, [Validators.required, Validators.email]],
      password: [this.userSelectedInModelOfRequest.Password, [Validators.required]],
    });
  }
}
