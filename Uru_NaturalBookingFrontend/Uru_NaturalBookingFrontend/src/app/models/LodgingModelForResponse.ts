import { ReviewModelForResponse } from './ReviewModelForResponse';
import { TouristSpotModelForLodgingResponseModel } from './TouristSpotModelForLodgingResponseModel';

export class LodgingModelForResponse{
        public Id: string;

        public Name: string;

        public Description: string;

        public IsAvailable: boolean;

        public QuantityOfStars: number;

        public Address: string;

        public ImagesPath: string[];

        public PricePerNight: number;

        public ReviewsAverageScore: number;

        public ReviewsForLodging: ReviewModelForResponse[];

        public LodgingTouristSpotModel: TouristSpotModelForLodgingResponseModel;
}
