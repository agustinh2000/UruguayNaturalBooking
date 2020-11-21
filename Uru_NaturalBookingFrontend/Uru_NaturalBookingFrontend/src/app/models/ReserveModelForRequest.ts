export class ReserveModelForRequest {

    public Name: string;

    public LastName: string;

    public Email: string;

    public CheckIn: Date;

    public CheckOut: Date;

    public QuantityOfAdult: number;

    public QuantityOfChild: number;

    public QuantityOfRetired: number;

    public QuantityOfBaby: number;

    public IdOfLodgingToReserve: string;

    public constructor(init?: Partial<ReserveModelForRequest>) {
        Object.assign(this, init);
    }
}
