<template>
    <div class="relative inline-block text-left">
        <app-button
        @click="toggleDropdown"
        :text="buttonText"
        :size="ActionComponentSize.Small"
        :icon="'fa-solid fa-caret-down'"/>
        
        <!-- Dropdown menu -->
        <div
        v-show="isDropdownVisible"
        class="absolute z-10 mt-2 bg-white divide-y divide-gray-100 rounded-lg shadow dark:bg-gray-700"
        >
        <ul class="py-2 text-sm text-gray-700 dark:text-gray-200">
            <li v-for="(item, index) in menuItems" :key="index">
            <span class="block px-4 py-2 cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
               @click="handleItemClick(item)">
                {{ item }}
            </span>
            </li>
        </ul>
        </div>
    </div>
</template>
  
<script setup lang="ts">
import { ref } from 'vue';
import AppButton from '../appButton/AppButton.vue';
import { ActionComponentSize } from '@/common/constants/ActionComponentSize';

const props = defineProps({
    buttonText: {
        type: String,
        required: true
    },
    menuItems: {
        type: Array<string>,
        required: true
    }
});

const emit = defineEmits(['action']);

const isDropdownVisible = ref(false);

const toggleDropdown = () => {
    isDropdownVisible.value = !isDropdownVisible.value;
};

const closeDropdown = () => {
    isDropdownVisible.value = false;
};

const handleItemClick = (item: string) => {
    closeDropdown();
    
    emit('action', item);
};

</script>
  