<template>

<ModalBase :isActive="props.isActive" class="h-fit w-fit" @closeModal="emitResult(false)">
    
    <template v-slot:content>
        <div class="pb-6 px-8 min-w-[500px] max-w-[700px]">
            <div class="flex justify-center ">
                <h1 class="text-xl font-semibold break-words whitespace-normal theme-text-primary">{{ props.title }}</h1>
            </div>
        
            <div class="flex gap-5 mt-10">
                <app-button class="w-full" :text="'Cancel'" :type="ButtonType.Secondary" @click="emitResult(false)"/>
                <app-button class="w-full" :text="'Confirm'" :type="ButtonType.Danger" @click="emitResult(true)"/>
            </div>
        </div>
    </template>
   

</ModalBase>

</template>

<script setup lang="ts">

import ModalBase from '@/common/components/modalBase/ModalBase.vue';
import AppButton from '@/common/components/appButton/AppButton.vue';
import { ButtonType } from '@/common/components/appButton/ButtonType';

const props = defineProps({
    isActive: {
        type: Boolean,
        required: true
    },
    title: {
        type: String,
        required: true
    }
});

const emit = defineEmits(['confirmationResult'])

const emitResult = (confirmationResult: boolean) => {
    emit('confirmationResult', confirmationResult)
}

</script>