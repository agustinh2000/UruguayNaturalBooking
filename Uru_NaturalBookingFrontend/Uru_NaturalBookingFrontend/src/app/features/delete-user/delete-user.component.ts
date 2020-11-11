import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserModelForRequest } from '../../models/UserModelForRequest';
import { UserModelForResponse } from '../../models/UserModelForResponse';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {

  private serviceUser: UserService;

  public formGroup: FormGroup;

  usersOfTheSystem: UserModelForResponse [];

  selected = new FormControl([Validators.required]);

  selectedUser: UserModelForResponse;

  userSelectedInModelOfRequest: UserModelForRequest;


  constructor(private formBuilder: FormBuilder, private serviceUserPassed: UserService) {
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      name: [''],
      lastName: [''],
      userName: [''],
      email: [''],
      password: [''],
    });
  }

  ngOnInit(): void {
    this.usersOfTheSystem = this.serviceUser.getUsersOfSystem();
  }

  public ChargeInfoInFields(): void{
    this.userSelectedInModelOfRequest = this.serviceUser.getUserById(this.selectedUser.Id);
    this.formGroup.controls.userName.setValue(this.userSelectedInModelOfRequest.UserName);
    this.formGroup.controls.email.setValue(this.userSelectedInModelOfRequest.Mail);
  }

  public Delete(): void {
    this.serviceUser.deleteUser(this.selectedUser.Id);
  }
}
