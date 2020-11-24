import { Injectable } from '@angular/core';
import { LodgingModelForResponse } from 'src/app/models/LodgingModelForResponse';
import { LodgingModelForSearchResponse } from 'src/app/models/LodgingModelForSearchResponse';
import { TouristSpotModelForLodgingResponseModel } from 'src/app/models/TouristSpotModelForLodgingResponseModel';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';
import { LodgingForSearchModel } from '../../models/LodgingForSearchModel';
import { SearchOfLodgingModelForRequest } from '../../models/SearchOfLodgingModelForRequest';
import { ReviewModelForResponse } from '../../models/ReviewModelForResponse';

@Injectable({
  providedIn: 'root',
})
export class LodgingService {
  readonly touristSpots: TouristSpotModelForLodgingResponseModel[] = [
    {
      id: '13046b7e-3d83-4576-b459-65c4c965b037',
      name: 'Punta del este',
    },

    {
      id: 'fcceee6e-5b30-433b-bcd7-10b45af6efc5',
      name: 'San Ramón',
    },

    {
      id: 'bd7a2aac-ceb5-49b6-a2ec-c288ba3f7c03',
      name: 'Atlantida',
    },

    {
      id: 'cb4f3a7b-1bff-4c85-a7c9-44a79d4c2c0a',
      name: 'Las Toscas',
    },

    {
      id: 'bcc6f5bc-d580-41ea-a28a-0626784cfee0',
      name: 'La floresta',
    },
  ];

  readonly reviews: ReviewModelForResponse[] = [
    {
      id: '53fd830d-988d-4cf6-91f9-abf920f9dc5b',
      score: 5,
      description: 'Fue una estadia excelente en este hospedaje de lujo.',
      nameOfWhoComments: 'Agustin',
      lastNameOfWhoComments: 'Hernandorena',
    },
    {
      id: '53fd830d-988d-4cf6-91f9-abf920f9dc5c',
      score: 1,
      description: 'Me encontre con JP y me quiso clavar muy mal.',
      nameOfWhoComments: 'Joaquin',
      lastNameOfWhoComments: 'Lamela',
    },
  ];

  readonly lodgings: LodgingModelForResponse[] = [
    {
      id: '4bd1d316-b8eb-42f3-8e76-af41b2687a10',
      name: 'Enjoy',
      description: 'Un hotel increible',
      quantityOfStars: 5,
      isAvailable: true,
      imagesPath: ['../../assets/img/enjoy.jpg'],
      lodgingTouristSpotModel: null,
      reviewsForLodging: null,
      address: 'Punta del Este',
      pricePerNight: 120,
      reviewsAverageScore: 4,
    },
    {
      id: '502c715e-33fc-4df9-94f6-bd62a6be3d14',
      name: 'Sheraton',
      description: 'Un hotel genial',
      quantityOfStars: 5,
      isAvailable: false,
      imagesPath: ['../../assets/img/sheraton.jpg'],
      lodgingTouristSpotModel: null,
      reviewsForLodging: null,
      address: 'Colonia',
      pricePerNight: 120,
      reviewsAverageScore: 4,
    },
    {
      id: '5690844e-ca55-4cea-a4d1-2e370fa147dc',
      name: 'Ibis',
      description: 'Un hotel increible',
      quantityOfStars: 3,
      isAvailable: false,
      imagesPath: ['../../assets/img/ibis.jpg'],
      lodgingTouristSpotModel: null,
      reviewsForLodging: null,
      address: 'Montevideo',
      pricePerNight: 120,
      reviewsAverageScore: 4,
    },
    {
      id: '28311417-7c70-4bae-9295-571fab4efe50',
      name: 'Arapey Thermal Resort',
      description: 'Un hotel fantastico',
      quantityOfStars: 5,
      isAvailable: true,
      imagesPath: ['../../assets/img/arapey.jpg'],
      lodgingTouristSpotModel: null,
      reviewsForLodging: null,
      address: 'Montevideo',
      pricePerNight: 120,
      reviewsAverageScore: 4,
    },
    {
      id: 'b94bbc9b-a083-4f66-ad79-ce0392b8e8b4',
      name: 'NH Columbia',
      description: 'Un hotel bueno',
      quantityOfStars: 3,
      isAvailable: true,
      imagesPath: ['../../assets/img/nh.jpg'],
      lodgingTouristSpotModel: null,
      reviewsForLodging: null,
      address: 'Montevideo',
      pricePerNight: 120,
      reviewsAverageScore: 4,
    },
    {
      id: '17e40d14-7124-46ab-8097-4fa5276718cf',
      name: 'Proa Sur UY',
      description: 'Un hotel bueno',
      quantityOfStars: 3,
      isAvailable: true,
      imagesPath: ['../../assets/img/proa.jpg', '../../assets/img/proa2.jpg'],
      lodgingTouristSpotModel: this.touristSpots[0],
      reviewsForLodging: this.reviews,
      address: 'Montevideo',
      pricePerNight: 120,
      reviewsAverageScore: 4.0,
    },
  ];

