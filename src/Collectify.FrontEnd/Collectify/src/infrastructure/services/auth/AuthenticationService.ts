import { useAccountStore } from '@/infrastructure/stores/AccountStore' 
import { LocalStorageService } from '../storage/LocalStorageService';
import { CollectifyApiClient } from '@/infrastructure/collectifyApiClients/CollectifyApiClient';
import { Account } from '@/modules/accounts/models/Account';
import type { SignInDetails } from '@/modules/accounts/models/SignInDetails';
import type { SignUpDetails } from '@/modules/accounts/models/SignUpDetails';

export class AuthenticationService {
    private collectifyApiClient: CollectifyApiClient;
    private localStorageService: LocalStorageService;
    private accountStore = useAccountStore();

    constructor() {
        this.collectifyApiClient = new CollectifyApiClient();
        this.localStorageService = new LocalStorageService();
    }

    public hasAccessToken() {
        return this.localStorageService.get('accessToken') !== null;
    }

    public isLoggedIn() {
        return this.accountStore.account.isLoggedIn();
    }

    public async signInAsync(signInDetails: SignInDetails) {
        const signInResponse = await this.collectifyApiClient.auth.signInAsync(signInDetails);

        if (!signInResponse.isSuccess)
            return signInResponse.error;

        this.localStorageService.set('accessToken', signInResponse.response);

        await this.setCurrentUser();
    }

    public async signUpAsync(signUpDetails: SignUpDetails) {
        const signUpResponse = await this.collectifyApiClient.auth.signUpAsync(signUpDetails);

        if (!signUpResponse.isSuccess)
            return signUpResponse.error;
    }

    public async setCurrentUser() {
        if (!this.hasAccessToken()) return;

        const currentUser = await this.collectifyApiClient.auth.getCurrentUser();
        if (currentUser.isSuccess) {
            const account = new Account();
            account.user = currentUser.response!;
            this.accountStore.set(account);
        }
        else {
            return currentUser.error;
        }
    }
}