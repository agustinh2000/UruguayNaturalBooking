import { Injectable } from '@angular/core';
import { UserModelForLoginRequest} from 'src/app/models/UserModelForLoginRequest';
import { UserModelForResponse } from 'src/app/models/UserModelForResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  Login(userInformationToLogin: UserModelForLoginRequest): UserModelForResponse {
    return;
    // this is a call to the service in the webAPI implemented in a few days;
  }
}
