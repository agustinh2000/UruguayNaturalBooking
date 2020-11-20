import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
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

  public hide: boolean = true;

  constructor(private formBuilder: FormBuilder, private serviceUserPassed: UserService) {
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      lastName: ['', [Validators.required, this.noWhitespaceValidator]],
      userName: ['', [Validators.required, this.noWhitespaceValidator]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, this.noWhitespaceValidator]],
    });
  }

  ngOnInit(): void {
  }

  public Register(): void {
    this.informationOfUserToRegister = new UserModelForRequest(this.formGroup.value);
    this.serviceUser.Register(this.informationOfUserToRegister);
  }

  getErrorMessage(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace') ? 'Error. El nombre ingresado no puede ser vacío.' : '';
  }

  getErrorMessageLastName(): string{
    if (this.formGroup.controls.lastName.hasError('required')) {
      return 'Error. El apellido es requerido.';
    }
    return this.formGroup.controls.lastName.hasError('whitespace') ? 'Error. El apellido ingresado no puede ser vacío.' : '';
  }

  getErrorMessageUserName(): string{
    if (this.formGroup.controls.userName.hasError('required')) {
      return 'Error. El nombre de usuario es requerido.';
    }
    return this.formGroup.controls.userName.hasError('whitespace') ? 'Error. El nombre de usuario ingresado no puede ser vacío.' : '';
  }

  getErrorMessageEmail(): string {
    if (this.formGroup.controls.email.hasError('required')) {
      return 'Error. El email es requerido.';
    }
    return this.formGroup.controls.email.hasError('email') ? 'Error. El email ingresado debe tener un formato valido.' : '';
  }

  getErrorMessagePassword(): string{
    if (this.formGroup.controls.password.hasError('required')) {
      return 'Error. La contraseña es requerida.';
    }
    return this.formGroup.controls.password.hasError('whitespace') ? 'Error. La contraseña ingresada no puede ser vacía.' : '';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }
}
