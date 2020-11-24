export class ReserveModelForRequest {
  public name: string;

  public lastName: string;

  public email: string;

  public checkIn: Date;

  public checkOut: Date;

  public quantityOfAdult: number;

  public quantityOfChild: number;

  public quantityOfRetired: number;

  public quantityOfBaby: number;

  public idOfLodgingToReserve: string;

  public constructor(init?: Partial<ReserveModelForRequest>) {
    Object.assign(this, init);
  }
}
