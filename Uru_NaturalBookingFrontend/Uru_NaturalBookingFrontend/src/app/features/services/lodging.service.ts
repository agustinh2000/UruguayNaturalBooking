import { Injectable } from '@angular/core';
import { LodgingModelForResponse } from 'src/app/models/LodgingModelForResponse';
import { LodgingModelForSearchResponse } from 'src/app/models/LodgingModelForSearchResponse';
import { TouristSpotModelForLodgingResponseModel } from 'src/app/models/TouristSpotModelForLodgingResponseModel';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';
import { LodgingForSearchModel } from '../../models/LodgingForSearchModel';
import { SearchOfLodgingModelForRequest } from '../../models/SearchOfLodgingModelForRequest';
import { ReviewModelForResponse } from '../../models/ReviewModelForResponse';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoginModelForResponse } from 'src/app/models/LoginModelForResponse';

@Injectable({
  providedIn: 'root',
})
export class LodgingService {
  uri = `${environment.baseUrl}api/lodgings`;

  constructor(private http: HttpClient) {}

  getLodgingById(lodgingId: string): Observable<LodgingModelForResponse> {
    return this.http.get<LodgingModelForResponse>(`${this.uri}/${lodgingId}`);
  }

  getLodgings(): Observable<LodgingModelForResponse> {
    return this.http.get<LodgingModelForResponse>(this.uri);
  }

  modify(
    id: string,
    lodgingModified: LodgingModelForRequest
  ): Observable<LodgingModelForResponse> {
    let myHeaders = new HttpHeaders();
    if (localStorage.token !== undefined) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return this.http.put<LodgingModelForResponse>(
      `${this.uri}/${id}`,
      lodgingModified,
      {
        headers: myHeaders,
      }
    );
  }
  delete(id: string): Observable<{}> {
    let myHeaders = new HttpHeaders();
    if (localStorage.token !== undefined) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return this.http.delete(`${this.uri}/${id}`, {
      headers: myHeaders,
      responseType: 'text' as 'json',
    });
  }

  add(
    lodgingToAdd: LodgingModelForRequest
  ): Observable<LodgingModelForResponse> {
    let myHeaders = new HttpHeaders();
    if (localStorage.token !== undefined) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return this.http.post<LodgingModelForResponse>(this.uri, lodgingToAdd, {
      headers: myHeaders,
    });
  }
}
