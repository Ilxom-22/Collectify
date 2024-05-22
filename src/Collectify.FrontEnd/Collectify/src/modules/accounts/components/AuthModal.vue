<template>

<ModalBase :isActive="props.isModalActive" @closeModal="closeModal">

    <template v-slot:header>
        <h1 class="text-xl font-semibold theme-text-primary">{{ showLoginForm ? 'Login' : 'Register' }}</h1>
    </template>

    <template v-slot:content>
        <div class="p-10 w-screen md:w-[600px]">
            <div class="w-full px-12 mt-10 space-y-5">
                <FormInput v-if="!showLoginForm" v-model="signUpDetails.emailAddress" :label="'Email Address'" :placeholder="'Email Address'" />
                <FormInput v-model="signUpDetails.username" :label="'Username'" :placeholder="'Username'"/>
                <FormInput v-model="signUpDetails.password" :label="'Password'" :placeholder="'Password'" />
            </div>
            
            <div class="flex w-full gap-10 px-12 mt-10">
                <AppButton class="w-full" :text="showLoginForm ? 'I\'m not registered yet' : 'I\'m already registered'" :type="ButtonType.Secondary" @click="changeAuthType"/>
                <AppButton class="w-full" :text="showLoginForm ? 'Login' : 'Register'" @click="onSubmit"/>
            </div>
        </div>
    </template>
</ModalBase>

<message-box ref="messageBox"/>

</template>

<script setup lang="ts">

import ModalBase from '@/common/components/modalBase/ModalBase.vue';
import { ButtonType } from '@/common/components/appButton/ButtonType';
import FormInput from '@/common/components/formInput/FormInput.vue';
import AppButton from '@/common/components/appButton/AppButton.vue';
import { ref } from 'vue';
import { SignUpDetails } from '../models/SignUpDetails';
import { SignInDetails } from '../models/SignInDetails';
import { AuthenticationService } from '@/infrastructure/services/auth/AuthenticationService';
import { MessageType } from '@/common/components/messageBox/MessageType';
import MessageBox from '@/common/components/messageBox/MessageBox.vue'

const props = defineProps({
    isModalActive: {
        type: Boolean,
        required: true
    }
});

const emit = defineEmits(['closeModal']);

const messageBox = ref();
const authenticationService = new AuthenticationService();
const showLoginForm = ref<boolean>(authenticationService.hasAccessToken());
const signUpDetails = ref<SignUpDetails>(new SignUpDetails());
const signInDetails = ref<SignInDetails>(new SignInDetails());

const changeAuthType = () => {
    showLoginForm.value = !showLoginForm.value;

    clearInputs();
}

const closeModal = () => {
    if (props.isModalActive == true) {
        emit('closeModal');   

        clearInputs();
    }
}

const onSubmit = async () => {
    if (showLoginForm.value) {
        await signInAsync();
    }
    else {
        await signUpAsync();
    }
}

const signUpAsync = async () => {
    const response = await authenticationService.signUpAsync(signUpDetails.value);
    
    if (response) {
        messageBox.value.showMessage(response.detail, MessageType.Error);
    }
    else {
        await signInAsync();
    }
}

const signInAsync = async () => {
    signInDetails.value.username = signUpDetails.value.username;
    signInDetails.value.password = signUpDetails.value.password;
    
    const response = await authenticationService.signInAsync(signInDetails.value);

    if (response) {
        messageBox.value.showMessage(response.detail, MessageType.Error);
    }
    else {
        var result = await authenticationService.setCurrentUser();

        closeModal();
    }
}

const clearInputs = () => {
    signUpDetails.value = new SignUpDetails();
    signInDetails.value = new SignInDetails();
}

</script>