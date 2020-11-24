import { TouristSpotModelForLodgingResponseModel } from './TouristSpotModelForLodgingResponseModel';
import { ReviewModelForResponse } from './ReviewModelForResponse';

export class LodgingModelForSearchResponse {
  public id: string;
  public name: string;
  public description: string;
  public quantityOfStars: number;
  public address: string;
  public imagesPath: string[];
  public pricePerNight: number;
  public reviewsAverageScore: number;
  public lodgingTouristSpotModel: TouristSpotModelForLodgingResponseModel;
  public reviewsForLodging: ReviewModelForResponse[];
}
