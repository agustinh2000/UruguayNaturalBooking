import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LodgingForSearchModel } from 'src/app/models/LodgingForSearchModel';
import { SearchOfLodgingModelForRequest } from 'src/app/models/SearchOfLodgingModelForRequest';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SearchOfLodgingsService {
  uri = `${environment.baseUrl}api/searchOfLodgings`;

  constructor(private http: HttpClient) {}

  getLodgingsOfSearch(
    search: SearchOfLodgingModelForRequest
  ): Observable<LodgingForSearchModel> {
    return this.http.post<LodgingForSearchModel>(this.uri, search);
  }
}
