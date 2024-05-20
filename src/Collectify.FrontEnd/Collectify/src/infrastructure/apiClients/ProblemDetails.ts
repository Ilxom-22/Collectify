export class ProblemDetails {
    
    public type: string;
    public title: string;
    public status: number;
    public detail: string | null;
    public isUserFriendly: boolean
    public errors: Record<string, string[]> | null

    constructor(type: string, title: string, status: number, detail: string | null, isUserFriendly: boolean, errors: Record<string,string[]> | null) {
        this.type = type;
        this.title = title;
        this.status = status;
        this.detail = detail;
        this.isUserFriendly = isUserFriendly;
        this.errors = errors;
    }
}