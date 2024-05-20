export class ProblemDetails {
    public type: string;
    public title: string;
    public status: number;
    public detail: string | null;
    public isUserFriendly: boolean

    constructor(type: string, title: string, status: number, detail: string | null, isUserFriendly: boolean) {
        this.type = type;
        this.title = title;
        this.status = status;
        this.detail = detail;
        this.isUserFriendly = isUserFriendly;
    }
}