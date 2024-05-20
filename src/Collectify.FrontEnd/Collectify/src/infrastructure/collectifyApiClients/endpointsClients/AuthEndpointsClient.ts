import type ApiClientBase from "@/infrastructure/apiClients/ApiClientBase";
import type { User } from "@/modules/accounts/models/User";
import type { SignUpDetails } from "@/modules/accounts/models/SignUpDetails";
import type { SignInDetails } from "@/modules/accounts/models/SignInDetails";

export class AuthEndpointsClient {
    public client: ApiClientBase;

    constructor(client: ApiClientBase) {
        this.client = client;
    }

    public async getCurrentUser() {
        const endpointUrl = 'api/auth/me';
        return await this.client.getAsync<User>(endpointUrl);
    }

    public async signUpAsync(signUpDetails: SignUpDetails) {
        const endpointUrl = 'api/auth/sign-up';
        return await this.client.postAsync<User>(endpointUrl, signUpDetails);
    }

    public async signInAsync(signInDetails: SignInDetails) {
        const endpointUrl = 'api/auth/sign-in';
        return await this.client.postAsync(endpointUrl, signInDetails)
    }

    public async logOutAsync() {
        const endpointUrl = 'api/auth/log-out';
        return await this.client.postAsync(endpointUrl);
    }
}