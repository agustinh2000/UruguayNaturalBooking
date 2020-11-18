import { TouristSpotModelForLodgingResponseModel } from './TouristSpotModelForLodgingResponseModel';

export class LodgingModelForSearchResponse{
    public Name: string;
    public Description: string;
    public QuantityOfStars: number;
    public  Address: string;
    public ImagesPath: string [];
    public PricePerNight: number;
    public ReviewsAverageScore: number;
    public LodgingTouristSpotModel: TouristSpotModelForLodgingResponseModel;
}
