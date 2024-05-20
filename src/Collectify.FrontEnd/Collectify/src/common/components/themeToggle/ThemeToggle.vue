<template>

<label class="relative inline-flex items-center cursor-pointer">
    <input type="checkbox" class="sr-only peer" v-model="isChecked">
    <div class="relative w-14 h-8 bg-tertiaryColor peer-focus:outline-none 
    peer-focus:ring-4 peer-focus:ring-accentPrimaryColor dark:peer-focus:ring-accentPrimaryColorDark rounded-full 
    peer dark:bg-gray-700 peer-checked:after:translate-x-full rtl:peer-checked:after:-translate-x-full 
    peer-checked:after:border-white after:content-[''] after:absolute after:top-[4px] after:start-[4px] 
    after:bg-white after:border-gray-300 after:border after:rounded-full after:h-6 after:w-6 
    after:transition-all dark:border-gray-600 peer-checked:bg-accentTertiaryColorDark">
        <span class="absolute z-10 items-center justify-center text-secondaryContentColor inset-y-1 left-2 animate-fadeIn"
            :class="{ 'hidden' : isChecked }">
            <i class="fas fa-sun"></i>
        </span>

        <span class="absolute z-10 items-center justify-center text-accentPrimaryColorDark inset-y-1 right-[10px] animate-fadeIn"
            :class="{ 'hidden' : !isChecked }">
            <i class="fas fa-moon"></i>
        </span>
    </div>
</label>

    
</template>

<script setup lang="ts">

import { AppThemeService } from '@/infrastructure/services/appTheme/AppThemeService';
import { ref, watch } from 'vue';

const appThemeService = new AppThemeService();

const isChecked = ref(appThemeService.isDarkMode());

watch(isChecked, (newValue) => {
    appThemeService.toggleDarkMode(newValue);
});

</script>
