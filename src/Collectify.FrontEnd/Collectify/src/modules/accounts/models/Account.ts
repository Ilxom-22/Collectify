import { User } from "./User";

export class Account {
    public user!: User;

    public isLoggedIn = () => {
        return this.user !== null && this.user !== undefined;
    }
}