<template>
    <div
        class="relative flex w-full theme-input-bg theme-action-transition theme-action-shadow theme-text-primary theme-action-border-round"
        :class="wrapperStyles" @focusin="isFocused = true" @focusout="isFocused = false">

        <!-- Form input field -->
        <input type="text" name="input"
               class="w-full z-20 px-2.5 pb-2.5 pt-5 peer theme-input theme-text-primary
                      theme-input-placeholder theme-action-transition text-base"
               :placeholder="placeholder"
               :value="modelValue"
               @input="onInput" />

        <label for="input" :class="'text-md top-4 -translate-y-4 peer-focus:-translate-y-4'" class="absolute z-10 transform scale-75 origin-[0] start-2.5
                    peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto
                    peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0
                     theme-input-label theme-action-transition">
            {{ label }}
        </label>
    </div>
</template>

<script setup lang="ts">
import { defineProps, ref, computed } from 'vue';

const props = defineProps({
    modelValue: {
        type: String,
        required: true
    },
    label: {
        type: String,
        default: ''
    },
    placeholder: {
        type: String,
        default: ''
    }
});

const emit = defineEmits(['update:modelValue']);

const onInput = (event: Event) => {
  const target = event.target as HTMLInputElement;
  emit('update:modelValue', target.value);
};

const isFocused = ref<boolean>(false);

const wrapperStyles = computed(() => {
    let styles = '';

    if(isFocused.value) {
        styles += ' theme-input-border-focus';
    } else {
        styles += ' theme-input-border';
    }

    return styles;
});
</script>
