<template>

<ModalBase :isActive="props.isModalActive" @closeModal="closeModal">

    <template v-slot:header>
        <h1 class="text-xl font-semibold theme-text-primary">{{ showLoginForm ? 'Login' : 'Register' }}</h1>
    </template>

    <template v-slot:content>
        <div class="p-10 w-screen md:w-[600px]">
            <div class="w-full px-12 mt-10 space-y-5">
                <FormInput v-if="!showLoginForm" v-model="testModel" :label="'Email Address'" :placeholder="'Email Address'" />
                <FormInput v-model="testModel" :label="'Username'" :placeholder="'Username'"/>
                <FormInput v-model="testModel" :label="'Password'" :placeholder="'Password'" />
            </div>
            
            <div class="flex w-full gap-10 px-12 mt-10">
                <AppButton class="w-full" :text="showLoginForm ? 'I\'m not registered yet' : 'I\'m already registered'" :type="ButtonType.Secondary" @click="changeAuthType"/>
                <AppButton class="w-full" :text="showLoginForm ? 'Login' : 'Register'" @click="onSubmit"/>
            </div>
        </div>
    
    </template>
</ModalBase>

</template>

<script setup lang="ts">
import ModalBase from '@/common/components/modalBase/ModalBase.vue';
import { ButtonType } from '@/common/components/appButton/ButtonType';
import FormInput from '@/common/components/formInput/FormInput.vue';
import AppButton from '@/common/components/appButton/AppButton.vue';
import { ref } from 'vue';

const showLoginForm = ref<boolean>(true);

const props = defineProps({
    isModalActive: {
        type: Boolean,
        required: true
    }
})

const emit = defineEmits(['closeModal']);

const testModel = '';

const closeModal = () => {
    if (props.isModalActive == true) {
        emit('closeModal');   
    }
}

const onSubmit = () => {
    
}

const changeAuthType = () => {
    showLoginForm.value = !showLoginForm.value;
}

</script>