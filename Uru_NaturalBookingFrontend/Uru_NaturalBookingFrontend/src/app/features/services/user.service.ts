import { Injectable } from '@angular/core';
import { UserModelForLoginRequest} from 'src/app/models/UserModelForLoginRequest';
import { UserModelForResponse } from 'src/app/models/UserModelForResponse';
import { UserModelForRequest } from '../../models/UserModelForRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  Login(userInformationToLogin: UserModelForLoginRequest): UserModelForResponse {
    return;
    // this is a call to the service in the webAPI to the method LOGIN of UserController
  }

  Register(userInformationToRegister: UserModelForRequest): UserModelForResponse{
    return;
    // this is a call to the service in the webAPI to the method POST of UserController
  }
}
