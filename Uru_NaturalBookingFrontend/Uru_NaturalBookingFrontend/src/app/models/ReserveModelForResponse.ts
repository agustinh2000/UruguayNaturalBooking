import { LodgingModelForReserveResponseModel } from './LodgingModelForReserveResponseModel';
import { ReserveState, DescriptionOfState } from './ReserveState';

export class ReserveModelForResponse {
    public Id: string;
    public Name: string;
    public LastName: string;
    public Email: string;
    public PhoneNumberOfContact: number;
    public DescriptionForGuest: string;
    public CheckIn: Date;
    public CheckOut: Date;
    public QuantityOfAdult: number;
    public QuantityOfChild: number;
    public QuantityOfBaby: number;
    public QuantityOfRetired: number;
    public ReserveState: ReserveState;
    public DescriptionOfState: string;
    public Lodging: LodgingModelForReserveResponseModel;
    public TotalPrice: number;

    public constructor(init?: Partial<ReserveModelForResponse>) {
        Object.assign(this, init);
    }
}
