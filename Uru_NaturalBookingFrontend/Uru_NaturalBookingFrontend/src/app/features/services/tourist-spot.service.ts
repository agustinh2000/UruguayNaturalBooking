import { Injectable } from '@angular/core';
import { TouristSpotForRequestModel } from 'src/app/models/TouristSpotForRequestModel';
import { TouristSpotModelForLodgingResponseModel } from '../../models/TouristSpotModelForLodgingResponseModel';

@Injectable({
  providedIn: 'root'
})
export class TouristSpotService {

  readonly touristSpots: TouristSpotModelForLodgingResponseModel[] = [
    {
      Id: '13046b7e-3d83-4576-b459-65c4c965b037',
      Name: 'Punta del este'
    },

    {
      Id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      Name: 'San Ramón'
    },

    {
      Id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      Name: 'Atlantida'
    },

    {
      Id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      Name: 'Las Toscas'
    },

    {
      Id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      Name: 'La floresta'
    },
  ];

  constructor() { }

  getTouristSpots(): TouristSpotModelForLodgingResponseModel[] {
    const touristSpotObteined: TouristSpotModelForLodgingResponseModel[] = [];
    for (const touristSpot of this.touristSpots) {
      touristSpotObteined.push(touristSpot);
    }
    return touristSpotObteined;
  }

  Add(touristSpotToAdd: TouristSpotForRequestModel) { return; }
}
