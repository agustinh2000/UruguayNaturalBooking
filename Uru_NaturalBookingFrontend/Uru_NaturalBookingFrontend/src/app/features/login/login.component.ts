import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModelForResponse } from 'src/app/models/LoginModelForResponse';
import { UserModelForResponse } from 'src/app/models/UserModelForResponse';
import { UserModelForLoginRequest } from '../../models/UserModelForLoginRequest';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  informationOfUserToLogin: UserModelForLoginRequest;

  public formGroup: FormGroup;

  private serviceUser: UserService;

  public hide: boolean = true;

  constructor(
    private formBuilder: FormBuilder,
    private serviceUserPassed: UserService,
    private router: Router
  ) {
    this.serviceUser = serviceUserPassed;
    this.formGroup = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, this.noWhitespaceValidator]],
    });
  }

  ngOnInit(): void {}

  getErrorMessage(): string {
    if (this.formGroup.controls.email.hasError('required')) {
      return 'Error. El email es requerido.';
    }
    return this.formGroup.controls.email.hasError('email')
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

  public login(): void {
    this.informationOfUserToLogin = new UserModelForLoginRequest(
      this.formGroup.value
    );
    this.serviceUser.login(this.informationOfUserToLogin).subscribe(
      (res: LoginModelForResponse) => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('idUser', res.idUser);
        this.router.navigate(['/regions']);
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
  }
}
