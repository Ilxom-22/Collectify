<template>
    <Teleport to="body">
      <div v-if="isVisible" :class="[messageStyle, 'z-50 fixed top-0 left-1/2 transform -translate-x-1/2 flex justify-center rounded-xl p-2 w-fit mt-4']">
        <h1 class="text-white theme-action-transition">{{ displayedMessage }}</h1>
      </div>
    </Teleport>
</template>
  
<script setup lang="ts">

import { MessageType } from './MessageType';
import { computed, ref, watch } from 'vue';
  
const isVisible = ref(false);
const displayedMessage = ref('');
const messageType = ref(MessageType.Success);
let timeoutId: number | null;
  
const showMessage = (message: string, type: MessageType = MessageType.Error) => {
    if (message === null || message === '' || message === undefined) {
        return;
    }

    if (timeoutId) {
      clearTimeout(timeoutId);
      timeoutId = null;
    }
    
    displayedMessage.value = message;
    messageType.value = type;
    isVisible.value = true;

    timeoutId = setTimeout(() => {
        hideMessage();
        timeoutId = null;
    }, 3000);
};
  
const hideMessage = () => {
    isVisible.value = false;
};

const messageStyle = computed(() => {
    switch (messageType.value) {
      case MessageType.Success:
        return 'theme-action-success';
      case MessageType.Error:
        return 'theme-action-danger';
      default:
        return '';
    }
});

defineExpose({
  showMessage
});

</script>
