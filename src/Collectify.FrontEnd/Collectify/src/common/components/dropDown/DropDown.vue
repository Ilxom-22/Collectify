<template>
    <div class="relative inline-block text-left">
        <app-button
        @click="toggleDropdown"
        ref="dropdownButton"
        :text="buttonText"
        :size="ActionComponentSize.Small"
        :icon="'fa-solid fa-caret-down'"
        />
        <!-- Dropdown menu -->
        <div
        v-show="isDropdownVisible"
        ref="dropdownMenu"
        class="absolute z-10 mt-2 bg-white divide-y divide-gray-100 rounded-lg shadow dark:bg-gray-700"
        >
        <ul class="py-2 text-sm text-gray-700 dark:text-gray-200">
            <li v-for="(item, index) in menuItems" :key="index">
            <a
                href="#"
                class="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                @click="handleItemClick(item)"
            >
                {{ item.name }}
            </a>
            </li>
        </ul>
        </div>
    </div>
</template>
  
<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue';
import AppButton from '../appButton/AppButton.vue';
import { ActionComponentSize } from '@/common/constants/ActionComponentSize';
import { DropDownItem } from './DropDownItem';
import { DropDownItemType } from './DropDownItemType';
import router from '@/infrastructure/router/Router';

const props = defineProps({
    buttonText: {
        type: String,
        required: true
    },
    menuItems: {
        type: Array<DropDownItem>,
        required: true
    }
});

const emit = defineEmits(['action']);

const isDropdownVisible = ref(false);
const dropdownButton = ref<HTMLElement | null>(null);
const dropdownMenu = ref<HTMLElement | null>(null);

const toggleDropdown = () => {
    isDropdownVisible.value = !isDropdownVisible.value;
};

const handleDocumentClick = (event: MouseEvent) => {
    if (isDropdownVisible.value) {
        closeDropdown(event);
    }
};

const closeDropdown = (event: MouseEvent) => {
    if (
        dropdownButton.value &&
        dropdownMenu.value &&
        !dropdownButton.value.contains(event.target as Node) &&
        !dropdownMenu.value.contains(event.target as Node)
    ) {
        isDropdownVisible.value = false;
    }
};

const handleItemClick = (item: DropDownItem) => {
    if (item.type == DropDownItemType.Route) {
        router.push(item.url);
    }
    else {
        emit('action', item.name);
    }

    isDropdownVisible.value = false;
};

onMounted(() => {
    document.addEventListener('click', handleDocumentClick);
});

onBeforeUnmount(() => {
    document.removeEventListener('click', handleDocumentClick);
});
</script>
  