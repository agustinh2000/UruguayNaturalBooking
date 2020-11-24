import { LodgingModelForReserveResponseModel } from './LodgingModelForReserveResponseModel';
import { ReserveState, DescriptionOfState } from './ReserveState';

export class ReserveModelForResponse {
  public id: string;
  public name: string;
  public lastName: string;
  public email: string;
  public phoneNumberOfContact: number;
  public descriptionForGuest: string;
  public checkIn: Date;
  public checkOut: Date;
  public quantityOfAdult: number;
  public quantityOfChild: number;
  public quantityOfBaby: number;
  public quantityOfRetired: number;
  public reserveState: ReserveState;
  public descriptionOfState: string;
  public lodging: LodgingModelForReserveResponseModel;
  public totalPrice: number;

  public constructor(init?: Partial<ReserveModelForResponse>) {
    Object.assign(this, init);
  }
}
