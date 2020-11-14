import { Injectable } from '@angular/core';
import { ReviewModelForResponse } from 'src/app/models/ReviewModelForResponse';
import { ReviewModelForRequest } from '../../models/ReviewModelForRequest';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor() { }

  comment(review: ReviewModelForRequest): ReviewModelForResponse {
    return;
    // this is a call into the method of webAPI in controller ReviewController POST
  }

}
