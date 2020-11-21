import { TouristSpotModelForLodgingResponseModel } from './TouristSpotModelForLodgingResponseModel';
import { ReviewModelForResponse } from './ReviewModelForResponse';

export class LodgingModelForSearchResponse{
    public Id: string;
    public Name: string;
    public Description: string;
    public QuantityOfStars: number;
    public  Address: string;
    public ImagesPath: string [];
    public PricePerNight: number;
    public ReviewsAverageScore: number;
    public LodgingTouristSpotModel: TouristSpotModelForLodgingResponseModel;
    public ReviewsForLodging: ReviewModelForResponse[];
}
