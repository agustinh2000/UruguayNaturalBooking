import { Injectable } from '@angular/core';
import { LodgingModelForReserveResponseModel } from 'src/app/models/LodgingModelForReserveResponseModel';
import { ReserveState } from 'src/app/models/ReserveState';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { DescriptionOfState } from '../../models/ReserveState';
import { ReserveModelForRequestUpdate } from '../../models/ReserveModelForRequestUpdate';
import { ReserveModelForRequest } from 'src/app/models/ReserveModelForRequest';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReserveService {
  uri = `${environment.baseUrl}api/reserves`;

  constructor(private http: HttpClient) {}

  getReserveById(reserveId: string): Observable<ReserveModelForResponse> {
    return this.http.get<ReserveModelForResponse>(`${this.uri}/${reserveId}`);
  }

  updateReserve(
    reserveModelForUpdate: ReserveModelForRequestUpdate,
    reserveId: string
  ): Observable<ReserveModelForResponse> {
    const headersForUpdate = this.defineHeaders();
    return this.http.put<ReserveModelForResponse>(
      `${this.uri}/${reserveId}`,
      reserveModelForUpdate,
      { headers: headersForUpdate }
    );
  }

  private hasUserLogued(): boolean {
    const token = localStorage.token;
    return token != null && token !== undefined && token !== '';
  }

  private defineHeaders(): HttpHeaders {
    let myHeaders = new HttpHeaders();
    if (this.hasUserLogued()) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return myHeaders;
  }

  createReserve(
    reserveModelForRequest: ReserveModelForRequest
  ): Observable<ReserveModelForResponse> {
    return this.http.post<ReserveModelForResponse>(
      this.uri,
      reserveModelForRequest
    );
  }
}
