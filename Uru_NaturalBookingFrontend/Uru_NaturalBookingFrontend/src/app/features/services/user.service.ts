import { Identifiers } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { UserModelForLoginRequest } from 'src/app/models/UserModelForLoginRequest';
import { UserModelForResponse } from 'src/app/models/UserModelForResponse';
import { UserModelForRequest } from '../../models/UserModelForRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly users: UserModelForResponse[] = [
    {
      Id: '5016bb05-4871-4079-a430-882b637e421a',
      UserName: 'Joaco00',
      Mail: 'joaquin.lamela00@gmail.com'
    },
    {
      Id: 'b198d786-9b5b-4ea0-923a-6b21021ac19d',
      UserName: 'agustinh00',
      Mail: 'agustinhernandorena@gmail.com'
    },
    {
      Id: 'cf688384-ab9e-4520-89c7-c059b875eec2',
      UserName: 'MartinG',
      Mail: 'rgskinks@gmail.com'
    },
    {
      Id: '36d6c3e6-87e2-44d1-a282-11c3abe1160f',
      UserName: 'MartinGuto',
      Mail: 'martin.gut@hotmail.com'
    }
  ];

  constructor() { }

  Login(userInformationToLogin: UserModelForLoginRequest): UserModelForResponse {
    return;
    // this is a call to the service in the webAPI to the method LOGIN of UserController
  }

  Register(userInformationToRegister: UserModelForRequest): UserModelForResponse {
    return;
    // this is a call to the service in the webAPI to the method POST of UserController
  }

  getUsersOfSystem(): UserModelForResponse[] {
    const usersObteined: UserModelForResponse[] = [];
    for (const user of this.users) {
      usersObteined.push(user);
    }
    return usersObteined;
  }

  getUserById(userId: string): UserModelForRequest{
    return ;
    // this is a call to the service in the webAPI to the method Get(Guid id)
  }

  deleteUser(userId: string): void{
    return;
      // this is a call to the service in the webAPI to the method Delete(Guid id);
  }



}
