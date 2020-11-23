import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormControl,
  FormGroupDirective,
  NgForm,
  ValidatorFn,
} from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserModelForRequest } from '../../models/UserModelForRequest';
import { UserModelForResponse } from '../../models/UserModelForResponse';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modify-user',
  templateUrl: './modify-user.component.html',
  styleUrls: ['./modify-user.component.css'],
})
export class ModifyUserComponent implements OnInit {
  usersOfTheSystem;

  informationOfUserToRegister: UserModelForRequest;

  public formGroup: FormGroup;

  private serviceUser: UserService;

  selected = new FormControl([Validators.required]);

  userSelectedInModelOfRequest: UserModelForResponse;

  public hide: boolean = true;

  public selectedUser: FormControl = new FormControl('', [Validators.required]);

  constructor(
    private formBuilder: FormBuilder,
    private serviceUserPassed: UserService,
    private router: Router
  ) {
    this.usersOfTheSystem = new Array();
    this.userSelectedInModelOfRequest = null;
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      lastName: ['', [Validators.required, this.noWhitespaceValidator]],
      userName: ['', [Validators.required, this.noWhitespaceValidator]],
      mail: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, this.noWhitespaceValidator]],
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

  public chargeInfoInFields(idUserSelected: string): void {
    this.serviceUser.getUserById(idUserSelected).subscribe(
      (res) => {
        this.result(res);
        this.chargeInfoInForm();
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
  }

  private chargeInfoInForm(): void {
    this.formGroup = this.formBuilder.group({
      name: [
        this.userSelectedInModelOfRequest.name,
        [Validators.required, this.noWhitespaceValidator],
      ],
      lastName: [
        this.userSelectedInModelOfRequest.lastName,
        [Validators.required, this.noWhitespaceValidator],
      ],
      userName: [
        this.userSelectedInModelOfRequest.userName,
        [Validators.required, this.noWhitespaceValidator],
      ],
      mail: [
        this.userSelectedInModelOfRequest.mail,
        [Validators.required, Validators.email],
      ],
      password: [
        this.userSelectedInModelOfRequest.password,
        [Validators.required, this.noWhitespaceValidator],
      ],
    });
  }

  private result(data: UserModelForResponse): void {
    this.userSelectedInModelOfRequest = {
      name: data.name,
      lastName: data.lastName,
      userName: data.userName,
      mail: data.mail,
      id: data.id,
      password: data.password,
    };
  }

  public modify(): void {
    this.informationOfUserToRegister = new UserModelForRequest(
      this.formGroup.value
    );
    /*this.serviceUser.modify(this.informationOfUserToRegister, this.selectedUser.id).subscribe(
      (res: UserModelForResponse) => {
        console.log(res);
        this.router.navigate(['/regions']);
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
    */
  }

  getErrorMessage(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace')
      ? 'Error. El nombre ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageLastName(): string {
    if (this.formGroup.controls.lastName.hasError('required')) {
      return 'Error. El apellido es requerido.';
    }
    return this.formGroup.controls.lastName.hasError('whitespace')
      ? 'Error. El apellido ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageUserName(): string {
    if (this.formGroup.controls.userName.hasError('required')) {
      return 'Error. El nombre de usuario es requerido.';
    }
    return this.formGroup.controls.userName.hasError('whitespace')
      ? 'Error. El nombre de usuario ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageEmail(): string {
    if (this.formGroup.controls.mail.hasError('required')) {
      return 'Error. El email es requerido.';
    }
    return this.formGroup.controls.mail.hasError('email')
      ? 'Error. El email ingresado debe tener un formato valido.'
      : '';
  }

  getErrorMessagePassword(): string {
    if (this.formGroup.controls.password.hasError('required')) {
      return 'Error. La contraseña es requerida.';
    }
    return this.formGroup.controls.password.hasError('whitespace')
      ? 'Error. La contraseña ingresada no puede ser vacía.'
      : '';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };
}
