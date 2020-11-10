export class LoginUser{
    public Email?: string;
    public Password?: string;

    public constructor(init?: Partial<LoginUser>){
        Object.assign(this, init);
    }
}
