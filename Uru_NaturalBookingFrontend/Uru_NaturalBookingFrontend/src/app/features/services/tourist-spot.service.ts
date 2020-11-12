import { Injectable } from '@angular/core';
import { TouristSpotForRequestModel } from 'src/app/models/TouristSpotForRequestModel';

@Injectable({
  providedIn: 'root'
})
export class TouristSpotService {

  constructor() { }

  Add(touristSpotToAdd: TouristSpotForRequestModel): TouristSpotForRequestModel {
    return;
    // this is a call to the service in the webAPI to the method POST of TouristSpotController
  }
}
