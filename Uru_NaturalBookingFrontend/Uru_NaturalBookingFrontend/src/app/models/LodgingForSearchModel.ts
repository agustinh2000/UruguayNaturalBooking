import { LodgingModelForSearchResponse } from './LodgingModelForSearchResponse';

export class LodgingForSearchModel{
    public  CheckIn: Date;
    public  CheckOut: Date;
    public  QuantityOfGuest: number[];
    public  Lodging: LodgingModelForSearchResponse;
    public  TotalPriceForSearch: number;
}
