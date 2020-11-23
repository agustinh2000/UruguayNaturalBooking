import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControl,
  FormGroupDirective,
  NgForm,
} from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserModelForResponse } from '../../models/UserModelForResponse';
import { MatSelectChange } from '@angular/material/select';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css'],
})
export class DeleteUserComponent implements OnInit {
  private serviceUser: UserService;

  public formGroup: FormGroup;

  public usersOfTheSystem;

  userSelectedInModelOfRequest: UserModelForResponse;

  private idOfUserToDelete: string;

  constructor(
    private formBuilder: FormBuilder,
    private serviceUserPassed: UserService,
    private router: Router
  ) {
    this.usersOfTheSystem = new Array<UserModelForResponse>();
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      userName: [''],
      email: [''],
    });
  }

  ngOnInit(): void {
    this.serviceUser.getUsersOfSystem().subscribe(
      (res) => {
        this.usersOfTheSystem = res;
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
  }

  public chargeInfoInFields(event: MatSelectChange): void {
    this.idOfUserToDelete = event.value;
    this.serviceUser.getUserById(this.idOfUserToDelete).subscribe(
      (res) => {
        this.userSelectedInModelOfRequest = res;
        this.chargeInfoInForm();
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
  }

  private chargeInfoInForm(): void {
    this.formGroup.controls.userName.setValue(
      this.userSelectedInModelOfRequest.userName
    );
    this.formGroup.controls.email.setValue(
      this.userSelectedInModelOfRequest.mail
    );
  }

  public delete(): void {
    this.serviceUser.deleteUser(this.idOfUserToDelete).subscribe(
      (res) => {
        alert('Se ha eliminado correctamente el usuario.');
        this.router.navigate(['/regions']);
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
  }
}
