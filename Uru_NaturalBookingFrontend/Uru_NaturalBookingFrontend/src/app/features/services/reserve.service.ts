import { Injectable } from '@angular/core';
import { LodgingModelForReserveResponseModel } from 'src/app/models/LodgingModelForReserveResponseModel';
import { ReserveState } from 'src/app/models/ReserveState';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { DescriptionOfState } from '../../models/ReserveState';
import { ReserveModelForRequestUpdate } from '../../models/ReserveModelForRequestUpdate';
import { ReserveModelForRequest } from 'src/app/models/ReserveModelForRequest';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReserveService {
  uri = `${environment.baseUrl}api/reserves`;

  reserveExist(reserveId: string): boolean {
    return true;
    // this is a call to the ReserveController in the webAPI to get the reserve by the id,
    // and in this case if we get a 200 means that reserve exist, in other case not.
  }

  constructor(private http: HttpClient) {}

  getReserveById(reserveId: string): Observable<ReserveModelForResponse> {
    return this.http.get<ReserveModelForResponse>(`${this.uri}/${reserveId}`);
  }

  updateReserve(
    reserveModelForUpdate: ReserveModelForRequestUpdate
  ): ReserveModelForResponse {
    return;
    // this is a call to the ReserveController in the webAPI to update the reserve passed in the parameter
    // and return the reserve update in ReserveModelForResponse
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
