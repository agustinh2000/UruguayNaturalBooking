import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Identifiers } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginModelForResponse } from 'src/app/models/LoginModelForResponse';
import { UserModelForLoginRequest } from 'src/app/models/UserModelForLoginRequest';
import { UserModelForResponse } from 'src/app/models/UserModelForResponse';
import { environment } from 'src/environments/environment';
import { UserModelForRequest } from '../../models/UserModelForRequest';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  uri = `${environment.baseUrl}api/users`;

  readonly users: UserModelForResponse[] = [
    {
      Id: '5016bb05-4871-4079-a430-882b637e421a',
      UserName: 'Joaco00',
      Mail: 'joaquin.lamela00@gmail.com',
    },
    {
      Id: 'b198d786-9b5b-4ea0-923a-6b21021ac19d',
      UserName: 'agustinh00',
      Mail: 'agustinhernandorena@gmail.com',
    },
    {
      Id: 'cf688384-ab9e-4520-89c7-c059b875eec2',
      UserName: 'MartinG',
      Mail: 'rgskinks@gmail.com',
    },
    {
      Id: '36d6c3e6-87e2-44d1-a282-11c3abe1160f',
      UserName: 'MartinGuto',
      Mail: 'martin.gut@hotmail.com',
    },
  ];

  constructor(private http: HttpClient) {}

  login(
    userInformationToLogin: UserModelForLoginRequest
  ): Observable<LoginModelForResponse> {
    return this.http.post<LoginModelForResponse>(
      `${this.uri}/login`,
      userInformationToLogin
    );
  }

  Register(
    userInformationToRegister: UserModelForRequest
  ): UserModelForResponse {
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

  getUserById(userId: string): UserModelForRequest {
    return;
    // this is a call to the service in the webAPI to the method Get(Guid id)
  }

  deleteUser(userId: string): void {
    return;
    // this is a call to the service in the webAPI to the method Delete(Guid id);
  }

  isLogued(): boolean {
    const token = localStorage.token;
    return token != null && token !== undefined && token !== '';
  }

  logout(): Observable<string> {
    let myHeaders = new HttpHeaders();
    const tokenValue = localStorage.token;
    myHeaders = myHeaders.append('token', localStorage.token);
    myHeaders = myHeaders.append('Accept', 'application/text');
    return this.http.delete<string>(`${this.uri}/logout`, {
      headers: myHeaders,
      responseType: 'text' as 'json',
    });
  }
}
