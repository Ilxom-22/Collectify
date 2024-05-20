<template>

<Teleport to="body">

<!-- Modal background overlay -->
<div class="fixed inset-0 z-30 flex items-center justify-center modal-bg-overlay-blur" @click="emit('closeModal')"
     :class="{'animate-backgroundFadeIn': props.isActive, 'animate-backgroundFadeOut': !props.isActive, 'hidden': hideModal}">

    <!-- Modal container -->
    <div
        class="relative z-40 overflow-auto w-fit h-fit no-scrollbar theme-modal-bg modal-border-round theme-modal-border" @click.stop>
        <div class="relative w-full h-full pt-20">

            <close-button class="absolute top-5 right-5" @click="emit('closeModal')"/>

            <!-- Modal header -->
            <div class="absolute -translate-x-1/2 top-6 left-1/2">
                <slot v-bind="$attrs" name="header"></slot>
            </div>

            <!-- Modal content -->
            <slot v-bind="$attrs" name="content"></slot>
        </div>
    </div>
</div>

</Teleport>


</template>

<script setup lang="ts">

import {onBeforeMount, onBeforeUnmount, ref, watch} from "vue";
import CloseButton from "../appButton/CloseButton.vue";
import { DocumentService } from "@/infrastructure/services/DocumentService";
import { TimerService } from "@/infrastructure/services/TimerService";

const timerService = new TimerService();
const documentService = new DocumentService();

const props = defineProps({
    isActive: {
        type: Boolean,
        default: false
    }
});

const timer = ref<number>(0);
const hideModal = ref<boolean>(!props.isActive);
const emit = defineEmits(['closeModal']);

onBeforeMount(() => setStyles());
watch(() => props.isActive, () => setStyles());
onBeforeUnmount(() => timerService.clearTimer(timer.value));

const setStyles = () => {
    documentService.handleBodyOverflow(props.isActive);
    timerService.setTimer(() => hideModal.value = !props.isActive, props.isActive ? 0 : 300);
}

</script>