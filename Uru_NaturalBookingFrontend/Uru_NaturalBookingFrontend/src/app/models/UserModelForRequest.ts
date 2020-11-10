export class UserModelForRequest {
    public Name: string;
    public LastName: string;
    public UserName: string;
    public Password: string;
    public Mail: string;

    public constructor(init?: Partial<UserModelForRequest>) {
        Object.assign(this, init);
    }
}
