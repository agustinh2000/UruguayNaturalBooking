export class UserModelForResponse{
    public id: string;
    public userName: string;
    public mail: string;
    public name: string;
    public lastName: string;
    public password: string;

    public constructor(init?: Partial<UserModelForResponse>) {
        Object.assign(this, init);
    }
}