import { Injectable } from '@angular/core';
import { LodgingModelForResponse } from 'src/app/models/LodgingModelForResponse';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';

@Injectable({
  providedIn: 'root'
})
export class LodgingService {

  readonly lodgings: LodgingModelForResponse[] = [];

  constructor() { }

  getLodgings(): LodgingModelForResponse[] {
    const lodgingsObteined: LodgingModelForResponse[] = [];
    for (const lodging of this.lodgings){
      lodgingsObteined.push(lodging);
    }
    return lodgingsObteined;
  }

  CreateLodging(lodgingToCreate: LodgingModelForRequest): LodgingModelForResponse {
    return;
    // this is a call to the service in the webAPI to the method POST of LodgingController
  }
}
