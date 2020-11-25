import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Parameter } from 'src/app/models/Parameter';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ImporterService {
  uri = `${environment.baseUrl}api/imports`;
  constructor(private http: HttpClient) {}

  getImportersName(): Observable<string> {
    const headersToImport = this.defineHeaders();
    return this.http.get<string>(this.uri, {
      headers: headersToImport,
    });
  }

  importLodgings(
    informationForTheImporters: Parameter[],
    nameOfImporter: string
  ): Observable<string> {
    const headersToImport = this.defineHeaders();
    return this.http.post<string>(this.uri, informationForTheImporters, {
      params: {
        pathOfDll: nameOfImporter,
      },
      headers: headersToImport,
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

  private isLogued(): boolean {
    const token = localStorage.token;
    return token != null && token !== undefined && token !== '';
  }
}
