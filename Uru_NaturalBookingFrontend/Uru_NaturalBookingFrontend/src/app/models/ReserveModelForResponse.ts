import { LodgingModelForReserveResponseModel } from './LodgingModelForReserveResponseModel';
import { ReserveState, DescriptionOfState } from './ReserveState';

export class ReserveModelForResponse {
    public Name: string;
    public LastName: string;
    public Email: string;
    public DescriptionForGuest: string;
    public CheckIn: Date;
    public CheckOut: Date;
    public ReserveState: ReserveState;
    public DescriptionOfState: string;
    public Lodging: LodgingModelForReserveResponseModel;

    public constructor(init?: Partial<ReserveModelForResponse>) {
        Object.assign(this, init);
    }
}
