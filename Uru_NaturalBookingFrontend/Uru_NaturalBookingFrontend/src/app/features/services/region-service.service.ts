import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Region } from '../../models/Region';

@Injectable({
  providedIn: 'root',
})
export class RegionServiceService {
  uri = `${environment.baseUrl}api/regions`;
  constructor(private http: HttpClient) {}

  getRegions(): Observable<Region> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.append('Accept', 'application/json');
    return this.http.get<Region>(this.uri, {
      headers: myHeaders,
    });
  }

  getRegionById(idRegion: string): Observable<Region> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.append('Accept', 'application/json');
    myHeaders = myHeaders.append('Accept', 'application/text');
    return this.http.get<Region>(`${this.uri}/${idRegion}`, {
      headers: myHeaders,
      responseType: 'text' as 'json',
    });
  }
}
