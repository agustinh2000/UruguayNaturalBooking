import { Injectable } from '@angular/core';
import { LodgingModelForResponse } from 'src/app/models/LodgingModelForResponse';
import { LodgingModelForSearchResponse } from 'src/app/models/LodgingModelForSearchResponse';
import { TouristSpotModelForLodgingResponseModel } from 'src/app/models/TouristSpotModelForLodgingResponseModel';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';
import { LodgingForSearchModel } from '../../models/LodgingForSearchModel';
import { SearchOfLodgingModelForRequest } from '../../models/SearchOfLodgingModelForRequest';

@Injectable({
  providedIn: 'root'
})
export class LodgingService {

  readonly lodgings: LodgingModelForResponse[] = [
    {
      Id: '4bd1d316-b8eb-42f3-8e76-af41b2687a10',
      Name: 'Enjoy',
      Description: 'Un hotel increible',
      QuantityOfStars: 5,
      IsAvailable: true,
      ImagesPath: ['../../assets/img/enjoy.jpg'],
      LodgingTouristSpotModel: null,
      ReviewsForLodging: null,
      Address: 'Punta del Este',
      PricePerNight: 120,
      ReviewsAverageScore: 4,
    },
    {
      Id: '502c715e-33fc-4df9-94f6-bd62a6be3d14',
      Name: 'Sheraton',
      Description: 'Un hotel genial',
      QuantityOfStars: 5,
      IsAvailable: false,
      ImagesPath: ['../../assets/img/sheraton.jpg'],
      LodgingTouristSpotModel: null,
      ReviewsForLodging: null,
      Address: 'Colonia',
      PricePerNight: 120,
      ReviewsAverageScore: 4,
    },
    {
      Id: '5690844e-ca55-4cea-a4d1-2e370fa147dc',
      Name: 'Ibis',
      Description: 'Un hotel increible',
      QuantityOfStars: 3,
      IsAvailable: false,
      ImagesPath: ['../../assets/img/ibis.jpg'],
      LodgingTouristSpotModel: null,
      ReviewsForLodging: null,
      Address: 'Montevideo',
      PricePerNight: 120,
      ReviewsAverageScore: 4,
    },
    {
      Id: '28311417-7c70-4bae-9295-571fab4efe50',
      Name: 'Arapey Thermal Resort',
      Description: 'Un hotel fantastico',
      QuantityOfStars: 5,
      IsAvailable: true,
      ImagesPath: ['../../assets/img/arapey.jpg'],
      LodgingTouristSpotModel: null,
      ReviewsForLodging: null,
      Address: 'Montevideo',
      PricePerNight: 120,
      ReviewsAverageScore: 4,
    },
    {
      Id: 'b94bbc9b-a083-4f66-ad79-ce0392b8e8b4',
      Name: 'NH Columbia',
      Description: 'Un hotel bueno',
      QuantityOfStars: 3,
      IsAvailable: true,
      ImagesPath: ['../../assets/img/nh.jpg'],
      LodgingTouristSpotModel: null,
      ReviewsForLodging: null,
      Address: 'Montevideo',
      PricePerNight: 120,
      ReviewsAverageScore: 4,
    },
    {
      Id: '2bb3620a-2b21-47a3-b0c2-0779a724fa8c',
      Name: 'Proa Sur UY',
      Description: 'Un hotel bueno',
      QuantityOfStars: 3,
      IsAvailable: true,
      ImagesPath: ['../../assets/img/proa.jpg'],
      LodgingTouristSpotModel: null,
      ReviewsForLodging: null,
      Address: 'Montevideo',
      PricePerNight: 120,
      ReviewsAverageScore: 4,
    }
  ];

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




  readonly lodgingsResultOfSearch: LodgingModelForSearchResponse[] =
  [
  {
    Name: 'Proa Sur UY',
    Description: 'Un hotel bueno',
    QuantityOfStars: 3,
    Address: 'Avda. Franklin Roosevelt',
    ImagesPath: ['../../assets/img/proa.jpg'],
    PricePerNight: 120,
    ReviewsAverageScore: 4.2,
    LodgingTouristSpotModel: this.touristSpots[0]
  },
  {
    Name: 'Enjoy',
    Description: 'Un hotel excelente',
    QuantityOfStars: 5,
    Address: 'Avda. Franklin Roosevelt parada 12343344',
    ImagesPath: ['../../assets/img/enjoy.jpg'],
    PricePerNight: 145,
    ReviewsAverageScore: 4.7,
    LodgingTouristSpotModel: this.touristSpots[0]
  },
  {
    Name: 'Arapey Thermal Resort',
    Description: 'Un hotel magnifico',
    QuantityOfStars: 5,
    Address: 'Avda. Luis A. De Herrera',
    ImagesPath: ['../../assets/img/arapey.jpg'],
    PricePerNight: 190,
    ReviewsAverageScore: 4.9,
    LodgingTouristSpotModel: this.touristSpots[0]
  }
  ];

  readonly lodgingsForSearch: LodgingForSearchModel[] =
  [
  {
    CheckIn: new Date(),
    CheckOut: new Date(),
    QuantityOfGuest: [1, 2, 3, 4],
    Lodging: this.lodgingsResultOfSearch[0],
    TotalPriceForSearch: 1900
  },
  {
    CheckIn: new Date(),
    CheckOut: new Date(),
    QuantityOfGuest: [1, 2, 3, 4],
    Lodging: this.lodgingsResultOfSearch[1],
    TotalPriceForSearch: 1900
  },
  {
    CheckIn: new Date(),
    CheckOut: new Date(),
    QuantityOfGuest: [1, 2, 3, 4],
    Lodging: this.lodgingsResultOfSearch[2],
    TotalPriceForSearch: 2220
  }
  ];

  constructor() { }

  getLodgings(): LodgingModelForResponse[] {
    const lodgingsObteined: LodgingModelForResponse[] = [];
    for (const lodging of this.lodgings) {
      lodgingsObteined.push(lodging);
    }
    return lodgingsObteined;
  }

  getLodgingsOfSearch(searchModel: SearchOfLodgingModelForRequest): LodgingForSearchModel[]{
    const lodgingsObteined: LodgingForSearchModel[] = [];
    for (const lodging of this.lodgingsForSearch) {
      lodgingsObteined.push(lodging);
    }
    return lodgingsObteined;
  }

  changeAvailability(id: string, newState: boolean): void {
    for (const lodging of this.lodgings) {
      if (lodging.Id === id && lodging.IsAvailable !== newState) {
        lodging.IsAvailable = newState;
      }
    }
  }

  CreateLodging(lodgingToCreate: LodgingModelForRequest): LodgingModelForResponse {
    return;
    // this is a call to the service in the webAPI to the method POST of LodgingController
  }
}
