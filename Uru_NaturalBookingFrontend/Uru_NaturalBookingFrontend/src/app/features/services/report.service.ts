import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ReportModel } from '../../models/ReportModel';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  uri = `${environment.baseUrl}api/reports`;

  constructor(private http: HttpClient) { }

  getLodgingsForReport(
    checkIn: Date, checkOut: Date, touristSpotId: string
  ): Observable<ReportModel> {
    let myHeaders = new HttpHeaders();
    if (localStorage.token !== undefined) {
      myHeaders = myHeaders.append('token', localStorage.token);
    }
    return this.http.get<ReportModel>(
      `${this.uri}/report`,
      {
        params: {
          idOfTouristSpot: touristSpotId,
          checkInMax: checkIn.toDateString(),
          checkOutMax: checkOut.toDateString(),
        },
        headers: myHeaders,
      }
    );
  }
}
