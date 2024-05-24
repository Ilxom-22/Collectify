import type ApiClientBase from "@/infrastructure/apiClients/ApiClientBase";
import type { PaginationOptions } from "@/modules/common/models/PaginationOptions";

export class AccountsEndpointsClient {
    public client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public getUsers(paginationOptions: PaginationOptions) {
        const endpointUrl = `api/accounts?pageToken=${paginationOptions.pageToken}&pageSize=${paginationOptions.pageSize}`;
        
        this.client.getAsync(endpointUrl);
    }
}