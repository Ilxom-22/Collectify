import ApiClientBase from "../apiClients/ApiClientBase";
import { AuthEndpointsClient } from "./endpointsClients/AuthEndpointsClient";

export class CollectifyApiClient {
    private readonly client: ApiClientBase;

    private readonly baseUrl: string;

    constructor() {
        this.baseUrl = " http://localhost:5031/";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl,
            withCredentials: true
        });

        this.auth = new AuthEndpointsClient(this.client);
    }

    public readonly auth: AuthEndpointsClient;
}