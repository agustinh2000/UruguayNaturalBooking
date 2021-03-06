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

  readonly users: UserModelForResponse[] = [];

  constructor(private http: HttpClient) {}

  login(
    userInformationToLogin: UserModelForLoginRequest
  ): Observable<LoginModelForResponse> {
    return this.http.post<LoginModelForResponse>(
      `${this.uri}/login`,
      userInformationToLogin
    );
  }

  register(
    userToRegister: UserModelForRequest
  ): Observable<UserModelForResponse> {
    const myHeaders = this.defineHeaders();
    return this.http.post<UserModelForResponse>(this.uri, userToRegister, {
      headers: myHeaders,
    });
  }

  modify(
    userModified: UserModelForRequest,
    idOfUserToModify: string
  ): Observable<UserModelForResponse> {
    const myHeaders = this.defineHeaders();
    return this.http.put<UserModelForResponse>(
      `${this.uri}/${idOfUserToModify}`,
      userModified,
      {
        headers: myHeaders,
      }
    );
  }

  getUserById(userId: string): Observable<UserModelForResponse> {
    const myHeaders = this.defineHeaders();
    return this.http.get<UserModelForResponse>(`${this.uri}/${userId}`, {
      headers: myHeaders,
    });
  }

  getUsersOfSystem(): Observable<UserModelForResponse> {
    const myHeaders = this.defineHeaders();
    return this.http.get<UserModelForResponse>(this.uri, {
      headers: myHeaders,
    });
  }

  deleteUser(userId: string): Observable<{}> {
    if (localStorage.idUser === userId) {
      localStorage.removeItem('idUser');
    }
    const myHeaders = this.defineHeaders();
    const result = this.http.delete(`${this.uri}/${userId}`, {
      headers: myHeaders,
      responseType: 'text' as 'json',
    });
    localStorage.removeItem('token');
    return result;
  }

  isLogued(): boolean {
    const token = localStorage.token;
    return token != null && token !== undefined && token !== '';
  }

  logout(): Observable<string> {
    const myHeaders = this.defineHeaders();
    return this.http.delete<string>(`${this.uri}/logout`, {
      headers: myHeaders,
      responseType: 'text' as 'json',
    });
  }

  private defineHeaders(): HttpHeaders {
    let myHeaders = new HttpHeaders();
    if (this.isLogued()) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return myHeaders;
  }
}
