export class UserModelForLoginRequest{
    public Email?: string;
    public Password?: string;

    public constructor(init?: Partial<UserModelForLoginRequest>){
        Object.assign(this, init);
    }
}
