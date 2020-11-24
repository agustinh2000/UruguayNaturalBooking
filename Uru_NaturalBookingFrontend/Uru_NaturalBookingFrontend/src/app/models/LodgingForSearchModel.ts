import { LodgingModelForSearchResponse } from './LodgingModelForSearchResponse';

export class LodgingForSearchModel {
  public checkIn: Date;
  public checkOut: Date;
  public quantityOfGuest: number[];
  public lodging: LodgingModelForSearchResponse;
  public totalPriceForSearch: number;
}
