import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReviewModelForResponse } from 'src/app/models/ReviewModelForResponse';
import { environment } from 'src/environments/environment';
import { ReviewModelForRequest } from '../../models/ReviewModelForRequest';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  uri = `${environment.baseUrl}api/reviews`;


  constructor(private http: HttpClient) { }

  comment(
    review: ReviewModelForRequest
  ): Observable<ReviewModelForResponse> {
    return this.http.post<ReviewModelForResponse>(
      this.uri,
      review
    );
  }
  
}
