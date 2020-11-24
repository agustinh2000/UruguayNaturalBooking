import { ReviewModelForResponse } from './ReviewModelForResponse';
import { TouristSpotModelForLodgingResponseModel } from './TouristSpotModelForLodgingResponseModel';

export class LodgingModelForResponse {
  public id: string;

  public name: string;

  public description: string;

  public isAvailable: boolean;

  public quantityOfStars: number;

  public address: string;

  public imagesPath: string[];

  public pricePerNight: number;

  public reviewsAverageScore: number;

  public reviewsForLodging: ReviewModelForResponse[];

  public lodgingTouristSpotModel: TouristSpotModelForLodgingResponseModel;
}
