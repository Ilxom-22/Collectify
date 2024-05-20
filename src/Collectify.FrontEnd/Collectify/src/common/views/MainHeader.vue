<template>
    <div class="flex flex-col items-center justify-center">
        <div class="flex items-center justify-between w-full h-16 px-4 theme-bg-primary">
            <div class="flex items-center space-x-4 font-black theme-text-primary">
                <img src="@/assets/collectify_logo.png" class="h-10 mr-4 invert dark:invert-0">
                <h1>Home</h1>
                <h1 class="text-nowrap">My Collections</h1>
            </div>

            <div class="w-full">
                <search-form :modelValue="searchValue" :placeholder="'Search'" class="w-full"/>
            </div>

            <div class="flex space-x-4 item-center ">
                <theme-toggle/>
                <app-button class="font-semibold" :type="ButtonType.Primary" :text="'Register/Login'" :size="ActionComponentSize.Small" @click="showAuthModal"/>
                <h1 v-if="false" class="font-semibold">Welcome James!</h1>
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
import { ref } from 'vue';
import AuthModal from '@/modules/accounts/components/AuthModal.vue';

const searchValue = ref<string>('');

const isModalActive = ref<boolean>(false);

const showAuthModal = () => {
    isModalActive.value = true;
}

const closeModal = () => {
    isModalActive.value = false;
}

</script>