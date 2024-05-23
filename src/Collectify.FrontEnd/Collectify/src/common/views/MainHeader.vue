<template>
    <div class="flex flex-col">
        <div class="flex items-center w-full h-16 px-4 theme-bg-primary">
            
            <img src="@/assets/collectify_logo.png" class="h-12 mr-4 invert dark:invert-0">
            
            <div class="flex items-center space-x-4 font-black theme-text-primary">
                <router-link to="/">Home</router-link>
                <router-link to="/my-collections" class="text-nowrap">My Collections</router-link>
            </div>

            <search-form :modelValue="searchValue" :placeholder="'Search'" class="w-full"/>

            <div class="flex space-x-4">
                <theme-toggle/>
                <app-button v-if="!authenticationService.isLoggedIn()" class="font-semibold" :type="ButtonType.Primary" :text="'Register/Login'" :size="ActionComponentSize.Small" @click="showAuthModal"/>
                <drop-down v-if="authenticationService.isLoggedIn()" @action="handleActionEvent" class="text-sm font-semibold" :buttonText="currentUser.userName" :menuItems="dropDownItems"/>
            </div>
        </div>
        
        <div class="w-full h-[1px] mt-2 theme-divider"></div>

        <auth-modal :isModalActive="isModalActive" @closeModal="closeModal"></auth-modal>
        <confirmation-modal :isActive="confirmationModalActive" :title="'Are you sure you want to log out?'" @confirmationResult="handleConfirmationResult"/>
        
        <message-box ref="messageBox"/>
    </div>

    

</template>

<script setup lang="ts">
import ThemeToggle from '../components/themeToggle/ThemeToggle.vue';
import AppButton from '../components/appButton/AppButton.vue';
import { ButtonType } from '../components/appButton/ButtonType';
import { ActionComponentSize } from '../constants/ActionComponentSize';
import SearchForm from '../components/searchForm/SearchForm.vue';
import { ref, watch, computed } from 'vue';
import AuthModal from '@/modules/accounts/components/AuthModal.vue';
import { AuthenticationService } from '@/infrastructure/services/auth/AuthenticationService';
import { useAccountStore } from '@/infrastructure/stores/AccountStore';
import DropDown from '../components/dropDown/DropDown.vue';
import { Role } from '@/modules/accounts/models/Role';
import ConfirmationModal from '@/modules/accounts/components/ConfirmationModal.vue';
import MessageBox from '../components/messageBox/MessageBox.vue';
import type { ProblemDetails } from '@/infrastructure/apiClients/ProblemDetails';
import { MessageType } from '../components/messageBox/MessageType';
import { MessageConstants } from '../constants/MessageConstants';
import router from '@/infrastructure/router/Router';

const authenticationService = new AuthenticationService();
const accountStore = useAccountStore();
const searchValue = ref<string>('');
const isModalActive = ref<boolean>(false);
const confirmationModalActive = ref<boolean>(false);
const messageBox = ref();

const currentUser = computed(() => accountStore.account.user);

let dropDownItems = Array<string>();

watch(currentUser, (newValue, oldValue) => {
  if (authenticationService.isLoggedIn()) {
    if (currentUser.value.role === Role.Admin) {
        dropDownItems = [
            'Admin Panel',
            'Log Out', 
         ]
    }
    else {
        dropDownItems = [ 'Log Out' ];
    }
  }
});

const showAuthModal = () => {
    isModalActive.value = true;
}

const closeModal = () => {
    isModalActive.value = false;
}

const handleActionEvent = async (action: string) => {
    if (action == 'Log Out') {
        confirmationModalActive.value = true;
    }
    else if (action == 'Admin Panel') {
      await router.push('/admin');
    }
}

const handleConfirmationResult = async (confirmationResult: boolean) => {
    confirmationModalActive.value = false;

    if (confirmationResult) {
        await logOutAsync();
    }
}

const displayErrorMessage = (error: ProblemDetails) => {
    if (error.isUserFriendly) {
        messageBox.value.showMessage(error.detail, MessageType.Error);
    }
    else {
        messageBox.value.showMessage(MessageConstants.DefaultErrorMessage, MessageType.Error);
    }
}

const logOutAsync = async () => {
    const logOutResult = await authenticationService.logOutAsync();

    if (logOutResult) {
        displayErrorMessage(logOutResult);
    }
}

</script>