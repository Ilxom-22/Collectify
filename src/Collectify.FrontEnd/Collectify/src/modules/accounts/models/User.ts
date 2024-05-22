import type { AccountStatus } from "./AccountStatus";
import type { Role } from "./Role";

export class User {
    public id!: string;
    public userName!: string;
    public emailAddress!: string;
    public role!: Role;
    public status!: AccountStatus;
}