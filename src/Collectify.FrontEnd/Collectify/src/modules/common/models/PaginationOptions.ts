export class PaginationOptions {
    public pageToken: number;
    public pageSize: number;

    constructor(pageToken: number, pageSize: number) {
        this.pageToken = pageToken;
        this.pageSize = pageSize;
    }
}