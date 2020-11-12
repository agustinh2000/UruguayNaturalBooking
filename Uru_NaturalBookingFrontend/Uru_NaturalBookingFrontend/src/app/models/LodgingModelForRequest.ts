export class LodgingModelForRequest{
        public Name: string;

        public Description: string;

        public QuantityOfStars: number;

        public Address: string;

        public Images: string[];

        public PricePerNight: number;

        public IsAvailable: boolean;

        public TouristSpotId: string;

        public constructor(init?: Partial<LodgingModelForRequest>) {
                Object.assign(this, init);
        }
}
