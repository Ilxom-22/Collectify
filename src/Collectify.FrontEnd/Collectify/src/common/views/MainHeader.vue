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

    </div>

    

</template>

<script setup lang="ts">
import ThemeToggle from '../components/themeToggle/ThemeToggle.vue';
import AppButton from '../components/appButton/AppButton.vue';
import { ButtonType } from '../components/appButton/ButtonType';
import { ActionComponentSize } from '../constants/ActionComponentSize';
import SearchForm from '../components/searchForm/SearchForm.vue';
import { onMounted, ref, watch, computed } from 'vue';
import AuthModal from '@/modules/accounts/components/AuthModal.vue';
import { AuthenticationService } from '@/infrastructure/services/auth/AuthenticationService';
import { useAccountStore } from '@/infrastructure/stores/AccountStore';
import DropDown from '../components/dropDown/DropDown.vue';
import { DropDownItem } from '../components/dropDown/DropDownItem';
import { DropDownItemType } from '../components/dropDown/DropDownItemType';
import { Role } from '@/modules/accounts/models/Role';

const authenticationService = new AuthenticationService();
const accountStore = useAccountStore();
const searchValue = ref<string>('');
const isModalActive = ref<boolean>(false);

const currentUser = computed(() => accountStore.account.user);

const dropDownItems = ref<Array<DropDownItem>>([
    new DropDownItem('Log Out', DropDownItemType.Action)
]);

watch(currentUser, (newValue, oldValue) => {
  if (authenticationService.isLoggedIn()) {
    if (currentUser.value.role === Role.Admin) {
        dropDownItems.value.unshift(new DropDownItem('Admin Panel', DropDownItemType.Route, '/admin'))
    }
    else {
        dropDownItems.value = [ new DropDownItem('Log Out', DropDownItemType.Action) ];
    }
  }
});

const showAuthModal = () => {
    isModalActive.value = true;
}

const closeModal = () => {
    isModalActive.value = false;
}

const handleActionEvent = (action: string) => {
    if (action == 'Log Out') {
        console.log('Log out event occured!');
    }
}

</script>