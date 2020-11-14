export class ReviewModelForRequest {
    public IdOfReserveAssociated: string;

    public Score: number;

    public Description: string;

    public constructor(init?: Partial<ReviewModelForRequest>) {
        Object.assign(this, init);
    }
}