  readonly lodgingsResultOfSearch: LodgingModelForSearchResponse[] = [
    {
      id: '17e40d14-7124-46ab-8097-4fa5276718cf',
      name: 'Proa Sur UY',
      description: 'Un hotel bueno',
      quantityOfStars: 3,
      address: 'Avda. Franklin Roosevelt',
      imagesPath: ['../../assets/img/proa.jpg', '../../assets/img/proa2.jpg'],
      pricePerNight: 120,
      reviewsAverageScore: 4.2,
      lodgingTouristSpotModel: this.touristSpots[0],
      reviewsForLodging: this.reviews,
    },
    {
      id: 'b8d09fa7-6469-4fc4-b250-156000a76e8a',
      name: 'Enjoy',
      description: 'Un hotel excelente',
      quantityOfStars: 5,
      address: 'Avda. Franklin Roosevelt parada 12343344',
      imagesPath: ['../../assets/img/enjoy.jpg'],
      pricePerNight: 145,
      reviewsAverageScore: 4.7,
      lodgingTouristSpotModel: this.touristSpots[0],
      reviewsForLodging: this.reviews,
    },
    {
      id: 'a7e9831d-bafc-4915-8e26-523a34ec03a5',
      name: 'Arapey Thermal Resort',
      description: 'Un hotel magnifico',
      quantityOfStars: 5,
      address: 'Avda. Luis A. De Herrera',
      imagesPath: ['../../assets/img/arapey.jpg'],
      pricePerNight: 190,
      reviewsAverageScore: 4.9,
      lodgingTouristSpotModel: this.touristSpots[0],
      reviewsForLodging: this.reviews,
    },
  ];

  readonly lodgingsForSearch: LodgingForSearchModel[] = [
    {
      checkIn: new Date(),
      checkOut: new Date(),
      quantityOfGuest: [1, 2, 3, 4],
      lodging: this.lodgingsResultOfSearch[0],
      totalPriceForSearch: 1900,
    },
    {
      checkIn: new Date(),
      checkOut: new Date(),
      quantityOfGuest: [1, 2, 3, 4],
      lodging: this.lodgingsResultOfSearch[1],
      totalPriceForSearch: 1900,
    },
    {
      checkIn: new Date(),
      checkOut: new Date(),
      quantityOfGuest: [1, 2, 3, 4],
      lodging: this.lodgingsResultOfSearch[2],
      totalPriceForSearch: 2220,
    },
  ];

  constructor() {}

  getLodgingById(lodgingId: string): LodgingModelForResponse {
    return this.lodgings[5];
  }

  getLodgings(): LodgingModelForResponse[] {
    const lodgingsObteined: LodgingModelForResponse[] = [];
    for (const lodging of this.lodgings) {
      lodgingsObteined.push(lodging);
    }
    return lodgingsObteined;
  }

  changeAvailability(id: string, newState: boolean): void {
    for (const lodging of this.lodgings) {
      if (lodging.id === id && lodging.isAvailable !== newState) {
        lodging.isAvailable = newState;
      }
    }
  }

  CreateLodging(
    lodgingToCreate: LodgingModelForRequest
  ): LodgingModelForResponse {
    return;
    // this is a call to the service in the webAPI to the method POST of LodgingController
  }

  isValidLodging(idLodging: string): boolean {
    const lodgingsObteined: LodgingModelForResponse[] = this.getLodgings();
    return lodgingsObteined.some((l) => l.id === idLodging);
  }
}
